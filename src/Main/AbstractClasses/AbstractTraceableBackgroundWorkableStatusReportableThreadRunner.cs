using System;
using System.ComponentModel;
using System.Diagnostics;

using USC.GISResearchLab.Common.Diagnostics.TraceEvents;
using USC.GISResearchLab.Common.Threading.ProgressStates;
using USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces;


namespace USC.GISResearchLab.Common.Threading.ThreadRunners.AbstractClasses
{

    public abstract class AbstractTraceableBackgroundWorkableStatusReportableThreadRunner : AbstractTraceableBackgroundWorkableThreadRunner, ITraceableBackgroundWorkableStatusReportableThreadRunner
    {

        public AbstractTraceableBackgroundWorkableStatusReportableThreadRunner()
            : base()
        {
        }

        public AbstractTraceableBackgroundWorkableStatusReportableThreadRunner(TraceSource traceSource)
            : base(traceSource) { }

        public AbstractTraceableBackgroundWorkableStatusReportableThreadRunner(BackgroundWorker backgroundWorker)
            : base(backgroundWorker) { }

        public AbstractTraceableBackgroundWorkableStatusReportableThreadRunner(BackgroundWorker backgroundWorker, TraceSource traceSource)
            : base(backgroundWorker, traceSource) { }


        public virtual void UpdateProcessingStatusNumberCompleted()
        {
            UpdateProcessingStatusNumberCompleted(RecordsCompleted++);
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

            if (TraceSource != null)
            {
                TraceSource.TraceEvent(TraceEventType.Information, (int)ProcessEvents.Completed);
            }
        }

        public virtual void UpdateProcessingStatusAborted(Exception e)
        {
            IsAborting = true;

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
        }
    }
}
