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

	public abstract class AbstractTraceableBackgroundWorkableWebStatusReportableThreadRunner : AbstractTraceableBackgroundWorkableStatusReportableThreadRunner, ITraceableBackgroundWorkableWebStatusReportableThreadRunner
	{

		#region Web Status Database Properties

		private DatabaseType _WebStatusDatabaseType;
		public DatabaseType WebStatusDatabaseType
		{
			get { return _WebStatusDatabaseType; }
			set { _WebStatusDatabaseType = value; }
		}

		private DataProviderType _WebStatusDataProviderType;
		public DataProviderType WebStatusDataProviderType
		{
			get { return _WebStatusDataProviderType; }
			set { _WebStatusDataProviderType = value; }
		}

		private string _WebStatusTable;
		public string WebStatusTable
		{
			get { return _WebStatusTable; }
			set { _WebStatusTable = value; }
		}


		private string _WebStatusConnectionString;
		public string WebStatusConnectionString
		{
			get { return _WebStatusConnectionString; }
			set { _WebStatusConnectionString = value; }
		}

		public QueryManager WebStatusQueryManager
		{
			get { return new QueryManager(WebStatusDataProviderType, WebStatusDatabaseType, WebStatusConnectionString); }
		}

		#endregion

		#region Notification Email Properties



		#endregion

        #region Constructors
		public AbstractTraceableBackgroundWorkableWebStatusReportableThreadRunner()
			: base()
		{
		}

		public AbstractTraceableBackgroundWorkableWebStatusReportableThreadRunner(DataProviderType webStatusDataProviderStatus, DatabaseType webStatusDatabaseType, string webStatusConnectionString)
			: base()
		{
			WebStatusConnectionString = webStatusConnectionString;
			WebStatusDatabaseType = WebStatusDatabaseType;
			WebStatusDataProviderType = WebStatusDataProviderType;
		}

		public AbstractTraceableBackgroundWorkableWebStatusReportableThreadRunner(TraceSource traceSource)
			: base(traceSource) { }

		public AbstractTraceableBackgroundWorkableWebStatusReportableThreadRunner(BackgroundWorker backgroundWorker)
			: base(backgroundWorker) { }

		public AbstractTraceableBackgroundWorkableWebStatusReportableThreadRunner(BackgroundWorker backgroundWorker, TraceSource traceSource)
			: base(backgroundWorker, traceSource) { }

		public AbstractTraceableBackgroundWorkableWebStatusReportableThreadRunner(BackgroundWorker backgroundWorker, DataProviderType webStatusDataProviderStatus, DatabaseType webStatusDatabaseType, string webStatusConnectionString)
			: base(backgroundWorker)
		{
			WebStatusConnectionString = webStatusConnectionString;
			WebStatusDatabaseType = WebStatusDatabaseType;
			WebStatusDataProviderType = WebStatusDataProviderType;
		}

		public AbstractTraceableBackgroundWorkableWebStatusReportableThreadRunner(BackgroundWorker backgroundWorker, TraceSource traceSource, DataProviderType webStatusDataProviderStatus, DatabaseType webStatusDatabaseType, string webStatusConnectionString)
			: base(backgroundWorker, traceSource)
		{
			WebStatusConnectionString = webStatusConnectionString;
			WebStatusDatabaseType = WebStatusDatabaseType;
			WebStatusDataProviderType = WebStatusDataProviderType;
		}

		#endregion

		public override void UpdateProcessingStatusNumberCompleted()
		{
            UpdateProcessingStatusNumberCompleted(RecordsCompleted + 1);
		}

		public override void UpdateProcessingStatusNumberCompleted(int numberCompleted)
		{
			try
			{
				base.UpdateProcessingStatusNumberCompleted(numberCompleted);

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
			catch (ThreadAbortException te)
			{
				throw te;
			}
			catch (Exception e)
			{
				throw new Exception("Error occured UpdateProcessingStatusNumberCompleted: " + e.Message, e);
			}
		}

        public override void UpdateProcessingStatus(int numberTotal, int numberCompleted)
        {
            UpdateProcessingStatus(numberTotal, numberCompleted, null);
        }

		public override void UpdateProcessingStatus(int numberTotal, int numberCompleted, object[] data)
		{
			try
			{
				base.UpdateProcessingStatus(numberTotal, numberCompleted, data);

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
			catch (ThreadAbortException te)
			{
				throw te;
			}
			catch (Exception e)
			{
				throw new Exception("Error occured UpdateProcessingStatus: " + e.Message, e);
			}
		}


		public override void UpdateProcessingStatusFinished()
		{
			try
			{
				base.UpdateProcessingStatusFinished();

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
					sql += "  recordsCompleted=@recordsCompleted ";
					sql += " WHERE ";
					sql += " processGUID=@processGUID ";

					IQueryManager qm = WebStatusQueryManager;
					qm.CreateParameters(5);
					qm.AddParameters(0, "@timeUpdated", DateTime.Now);
					qm.AddParameters(1, "@resultStatus", "Completed Successfully");
					qm.AddParameters(2, "@recordsTotal", RecordsTotal);
					qm.AddParameters(3, "@recordsCompleted", RecordsTotal);
					qm.AddParameters(4, "@processGUID", Guid.ToString());

					qm.ExecuteNonQuery(CommandType.Text, sql, true);
				}
			}
			catch (ThreadAbortException te)
			{
				throw te;
			}
			catch (Exception e)
			{
				throw new Exception("Error occured UpdateProcessingStatusFinished: " + e.Message, e);
			}
		}

		public override void UpdateProcessingStatusAborted(Exception e)
		{

			try
			{
				base.UpdateProcessingStatusAborted(e);

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
					qm.AddParameters(1, "@" + "resultStatus", e.Message);
					qm.AddParameters(2, "@" + "processGUID", Guid.ToString());

					qm.ExecuteNonQuery(CommandType.Text, sql, true);
				}
			}
			catch (ThreadAbortException te)
			{
				throw te;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured UpdateProcessingStatusAborted: " + ex.Message, ex);
			}
		}

		public virtual void UpdateProcessingStatusException(Exception e)
		{
			UpdateProcessingStatusAborted(e);
		}

		public virtual void UpdateProcessingStatusThreadAbortException(ThreadAbortException e)
		{
			try
			{
				if (TraceSource != null)
				{
					TraceSource.TraceEvent(TraceEventType.Error, (int)ExceptionEvents.ExceptionOccurred, e.Message);
				}

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
					qm.AddParameters(1, "@" + "resultStatus", e.Message);
					qm.AddParameters(2, "@" + "processGUID", Guid.ToString());

					qm.ExecuteNonQuery(CommandType.Text, sql, true);
				}
			}
			catch (ThreadAbortException te)
			{
				throw te;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured UpdateProcessingStatusThreadAbortException: " + ex.Message, ex);
			}
		}
	}
}