using System;
using System.ComponentModel;

using USC.GISResearchLab.Common.Threading.ProgressStates;
using USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces;


namespace USC.GISResearchLab.Common.Threading.ThreadRunners.AbstractClasses
{

    public abstract class AbstractBackgroundWorkableStatusReportableThreadRunner : AbstractBackgroundWorkableThreadRunner, IBackgroundWorkableStatusReportableThreadRunner
    {
        #region Constructors

        public AbstractBackgroundWorkableStatusReportableThreadRunner()
            : base()
        {
        }

        public AbstractBackgroundWorkableStatusReportableThreadRunner(BackgroundWorker backgroundWorker)
            : base(backgroundWorker)
        {
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
        }
    }
}
