using OptionSoft.Core.Extensions;
using OptionSoft.Core.Helpers;
using RawImport_Lightspeed.LightspeedClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using Deal = RawImport_Lightspeed.LightspeedClasses.LS_Deal;

namespace RawImport_Lightspeed
{
    class LightspeedCalls
    {
        protected const int THREADCHUNKS = 8;
        public List<string> ErrorList { get; set; }
        private const string BaseUrl = @"https://int.LightspeedADP.com/lsapi/";
        private static string UserName = "Sample";
        private static string Password = "L1ght$p33d$amP1e";
        private static int ItemsPerCall = 500;

        //private static string UserName = "optionsoft";
        //private static string Password = "00006987d7";

        private const string urlDateFormat = "yyyy-MM-dd";

        private string FormatDate(DateTime sDate)
        {
            return $"datetime'{sDate.ToString(urlDateFormat)}'";
        }

        public LightspeedCalls()
        {
            ErrorList = new List<string>();
        }
        private enum LightspeedSource
        {
            DealDetail,
            Customer,
            Dealer,
        }
        private enum CompareMethod
        {
            Equals,
            GreaterThan,
            GreaterThanOrEqual,
        }

        private static string BuildUrl(LightspeedSource reqType, string dealerID, string key, CompareMethod compareMethod, IEnumerable<string> parameters, int skip)
        {
            StringBuilder req = new StringBuilder(BaseUrl);
            req.Append(reqType.ToString());
            if(reqType != LightspeedSource.Dealer) req.Append($"/76155991");
            if (skip > 0)
            {
                req.Append("?$skip=" + skip.ToString() + "&$top=" + ItemsPerCall.ToString());
            }
            else req.Append("?$top=" + ItemsPerCall.ToString());
            if (parameters.Any())
            {
                req.Append("&$filter=");
                req.Append(AppendCriteria(key, compareMethod, parameters));
            }

            string sURL = req.ToString();
            return sURL;
        }

        private static string AppendCriteria(string key, CompareMethod compareMethod, IEnumerable<string> parameters)
        {
            var compare = compareMethod == CompareMethod.GreaterThanOrEqual ? " ge " : compareMethod == CompareMethod.GreaterThan ? " gt " : "=";
            return string.Join(" or ", parameters.Select(xx => $"{key}{compare}{xx}"));
        }

        private static HttpWebRequest BuildRequest(string sURL)
        {
            CredentialCache cache = new CredentialCache
                {
                    {
                        new Uri(sURL),
                        "Basic",
                        new NetworkCredential(UserName, Password)
                    }
                };
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(sURL);
            request.Credentials = cache;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add(
                    "Authorization",
                    "Basic " + Convert.ToBase64String(
                        new ASCIIEncoding().GetBytes(
                            UserName + ":" + Password
                        )
                    )
                );
            request.Timeout = 9999 * 60 * 1000;
            return request;
        }

        private IEnumerable<T> GetDataFromLightspeed<T>(LightspeedSource reqType, string dealerID, string parentCompanyID, string key, CompareMethod compareMethod, int skip, params string[] parameters)
        {
            string sURL = BuildUrl(reqType, dealerID, key, compareMethod, parameters, skip);

            HttpWebRequest request = BuildRequest(sURL);

            var startTime = DateTime.UtcNow;
            var dataStream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var retValue = Newtonsoft.Json.JsonConvert.DeserializeObject<T[]>(reader.ReadToEnd());
            DbQueries.LogMeggage(sURL, $"At {startTime} UTC, the request {sURL} was made. {retValue.Count()} items were returned", parentCompanyID);
            return retValue;
        }

        internal void ImportDeals(string parentCompanyId, string remoteDealershipId, DateTime? sDate)
        {
            var itemsFound = int.MaxValue;
            var skip = 0;
            while (itemsFound >= 0)
            {
                var deals = sDate.HasValue ?
                    GetDataFromLightspeed<LS_Deal>(LightspeedSource.DealDetail, remoteDealershipId, parentCompanyId, "lastmodifieddate", CompareMethod.GreaterThanOrEqual, skip, FormatDate(sDate.Value)) :
                    GetDataFromLightspeed<LS_Deal>(LightspeedSource.DealDetail, remoteDealershipId, parentCompanyId, "lastmodifieddate", CompareMethod.GreaterThanOrEqual, skip);

                if (!deals.Any()) return;
                itemsFound = deals.Count();
                skip += itemsFound;
                var customers = ImportCustomers(parentCompanyId, remoteDealershipId, !sDate.HasValue, deals.Select(x => x.CustId));

                var existingitems = ProcessItems(remoteDealershipId, deals);
                ProcessDealSubTables(remoteDealershipId, deals, existingitems.Item1);
                DbQueries.SetRowsAsUnprocessed(existingitems.Item1.Select(x => x.RowID), deals.First().TableName);
            }
        }

        private void ProcessDealSubTables(string remoteDealershipId, IEnumerable<LS_Deal> deals, IEnumerable<ExistingItem> existingitems)
        {
            foreach (var item in deals)
            {
                var match = existingitems.First(xx => xx.MatchingNumerical == item.ItemNumber).RowID;
                SetParentRow<BaseLightspeedClass>(match, item.Trade, item.Units, item.DealExtraLines, item.DealProspect);
            }
            var results = ProcessItems(remoteDealershipId, deals.SelectMany(x => x.Trade), "Deal");
            DbQueries.DeleteOrphans("ID", results.Item3, results.Item2);
            results = ProcessItems(remoteDealershipId, deals.SelectMany(x => x.DealExtraLines), "Deal");
            DbQueries.DeleteOrphans("ID", results.Item3, results.Item2);
            results = ProcessItems(remoteDealershipId, deals.SelectMany(x => x.DealProspect), "Deal");
            DbQueries.DeleteOrphans("ID", results.Item3, results.Item2);
            var existingUnits = ProcessItems(remoteDealershipId, deals.SelectMany(x => x.Units), "Deal");
            foreach (var item in deals.SelectMany(x => x.Units))
            {
                var match = existingUnits.Item1.First(xx => xx.MatchingNumerical == item.ItemNumber).RowID;
                SetParentRow<BaseLightspeedClass>(match, item.Parts);
            }
            results = ProcessItems(remoteDealershipId, deals.SelectMany(x => x.Units).SelectMany(x => x.Parts), "Deal_Unit");
            DbQueries.DeleteOrphans("Deal_Unit", "Deal_Unit_ID", existingUnits.Item2); //remove sub items from being deleted
            DbQueries.DeleteOrphans("ID", existingUnits.Item3, existingUnits.Item2);
            DbQueries.DeleteOrphans("ID", results.Item3, results.Item2);
        }

        internal static void SetParentRow<T>(long rowId, params IEnumerable<T>[] items) where T: BaseLightspeedClass
        {
            foreach(var item in items)
            foreach (var subIem in item ?? new List<T>()) subIem.ParentRowId = rowId;
        }

        internal void ImportCustomers(string parentCompanyId, string remoteDealershipId, DateTime? sDate)
        {
            var itemsFound = int.MaxValue;
            var skip = 0;
            while (itemsFound >= 0)
            {
                var customers = sDate.HasValue ?
                GetDataFromLightspeed<LightspeedClasses.Customer>(LightspeedSource.Customer, remoteDealershipId, parentCompanyId, "DateGathered", CompareMethod.GreaterThanOrEqual, skip, FormatDate(sDate.Value)) :
                GetDataFromLightspeed<LightspeedClasses.Customer>(LightspeedSource.Customer, remoteDealershipId, parentCompanyId, "DateGathered", CompareMethod.GreaterThanOrEqual, skip);
                if (customers?.Any() != true) return;
                itemsFound = customers.Count();
                skip += itemsFound;
                ProcessItems(remoteDealershipId, customers);
            }
        }

        internal IEnumerable<ExistingItem> ImportCustomers(string parentCompanyId, string remoteDealershipId, bool newItemsOnly, IEnumerable<string> CustomerIDs = null)
        {
            CustomerIDs = (CustomerIDs ?? Enumerable.Empty<string>()).Where(x => x.IsNotNullOrWhiteSpace() && long.TryParse(x, out long tryLong) && tryLong != 0);
            if (!CustomerIDs.Any()) return Enumerable.Empty<ExistingItem>();
            if (newItemsOnly)
            {
                var known = DbQueries.GetMatchingItems(CustomerIDs.Select(x => new Customer { CustomerId = long.Parse(x) }), remoteDealershipId, out List<ExistingItem> ei);
                CustomerIDs = CustomerIDs.Where(x => !ei.Any(xx => xx.MatchingNumerical.Value == long.Parse(x)));
                if (!CustomerIDs.Any()) return Enumerable.Empty<ExistingItem>();
            }
            var customers = GetItemsThreaded<LightspeedClasses.Customer>(parentCompanyId, remoteDealershipId, LightspeedSource.Customer, CustomerIDs, "CustomerId");
            return ProcessItems(remoteDealershipId, customers.Where(x=> x.ItemNumber.HasValue)).Item1;
        }

        private Tuple<IEnumerable<ExistingItem>, IEnumerable<ExistingItem>, string> ProcessItems<T>(string remoteDealershipId, IEnumerable<T> items, string parentTableName = null) where T : BaseLightspeedClass
        {
            if (items?.Any() != true) return Tuple.Create(Enumerable.Empty<ExistingItem>(), Enumerable.Empty<ExistingItem>(), string.Empty);
            var matchColumn = items.First().MatchingColumn;
            ErrorList.AddRange(DbQueries.GetMatchingItems(items, remoteDealershipId, out List<RawImport_Lightspeed.ExistingItem> existingitems));
            var matchedItems = new List<ExistingItem>();
            var isPrimary = items.First() is PrimaryTable;
            foreach (var item in items)
            {
                if (isPrimary) (item as PrimaryTable).RemoteDealershipID = remoteDealershipId;
                var match = existingitems.FirstOrDefault(xx => (!item.ItemNumber.HasValue || xx.MatchingNumerical == item.ItemNumber) && (item.ItemString.IsNullOrWhiteSpace() ||  xx.MatchingString == item.ItemString));
                if (match != null)
                {
                    item.ID = match.RowID;
                    matchedItems.Add(match);
                    existingitems.Remove(match);
                    if (isPrimary) (item as PrimaryTable).UpdateDateTime = DateTime.UtcNow;
                }
                else if (isPrimary) (item as PrimaryTable).InsertDateTime = DateTime.UtcNow;
            }

            ErrorList.AddRange(DbQueries.UpdateDataObjectArray(items.Where(x => x.ID > 0), remoteDealershipId, parentTableName));

            ErrorList.AddRange(DbQueries.InsertDataObjectArray(items.Where(x => x.ID == 0), remoteDealershipId, parentTableName));
            ErrorList.AddRange(DbQueries.GetMatchingItems(items.Where(x => x.ID == 0), remoteDealershipId, out List<RawImport_Lightspeed.ExistingItem> newitems));

            return Tuple.Create(matchedItems.Union(newitems), existingitems.Select(x=>x), items.First().TableName);
        }

        private IEnumerable<T> GetItemsThreaded<T>(string parentCompanyID, string remoteID, LightspeedSource reqType, IEnumerable<string> dealNumbers, string item)
        {
            var xElementChunks = new List<T>[THREADCHUNKS];
            var threads = new List<Thread>();
            var chunkSize = dealNumbers.Count() / (THREADCHUNKS - 1);
            var chunkedData = new List<List<string>>();
            for (int i = 0; i < THREADCHUNKS - 1; i++)
            {
                var batch = dealNumbers.Skip(i * chunkSize).Take(chunkSize);
                chunkedData.Add(batch.ToList());
            }

            var lastBatch = dealNumbers.Skip((THREADCHUNKS - 1) * chunkSize).ToList();

            chunkedData.Add(lastBatch);//this creates the 8th thread

            for (int i = 0; i < THREADCHUNKS; i++)
            {
                try
                {
                    var index = i;
                    var dealsInThread = chunkedData[i];
                    var thread = new Thread(() => ActionHelper.TryAndSwallow(() =>
                    {
                        var threadResult = new List<T>();
                        for (int ii = 0; ii<dealsInThread.Count; ii += 10)
                        {
                            threadResult.AddRange(GetDataFromLightspeed<T>(reqType, remoteID, parentCompanyID, item, CompareMethod.Equals, 0, dealsInThread.Skip(ii).Take(10).ToArray()));
                        }
                        xElementChunks[index] = threadResult;
                    }));

                    thread.Start();
                    threads.Add(thread);
                }
                catch (Exception e)
                {
                    var ie = e.InnerException?.ToString() ?? e.Message;
                    ErrorList.Add($"Error retrieving {item} for {remoteID}: {ie}");
                }
            }

            threads.ForEach(thread => thread.Join());
            var results = xElementChunks.Where(x => x != null).SelectMany(x => x).ToList();
            return results;
        }
    }
}
