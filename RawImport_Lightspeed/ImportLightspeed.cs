using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RawImport_Lightspeed
{
    public partial class ImportLightspeed : ServiceBase
    {
        private bool stopping;
        private ManualResetEvent stoppedEvent;
        public ImportLightspeed(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        public ImportLightspeed()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Queue the main service function for execution in a worker thread.
            ThreadPool.QueueUserWorkItem(new WaitCallback(ServiceWorkerThread));
        }

        protected override void OnStop()
        {
            this.stopping = true;
            this.stoppedEvent.WaitOne();
        }
        private void ServiceWorkerThread(object state)
        {
            // Periodically check if the service is stopping.
            while (!this.stopping)
            {
                var sleepTime = ServiceWork(new List<string>());

                Thread.Sleep(sleepTime);  //Wait up to an hour for next scheduled run
            }
            this.stoppedEvent.Set();
        }

        public int ServiceWork(List<string> ParentCompanyIDs, DateTime? sDateOverride = null)
        {
            try
            {
                //new dbQueries().fillFieldMappingsCUTX();
                var oLS = new LightspeedCalls();
                var RemoteDealershipIDs = DbQueries.GetRemoteIDs(ParentCompanyIDs);
                using (var db = new DataContext(DbQueries.IntegrationDB))
                {
                    var dlo = new DataLoadOptions();
                    dlo.LoadWith<MasterDMSImport>(x => x.DMSImports);
                    dlo.LoadWith<MasterDMSImport>(x => x.CompanyIntegration);
                    dlo.LoadWith<DMSImport>(x => x.DMSImportType);
                    db.LoadOptions = dlo;
                    var integrations = from item in db.GetTable<MasterDMSImport>()
                                       where item.CompanyIntegration.IntegrationType == "Lightspeed" &&
                                               (item.RunImport && (!ParentCompanyIDs.Any() && (!item.NextImportDate.HasValue || item.NextImportDate.Value < DateTime.UtcNow))) ||
                                               ParentCompanyIDs.Contains(item.ParentCompanyId)
                                       select item;
                    foreach (var remoteID in integrations)
                    {
                        var importStart = DateTime.UtcNow;
                        bool anyImport = false;
                        foreach (var importType in remoteID.DMSImports.Where(x => x.RunImport && (ParentCompanyIDs.Any() || !x.NextImportDate.HasValue || x.NextImportDate.Value < DateTime.UtcNow)))
                        {
                            try
                            {
                                var existingErrors = oLS.ErrorList.Count;
                                if (remoteID.LastImportDate.HasValue) remoteID.LastImportDate = remoteID.LastImportDate.Value.AddDays(-1);
                                var interval = importType.ImportInterval ?? 1;
                                DateTime? sDate = sDateOverride ??
                                                    importType.LastImportDate?.AddDays(-1) ??
                                                    importType.EarliestImportDate ?? remoteID.EarliestImportDate;// ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                                switch (importType.DMSImportType.Name)
                                {
                                    #region Customers
                                    case "Customer":
                                        oLS.ImportCustomers(remoteID.ParentCompanyId, remoteID.CompanyIntegration.RemoteDealershipId, sDate);
                                        break;
                                    #endregion
                                    #region Deals
                                    case "Deal":
                                        oLS.ImportDeals(remoteID.ParentCompanyId, remoteID.CompanyIntegration.RemoteDealershipId, sDate);
                                        break;
                                    #endregion
                                }
                                #region update next run time
                                if (oLS.ErrorList.Count == existingErrors)
                                {
                                    importType.LastImportDate = DateTime.UtcNow;
                                    anyImport = true;
                                }
                                if (!importType.NextImportDate.HasValue)
                                {
                                    importType.NextImportDate = DateTime.UtcNow.AddHours(interval);
                                }
                                while (importType.NextImportDate <= DateTime.UtcNow.AddMinutes(15))
                                {
                                    importType.NextImportDate =
                                    importType.NextImportDate.Value.AddHours(interval);
                                }
                                #endregion
                                db.SubmitChanges();
                            }
                            catch (Exception e)
                            {
                                oLS.ErrorList.Add($"Error importing {importType.DMSImportType.Name}: {OptionSoft.Core.ExceptionHelper.FormatException(e)}");
                            }
                        }
                        if (anyImport)
                        {
                            if (remoteID.NextImportDate.HasValue)
                            {
                                while (remoteID.NextImportDate.Value < DateTime.UtcNow.AddMinutes(15)) remoteID.NextImportDate = remoteID.NextImportDate.Value.AddHours(remoteID.ImportInterval ?? 1);
                            }
                            else
                            {
                                remoteID.NextImportDate = DateTime.UtcNow.AddHours(remoteID.ImportInterval ?? 1);
                            }
                            if (!oLS.ErrorList.Any()) remoteID.LastImportDate = importStart;
                            db.SubmitChanges();
                        }
                    }
                }
                return DbQueries.GetWaitTime();
            }
            catch (Exception e)
            {
                try
                {
                    BuildAndSendEmail(new List<string> { $"Exception Occured while processing: {e?.Message ?? string.Empty} : {e?.InnerException?.Message ?? string.Empty}" });
                }
                catch (Exception)
                {
                }
                return 30000;
            }
        }

        public static void BuildAndSendEmail(List<string> errorList)
        {
            StringBuilder message = new StringBuilder();
            var server = "Unknown location";
            try { server = DbQueries.GetAppSetting("ServerIdentifier"); } catch { }
            message.Append($"RawImport_Lightspeed running on {server} Encountered the following errors while importing: ");
            foreach (var item in errorList)
            {
                message.Append(item + "<br/><br/>");
            }
            EmailHandler.SendEmail("RawImport_Lightspeed Problems", message.ToString());
        }
    }
}
