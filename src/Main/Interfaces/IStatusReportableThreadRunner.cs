using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using USC.GISResearchLab.Common.Threading.ProgressStates;
using System.Diagnostics;
using USC.GISResearchLab.Common.Databases.QueryManagers;
using System.Threading;

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
