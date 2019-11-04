using System;

using USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces;

namespace USC.GISResearchLab.Common.Threading.ThreadRunners.AbstractClasses
{

    public abstract class AbstractStatusReportableThreadRunner : AbstractThreadRunner, IStatusReportableThreadRunner
    {

        #region Properties

        #endregion

        #region Constructors

        public AbstractStatusReportableThreadRunner()
            : base() { }


        #endregion

        public abstract void UpdateProcessingStatusNumberCompleted();
        public abstract void UpdateProcessingStatusNumberCompleted(int numberCompleted);
        public abstract void UpdateProcessingStatus(int numberTotal, int numberCompleted);
        public abstract void UpdateProcessingStatus(int numberTotal, int numberCompleted, object[] data);
        public abstract void UpdateProcessingStatusFinished();
        public abstract void UpdateProcessingStatusFinished(object result);
        public abstract void UpdateProcessingStatusAborted(Exception e);

        public override void DoStopActions()
        {

        }
    }
}
