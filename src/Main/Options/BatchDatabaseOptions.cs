using System;
using USC.GISResearchLab.Common.Core.Databases;

namespace USC.GISResearchLab.Common.Databases.Runners.Options
{
    [Serializable]
    public class BatchDatabaseOptions
    {
        #region Properties

        private string _Table;
        public string Table
        {
            get { return _Table; }
            set { _Table = value; }
        }

        private string _FieldId;
        public string FieldId
        {
            get { return _FieldId; }
            set { _FieldId = value; }
        }

        private string _FieldTransactionId;
        public string FieldTransactionId
        {
            get { return _FieldTransactionId; }
            set { _FieldTransactionId = value; }
        }

        private string _FieldProcessed;
        public string FieldProcessed
        {
            get { return _FieldProcessed; }
            set { _FieldProcessed = value; }
        }

        private string _FieldSource;
        public string FieldSource
        {
            get { return _FieldSource; }
            set { _FieldSource = value; }
        }

        private string _FieldTimeTaken;
        public string FieldTimeTaken
        {
            get { return _FieldTimeTaken; }
            set { _FieldTimeTaken = value; }
        }

        private string _DatabaseLocation;
        public string DatabaseLocation
        {
            get { return _DatabaseLocation; }
            set { _DatabaseLocation = value; }
        }

        private string _DatabaseName;
        public string DatabaseName
        {
            get { return _DatabaseName; }
            set { _DatabaseName = value; }
        }

        private string _DatabaseExtension;
        public string DatabaseExtension
        {
            get { return _DatabaseExtension; }
            set { _DatabaseExtension = value; }
        }

        private string _DatabaseConnectionString;
        public string DatabaseConnectionString
        {
            get { return _DatabaseConnectionString; }
            set { _DatabaseConnectionString = value; }
        }

        private bool _NonProcessedOnly;
        public bool NonProcessedOnly
        {
            get { return _NonProcessedOnly; }
            set { _NonProcessedOnly = value; }
        }

        private bool _AbortOnError;
        public bool AbortOnError
        {
            get { return _AbortOnError; }
            set { _AbortOnError = value; }
        }

        private bool _ShouldLeaveDatabaseConnectionOpen;
        public bool ShouldLeaveDatabaseConnectionOpen
        {
            get { return _ShouldLeaveDatabaseConnectionOpen; }
            set { _ShouldLeaveDatabaseConnectionOpen = value; }
        }

        public bool ShouldNotStoreTransactionDetails { get; set; }

        private bool _NotifyOnCompletion;
        public bool NotifyOnCompletion
        {
            get { return _NotifyOnCompletion; }
            set { _NotifyOnCompletion = value; }
        }

        private string _NotificationEmail;
        public string NotificationEmail
        {
            get { return _NotificationEmail; }
            set { _NotificationEmail = value; }
        }

        private string _NotificationEmailConfiguration;
        public string NotificationEmailConfiguration
        {
            get { return _NotificationEmailConfiguration; }
            set { _NotificationEmailConfiguration = value; }
        }

        private DataProviderType _DataProviderType;
        public DataProviderType DataProviderType
        {
            get { return _DataProviderType; }
            set { _DataProviderType = value; }
        }

        private DatabaseType _DatabaseType;
        public DatabaseType DatabaseType
        {
            get { return _DatabaseType; }
            set { _DatabaseType = value; }
        }
        #endregion
    }
}
