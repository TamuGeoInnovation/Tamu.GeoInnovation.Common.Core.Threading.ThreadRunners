using System;

namespace USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces
{
    public interface IStatusReportableThreadRunner : IThreadRunner
    {
        void UpdateProcessingStatusNumberCompleted();
        void UpdateProcessingStatusNumberCompleted(int numberCompleted);
        void UpdateProcessingStatus(int numberTotal, int numberCompleted);
        void UpdateProcessingStatus(int numberTotal, int numberCompleted, object[] data);
        void UpdateProcessingStatusFinished();
        void UpdateProcessingStatusFinished(object result);
        void UpdateProcessingStatusAborted(Exception e);
    }
}
