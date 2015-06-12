using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;
using System.Text;
using System.Threading;

using USC.GISResearchLab.Common.Utils.Databases;
using USC.GISResearchLab.Common.Diagnostics.TraceEvents;
using USC.GISResearchLab.Common.Threading;
using USC.GISResearchLab.Common.Utils.Strings;
using USC.GISResearchLab.Common.Databases.QueryManagers;
using USC.GISResearchLab.Common.Threading.ProgressStates;
using USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces;
using USC.GISResearchLab.Common.Core.Databases;

namespace USC.GISResearchLab.Common.Threading.ThreadRunners.AbstractClasses
{

    public abstract class AbstractBackgroundWorkableWebStatusReportableThreadRunner : AbstractBackgroundWorkableThreadRunner, IBackgroundWorkableStatusReportableThreadRunner, IWebStatusReportableThreadRunner
    {

        #region Web Status Database Properties

        private DatabaseType _WebStatusDatabaseType;
        public DatabaseType WebStatusDatabaseType
        {
            get { return _WebStatusDatabaseType; }
            set { _WebStatusDatabaseType = value; }
        }

        private DataProviderType _WebStatusDataProviderStatus;
        public DataProviderType WebStatusDataProviderStatus
        {
            get { return _WebStatusDataProviderStatus; }
            set { _WebStatusDataProviderStatus = value; }
        }

        private string _WebStatusConnectionString;
        public string WebStatusConnectionString
        {
            get { return _WebStatusConnectionString; }
            set { _WebStatusConnectionString = value; }
        }

        private string _WebStatusTable;
        public string WebStatusTable
        {
            get { return _WebStatusTable; }
            set { _WebStatusTable = value; }
        }


        public QueryManager WebStatusQueryManager
        {
            get { return new QueryManager(WebStatusDataProviderStatus, WebStatusDatabaseType, WebStatusConnectionString); }
        }

        #endregion

        #region Constructors

        public AbstractBackgroundWorkableWebStatusReportableThreadRunner()
            : base()
        {
        }

        public AbstractBackgroundWorkableWebStatusReportableThreadRunner(DataProviderType webStatusDataProviderStatus, DatabaseType webStatusDatabaseType, string webStatusConnectionString)
            : base()
        {
            WebStatusConnectionString = webStatusConnectionString;
            WebStatusDatabaseType = WebStatusDatabaseType;
            WebStatusDataProviderStatus = WebStatusDataProviderStatus;
        }

        public AbstractBackgroundWorkableWebStatusReportableThreadRunner(BackgroundWorker backgroundWorker)
            : base(backgroundWorker)
        {
        }

        public AbstractBackgroundWorkableWebStatusReportableThreadRunner(BackgroundWorker backgroundWorker, DataProviderType webStatusDataProviderStatus, DatabaseType webStatusDatabaseType, string webStatusConnectionString)
            : base(backgroundWorker)
        {
            WebStatusConnectionString = webStatusConnectionString;
            WebStatusDatabaseType = WebStatusDatabaseType;
            WebStatusDataProviderStatus = WebStatusDataProviderStatus;
        }

        #endregion


        public virtual void UpdateProcessingStatusNumberCompleted()
        {
            UpdateProcessingStatusNumberCompleted(RecordsCompleted + 1);
        }

        public virtual void UpdateProcessingStatusNumberCompleted(int numberCompleted)
        {
            RecordsCompleted = numberCompleted;

            if (BackgroundWorker != null)
            {
                ProgressState.Completed = numberCompleted;
                BackgroundWorker.ReportProgress(Convert.ToInt32(ProgressState.PercentCompleted), ProgressState);
            }

            if (!String.IsNullOrEmpty(WebStatusConnectionString) && !String.IsNullOrEmpty(WebStatusTable))
            {
                string sql = "";
                sql += " update ";
                sql += WebStatusTable;
                sql += " set ";
                sql += "  recordsCompleted=@recordsCompleted, ";
                sql += "  timeUpdated=@timeUpdated WHERE ";
                sql += "  processGUID=@processGUID";

                IQueryManager qm = WebStatusQueryManager;
                qm.CreateParameters(3);
                qm.AddParameters(0, "@" + "recordsCompleted", numberCompleted);
                qm.AddParameters(1, "@" + "timeUpdated", DateTime.Now);
                qm.AddParameters(2, "@" + "processGUID", Guid.ToString());
                qm.ExecuteNonQuery(CommandType.Text, sql, true);
            }
        }

        public virtual void UpdateProcessingStatus(int numberTotal, int numberCompleted)
        {
            UpdateProcessingStatus(numberTotal, numberCompleted, null);
        }

        public virtual void UpdateProcessingStatus(int numberTotal, int numberCompleted, object[] data)
        {
            RecordsTotal = numberTotal;
            RecordsCompleted = numberCompleted;

            if (BackgroundWorker != null)
            {
                PercentCompletableProgressState progressState = new PercentCompletableProgressState();
                progressState.Total = numberTotal;
                progressState.Completed = numberCompleted;

                if (data != null)
                {
                    progressState.Data = data;
                }

                BackgroundWorker.ReportProgress(Convert.ToInt32(progressState.PercentCompleted), progressState);
            }

            if (!String.IsNullOrEmpty(WebStatusConnectionString) && !String.IsNullOrEmpty(WebStatusTable))
            {
                string sql = "";
                sql += " update ";
                sql += WebStatusTable;
                sql += " set ";
                sql += "  recordsTotal=@recordsTotal, ";
                sql += "  recordsCompleted=@recordsCompleted, ";
                sql += "  timeUpdated=@timeUpdated WHERE ";
                sql += "  processGUID=@processGUID";

                IQueryManager qm = WebStatusQueryManager;
                qm.CreateParameters(4);
                qm.AddParameters(0, "@" + "recordsTotal", numberTotal);
                qm.AddParameters(1, "@" + "recordsCompleted", numberCompleted);
                qm.AddParameters(2, "@" + "timeUpdated", DateTime.Now);
                qm.AddParameters(3, "@" + "processGUID", Guid.ToString());
                qm.ExecuteNonQuery(CommandType.Text, sql, true);
            }
        }

        public virtual void UpdateProcessingStatusFinished()
        {
            UpdateProcessingStatusFinished(null);
        }

        public virtual void UpdateProcessingStatusFinished(object result)
        {
            if (BackgroundWorker != null)
            {
                ProgressState.Total = RecordsTotal;
                ProgressState.Completed = RecordsTotal;
                BackgroundWorker.ReportProgress(Convert.ToInt32(ProgressState.PercentCompleted), ProgressState);

                if (DoWorkEventArgs != null)
                {
                    DoWorkEventArgs.Result = result;
                }
            }

            if (!String.IsNullOrEmpty(WebStatusConnectionString) && !String.IsNullOrEmpty(WebStatusTable))
            {
                string sql = "";
                sql += " update ";
                sql += WebStatusTable;
                sql += " set ";
                sql += "  ProcessStatusId=5, ";
                sql += "  timeUpdated=@timeUpdated, ";
                sql += "  resultStatus=@resultStatus, ";
                sql += "  recordsTotal=@recordsTotal, ";
                sql += "  recordsCompleted=@recordsCompleted, ";
                sql += " WHERE ";
                sql += " processGUID=@processGUID ";

                IQueryManager qm = WebStatusQueryManager;
                qm.CreateParameters(5);
                qm.AddParameters(0, "@" + "timeUpdated", DateTime.Now);
                qm.AddParameters(1, "@" + "resultStatus", "Completed Successfully");
                qm.AddParameters(2, "@" + "recordsTotal", RecordsTotal);
                qm.AddParameters(3, "@" + "recordsCompleted", RecordsTotal);
                qm.AddParameters(4, "@" + "processGUID", Guid.ToString());

                qm.ExecuteNonQuery(CommandType.Text, sql, true);
            }
        }

        public virtual void UpdateProcessingStatusAborted(Exception e)
        {

            IsAborting = true;

            if (BackgroundWorker != null)
            {
                ProgressState.Error = true;
                ProgressState.Exception = e;
                ProgressState.Message = e.Message;
                BackgroundWorker.ReportProgress(Convert.ToInt32(ProgressState.PercentCompleted), ProgressState);
            }

            if (!String.IsNullOrEmpty(WebStatusConnectionString) && !String.IsNullOrEmpty(WebStatusTable))
            {
                string sql = "";
                sql += " update ";
                sql += WebStatusTable;
                sql += " set ";
                sql += "  ProcessStatusId=13, ";
                sql += "  timeUpdated=@timeUpdated, ";
                sql += "  resultStatus=@resultStatus ";
                sql += " WHERE ";
                sql += " processGUID=@processGUID ";

                IQueryManager qm = WebStatusQueryManager;
                qm.CreateParameters(3);
                qm.AddParameters(0, "@" + "timeUpdated", DateTime.Now);
                qm.AddParameters(1, "@" + "resultStatus", "Completed Successfully");
                qm.AddParameters(2, "@" + "processGUID", Guid.ToString());

                qm.ExecuteNonQuery(CommandType.Text, sql, true);
            }
        }

        public virtual void UpdateProcessingStatusException(Exception e)
        {
            UpdateProcessingStatusAborted(e);
        }

        public virtual void UpdateProcessingStatusThreadAbortException(ThreadAbortException e)
        {
            if (BackgroundWorker != null)
            {
                ProgressState.Error = true;
                ProgressState.Exception = e;
                ProgressState.Message = e.Message;
                BackgroundWorker.ReportProgress(Convert.ToInt32(ProgressState.PercentCompleted), ProgressState);
            }

            if (!String.IsNullOrEmpty(WebStatusConnectionString) && !String.IsNullOrEmpty(WebStatusTable))
            {
                string sql = "";
                sql += " update ";
                sql += WebStatusTable;
                sql += " set ";
                sql += "  ProcessStatusId=15, ";
                sql += "  timeUpdated=@timeUpdated, ";
                sql += "  resultStatus=@resultStatus ";
                sql += " WHERE ";
                sql += " processGUID=@processGUID ";

                IQueryManager qm = WebStatusQueryManager;
                qm.CreateParameters(3);
                qm.AddParameters(0, "@" + "timeUpdated", DateTime.Now);
                qm.AddParameters(1, "@" + "resultStatus", "Completed Successfully");
                qm.AddParameters(2, "@" + "processGUID", Guid.ToString());

                qm.ExecuteNonQuery(CommandType.Text, sql, true);
            }
        }
    }
}
