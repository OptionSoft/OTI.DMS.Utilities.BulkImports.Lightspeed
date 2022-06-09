using DataProtectionLib;
using OptionSoft.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawImport_Lightspeed
{
    public class IntegrationData
    {
        public string ParentCompanyID { get; set; }
        public string RemoteDealershipID { get; set; }
        public DateTime? LastImportDate { get; set; }
        public DateTime? LastInvImportDate { get; set; }
        public DateTime? NextImportDate { get; set; }
        public DateTime? EarliestImportDate { get; set; }
        public int Interval { get; set; }
    }
    public class ExistingItem
    {
        public long? MatchingNumerical { get; set; }
        public string MatchingString { get; set; }
        public long RowID { get; set; }
        public List<long> SubTableRowIDs { get; set; }
    }

    internal static class DbQueries
    {
        private static Dictionary<string, string> DecodedSettings = new Dictionary<string, string>();
        private const string DbSchema = "[LS]";
        private static string Decrypt(string encrypted)
        {
            try
            {
                var decoded = clsTripleDESCryptoServiceProvider.TripleDESDecrypt(encrypted);
                return decoded.StartsWith("Could Not Decrypt the Encrypted Data") ? encrypted : decoded;
            }
            catch
            {
                return encrypted;
            }
        }
        public static string GetAppSetting(string key)
        {
            if (!DecodedSettings.ContainsKey(key))
            {
                DecodedSettings[key] = Decrypt(ConfigurationManager.AppSettings[key]);
            }
            return DecodedSettings[key];
        }
        private static string IntegrationServer { get { return GetAppSetting("IntegrationServer"); } }
        private static string RawDbServer { get { return GetAppSetting("RawDbServer"); } }
        private static string IntegrationCatalog { get { return GetAppSetting("DMSIntegrationCatalog"); } }
        private static string RawCatalog { get { return GetAppSetting("RawCatalog"); } }
        private static string IntegrationUserID { get { return GetAppSetting("IntegrationId"); } }
        private static string RawDbUserID { get { return GetAppSetting("RawDbId"); } }
        private static string IntegrationPw { get { return GetAppSetting("IntegrationPW"); } }
        private static string RawDbPw { get { return GetAppSetting("RawDbPW"); } }
        public static string IntegrationDB { get { return $"Data Source= {IntegrationServer} ;Initial Catalog = {IntegrationCatalog}; User Id = {IntegrationUserID}; Password = {IntegrationPw}; Integrated Security = False;"; } }
        public static string RawDB => $"Data Source= {RawDbServer} ;Initial Catalog = {RawCatalog}; User Id = {RawDbUserID}; Password = {RawDbPw}; Integrated Security = False;";
        
        public static void LogMeggage(string request, string message, string parentCompanyID)
        {
            return;
            try
            {
                using (var db = new IntegrationTablesDataContext(IntegrationDB))
                {
                    db.MessageLogs.InsertOnSubmit(new MessageLog
                    {
                        MessageType = "Response",
                        ConversationId = new Random().Next(0, int.MaxValue),
                        LogTime = DateTime.Now,
                        Message = message,
                        ParentCompanyId = parentCompanyID,
                        RequestParameter = request
                    });
                    db.SubmitChanges();
                }
            }
            catch
            {

            }
        }

        public static bool IsRetryableSqlError(Exception e)
        {
            return ContainsCaseInsensitive(e.Message, "A transport-level error", "Execution Timeout Expired", "Rerun the transaction");
        }

        public static bool ContainsCaseInsensitive(string testString, params string[] values)
        {
            return values.Any(v => !string.IsNullOrWhiteSpace(v) && (testString ?? string.Empty).ToUpper().Contains(v.ToUpper()));
        }

        internal static void SetRowsAsUnprocessed(IEnumerable<long> RowIds, string tableName)
        {
            if (RowIds?.Any() != true) return;
            for(int i=0; i< RowIds.Count(); i += 500)
            {
                var rows = string.Join(",", RowIds.Skip(i).Take(500));
                string sSQL = $"UPDATE {DbSchema}.[{tableName}] SET Processed = 0 WHERE ID in ({rows})";
                ExecuteNonQuery(sSQL, RawDB);
            }
        }

        private static void ExecuteNonQuery(string sSQL, string rawDB)
        {
            var sCom = new SqlCommand(sSQL, new SqlConnection(rawDB));
            try
            {
                sCom.Connection.Open();
                sCom.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                if (IsRetryableSqlError(e)) { ExecuteNonQuery(sSQL, rawDB); return; }
                throw;
            }
            finally
            {
                if (sCom.Connection.State == System.Data.ConnectionState.Open) sCom.Connection.Close();
            }
        }

        public static int GetWaitTime()
        {
            try
            {
                using (var db = new IntegrationTablesDataContext(IntegrationDB))
                {
                    var nextImport = db.MasterDMSImports
                                        .Where(x => x.RunImport && x.CompanyIntegration.IntegrationType == "Lightspeed" && x.DMSImports.Any(xx => xx.RunImport && xx.NextImportDate.HasValue)).ToList()
                                        .SelectMany(x => x.DMSImports.Where(xx => xx.RunImport && xx.NextImportDate.HasValue).Select(xx => xx.NextImportDate.Value)).Min();
                    //if overdue, don't wait at all, otherwise wait until the next scheduled import or one hour max
                    return (int)Math.Min(3600000, Math.Max(1, (nextImport - DateTime.UtcNow).TotalMilliseconds));
                }
            }
            catch (Exception e)
            {
                return 3600000;
            }
        }

        internal static void DeleteOrphans(string v, string item3, IEnumerable<ExistingItem> ItemsToMatch)
        {
            for (int i = 0; i < ItemsToMatch.Count(); i += 2000)
            {
                var rows = string.Join(",", ItemsToMatch.Skip(i).Take(2000).Select(x => x.RowID));
                var sSQL = $"DELETE FROM {DbSchema}.[{item3}] WHERE[{v}] in ({rows})";
                ExecuteNonQuery(sSQL, RawDB);
            }
        }

        public static List<IntegrationData> GetRemoteIDs(List<string> parentCompanyIDs)
        {
            return (from item in new DataContext(IntegrationDB).GetTable<MasterDMSImport>()
                    where item.CompanyIntegration.IntegrationType == "Lightspeed" && (item.RunImport && (!parentCompanyIDs.Any() && (!item.NextImportDate.HasValue || item.NextImportDate.Value < DateTime.UtcNow))) || parentCompanyIDs.Contains(item.ParentCompanyId)
                    select new IntegrationData
                    {
                        ParentCompanyID = item.ParentCompanyId,
                        RemoteDealershipID = item.CompanyIntegration.RemoteDealershipId,
                        EarliestImportDate = item.EarliestImportDate,
                        NextImportDate = item.NextImportDate,
                        LastImportDate = item.LastImportDate,
                        Interval = item.ImportInterval ?? 1,
                    }).ToList();
        }
        public static int InsertDataObject<T>(T oClass, string parentTableName, out List<string> Errors) where T : RawImport_Lightspeed.LightspeedClasses.BaseLightspeedClass
        {
            Errors = new List<string>();
            if (oClass == null) return 0;
            var sTableName = oClass.TableName;
            var ignoreFields = oClass.FieldsToIgnore.Concat(new[] { "ID", "UpdateDateTime" });
            int iReturn = 0;
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbTop = new StringBuilder();
            StringBuilder sbBottom = new StringBuilder();
            Dictionary<string, object> ParamsList = new Dictionary<string, object>();
            sbSQL.Append($"INSERT INTO {DbSchema}.[{sTableName}] (");
            foreach (var prop in oClass.GetType().GetProperties().Where(z => z.PropertyType.Name.ToLower() != "entityset`1"))
            {
                var pname = prop.Name;
                if (!ignoreFields.Contains(pname) && prop.GetValue(oClass, null) != null)
                {
                    if (pname == "ParentRowId")
                    {
                        if (parentTableName.IsNullOrWhiteSpace()) continue;
                        pname = $"{parentTableName}_Id";
                    }
                    sbTop.Append("," + pname);
                    sbBottom.Append(",@" + pname);
                    ParamsList[pname] = prop.GetValue(oClass, null);
                }
            }
            string sSQL = sbSQL.ToString() + sbTop.ToString().Remove(0, 1) + ") VALUES (" + sbBottom.ToString().Remove(0, 1) + ");SELECT SCOPE_IDENTITY();";
            SqlConnection sqlCon = new SqlConnection(RawDB);
            SqlCommand sqlCom = new SqlCommand(sSQL.ToString())
            {
                Connection = sqlCon
            };
            foreach (var key in ParamsList.Keys)
            {
                sqlCom.Parameters.AddWithValue("@" + key, ParamsList[key]);
            }
            sqlCon.Open();
            try
            {
                iReturn = Convert.ToInt32(sqlCom.ExecuteScalar());
            }
            catch (Exception e)
            {
                var ie = e.InnerException?.ToString() ?? e.Message;
                Errors.Add($"Error Adding {typeof(T).ToString()} to SQL Table {sTableName}: {ie}");
            }
            finally
            { sqlCon.Close(); }
            return iReturn;
        }
        public static List<string> InsertDataObjectArray<T>(IEnumerable<T> _oClassArray, string RemoteDealershipID, string parentTableName) where T : RawImport_Lightspeed.LightspeedClasses.BaseLightspeedClass
        {
            if (!_oClassArray.Any()) return new List<string>();
            var sTableName = _oClassArray.First().TableName;
            var paramsPerClass = _oClassArray.First().GetType().GetProperties().Count();
            int position = 0;
            var ignoreFields = _oClassArray.First().FieldsToIgnore.Concat(new[] { "ID", "UpdateDateTime" });

            while (position < _oClassArray.Count())
            {
                var oClassArray = _oClassArray.Skip(position);
                int i = 0;
                StringBuilder sbSQL = new StringBuilder();
                string topSQL = string.Empty;
                List<string> valueArray = new List<string>();
                Dictionary<string, object> ParamsList = new Dictionary<string, object>();
                sbSQL.Append($"INSERT INTO {DbSchema}.[{sTableName}] (");
                foreach (var oClass in oClassArray)
                {
                    position++;
                    StringBuilder sbTop = new StringBuilder();
                    StringBuilder sbBottom = new StringBuilder();
                    foreach (var prop in oClass.GetType().GetProperties().Where(z => !(ignoreFields.Contains(z.Name)) && z.PropertyType.Name.ToLower() != "entityset`1"))
                    {
                        var pname = prop.Name;
                        if (pname == "ParentRowId")
                        {
                            if (parentTableName.IsNullOrWhiteSpace()) continue;
                            pname = $"{parentTableName}_Id";
                        }

                        if (sbTop.Length > 0) sbTop.Append(",");
                        sbTop.Append($"[{pname}]");
                        if (prop.GetValue(oClass, null) != null)
                        {
                            sbBottom.Append(",@" + i.ToString() + pname);
                            ParamsList["@" + i.ToString() + pname] = prop.GetValue(oClass, null);
                        }
                        else sbBottom.Append(",NULL");
                    }
                    topSQL = sbTop.ToString();
                    valueArray.Add(sbBottom.ToString().Remove(0, 1));
                    i++;
                    if (2050 - paramsPerClass < ParamsList.Count) break;
                }
                string bottomSQL = string.Join("),(", valueArray);
                string sSQL = sbSQL.ToString() + topSQL + ") VALUES (" + bottomSQL + ");";
                SqlConnection sqlCon = new SqlConnection(RawDB);
                SqlCommand sqlCom = new SqlCommand(sSQL.ToString());
                foreach (var key in ParamsList.Keys)
                {
                    sqlCom.Parameters.AddWithValue(key, ParamsList[key]);
                }
                sqlCom.Connection = sqlCon;
                sqlCon.Open();
                try
                {
                    sqlCom.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    if (IsRetryableSqlError(e)) return InsertDataObjectArray(_oClassArray, RemoteDealershipID, parentTableName);
                    var ie = e.InnerException?.ToString() ?? e.Message;
                    var Errors = new List<string> { $"Error during bulk Insert of { typeof(T).ToString() } for RemoteDealershipID: {RemoteDealershipID}" };
                    foreach (var item in oClassArray)
                    {
                        InsertDataObject(item, parentTableName, out List<string> newErrors);
                        if (newErrors.Any()) Errors.AddRange(newErrors);
                    }
                    return Errors;
                }
                finally
                { sqlCon.Close(); }
            }
            return new List<string>();
        }
        public static List<string> UpdateDataObjectArray<T>(IEnumerable<T> _oClassArray, string RemoteDealershipID, string parentTableName) where T: RawImport_Lightspeed.LightspeedClasses.BaseLightspeedClass
        {
            if (!_oClassArray.Any()) return new List<string>();
            var sTableName = _oClassArray.First().TableName;
            var errors = new List<string>();
            var paramsPerClass = _oClassArray.First().GetType().GetProperties().Count();
            var sqlCommands = new List<SqlCommand>();
            SqlConnection sqlCon = new SqlConnection(RawDB);
            var ignoreFields = _oClassArray.First().FieldsToIgnore;
            int position = 0;
            while (position < _oClassArray.Count())
            {
                var oClassArray = _oClassArray.Skip(position);
                int i = 0;
                StringBuilder sbSQL = new StringBuilder();
                StringBuilder midSQL = new StringBuilder();
                string ttSQL = string.Empty;
                string setSQL = string.Empty;
                List<string> valueArray = new List<string>();
                Dictionary<string, object> ParamsList = new Dictionary<string, object>();
                sbSQL.Append("UPDATE st SET ");
                midSQL.Append($" From {DbSchema}.[{sTableName}] st JOIN ( VALUES ");
                foreach (var oClass in oClassArray)
                {
                    position++;
                    StringBuilder sbSet = new StringBuilder();
                    StringBuilder sbValues = new StringBuilder();
                    StringBuilder ttColums = new StringBuilder();
                    foreach (var prop in oClass.GetType().GetProperties().Where(z => !(new List<string> { "InsertDateTime" }.Contains(z.Name)) && z.PropertyType.Name.ToLower() != "entityset`1"))
                    {
                        var propName = prop.Name;
                        if (ignoreFields.Contains(propName)) continue;
                        if (propName == "ParentRowId")
                        {
                            if (parentTableName.IsNullOrWhiteSpace()) continue;
                            propName = $"{parentTableName}_Id";
                        }
                        if (propName != "ID")
                        {
                            if (sbSet.Length > 0) sbSet.Append(",");
                            sbSet.Append("st." + propName + " = tt." + propName);
                        }
                        if (prop.GetValue(oClass, null) != null)
                        {
                            sbValues.Append(",@" + i.ToString() + propName);
                            ParamsList["@" + i.ToString() + propName] = prop.GetValue(oClass, null);
                        }
                        else sbValues.Append(",NULL");
                        if (ttColums.Length > 0) ttColums.Append(",");
                        ttColums.Append(propName);
                    }
                    setSQL = sbSet.ToString();
                    ttSQL = ttColums.ToString();
                    valueArray.Add(sbValues.ToString().Remove(0, 1));
                    i++;
                    if (2050 - paramsPerClass < ParamsList.Count) break;
                }
                string bottomSQL = string.Join("),(", valueArray);

                string _sSQL = sbSQL.ToString() + setSQL.ToString() + midSQL.ToString() + "(" + bottomSQL + ")) tt (" + ttSQL + ") ON st.ID = tt.ID;";
                SqlCommand sSQL = new SqlCommand(_sSQL, sqlCon);

                foreach (var key in ParamsList.Keys)
                {
                    sSQL.Parameters.AddWithValue(key, ParamsList[key]);
                }
                sqlCommands.Add(sSQL);
            }
            try
            {
                sqlCon.Open();
                foreach (var sqlCom in sqlCommands)
                {
                    sqlCom.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                if (IsRetryableSqlError(e)) UpdateDataObjectArray(_oClassArray, RemoteDealershipID, parentTableName);
                var ie = e.InnerException?.ToString() ?? e.Message;
                return new List<string> { $"Error during bulk Update of { typeof(T).ToString() } for RemoteDealershipID {RemoteDealershipID}: {ie}" };
            }
            finally
            { sqlCon.Close(); }
            return new List<string>();
        }
        public static List<string> GetMatchingItems<T>(IEnumerable<T> ItemsToMatch, string RemoteDealershipID, out List<ExistingItem> matchingItems) where T: LightspeedClasses.BaseLightspeedClass
        {
            List<string> Errors = new List<string>();
            matchingItems = new List<ExistingItem>();
            if (ItemsToMatch?.Any() != true) return Errors;
            bool LongMatch = ItemsToMatch.First().ItemNumber.HasValue;
            try
            {
                string mathingColumn = ItemsToMatch.First().MatchingColumn;
                string secondaryMatch = ItemsToMatch.First().SecondaryMatch.IsNotNullOrWhiteSpace() ? $"[{ItemsToMatch.First().SecondaryMatch}]" : "NULL";
                for (int i = 0; i < ItemsToMatch.Count(); i += 2000)
                {
                    var matching = string.Join(",", ItemsToMatch.Skip(i).Take(2000).Select(x => x.ItemNumber.HasValue ? x.ItemNumber.ToString() : $"'{x.ItemString}'"));
                    StringBuilder sb = new StringBuilder($"SELECT [ID], [{mathingColumn}], {secondaryMatch} FROM {DbSchema}.[{ItemsToMatch.First().TableName}] WHERE ");
                    if(ItemsToMatch.First() is LightspeedClasses.PrimaryTable)
                    {
                        sb.Append($"RemoteDealershipID = '{RemoteDealershipID}' AND ");
                    }
                    sb.Append($"[{mathingColumn}] in ({matching}) ");

                    FillMatchingItems(matchingItems, LongMatch, sb);
                }
            }
            catch (Exception e)
            {
                var ie = e.InnerException?.ToString() ?? e.Message;
                Errors.Add($"Error retrieving existing Deals from Typed Database for RemoteDealershipID {RemoteDealershipID}: {ie}");
            }
            return Errors;
        }

        private static void FillMatchingItems(List<ExistingItem> matchingItems, bool LongMatch, StringBuilder sb)
        {
            try
            {
                var db = new SqlConnection(RawDB);
                var sql = new SqlCommand(sb.ToString(), db);
                db.Open();
                SqlDataReader sqlDR = sql.ExecuteReader();
                while (sqlDR.Read())
                {
                    matchingItems.Add(new ExistingItem
                    {
                        RowID = sqlDR.GetInt64(0),
                        MatchingNumerical = LongMatch ? sqlDR.GetInt64(1) : default,
                        MatchingString = !LongMatch ? sqlDR.GetString(1) : sqlDR.IsDBNull(2) ? null : sqlDR.GetString(2),
                    });
                }
            }
            catch(Exception e)
            {
                if (IsRetryableSqlError(e)) FillMatchingItems(matchingItems, LongMatch, sb);
                else throw;
            }
        }
    }
}
