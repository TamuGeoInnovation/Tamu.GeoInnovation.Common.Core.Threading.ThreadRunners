using System;
using System.Threading;

namespace USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces
{
    public interface IWebStatusReportableThreadRunner : IStatusReportableThreadRunner
    {
        void UpdateProcessingStatusException(Exception e);
        void UpdateProcessingStatusThreadAbortException(ThreadAbortException e);
    }
}
