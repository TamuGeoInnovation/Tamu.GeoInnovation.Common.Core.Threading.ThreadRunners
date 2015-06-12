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

    public abstract class AbstractWebStatusReportableThreadRunner : AbstractStatusReportableThreadRunner, IWebStatusReportableThreadRunner
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

        public QueryManager WebStatusQueryManager
        {
            get { return new QueryManager(WebStatusDataProviderStatus, WebStatusDatabaseType, WebStatusConnectionString); }
        }


        #endregion

        #region Constructors

        public AbstractWebStatusReportableThreadRunner()
            : base() { }


        #endregion

        public abstract void UpdateProcessingStatusException(Exception e);
        public abstract void UpdateProcessingStatusThreadAbortException(ThreadAbortException e);
    }
}
