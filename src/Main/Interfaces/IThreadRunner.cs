using System;

namespace USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces
{
    public interface IThreadRunner
    {

        #region RunnerProperties
        string RunnerName
        {
            get;
            set;
        }

        object Arguments
        {
            get;
            set;
        }

        #endregion

        #region Thread Properties
        DateTime Created
        {
            get;
            set;
        }

        Guid Guid
        {
            get;
            set;
        }

        bool ShouldStop
        {
            get;
        }

        #endregion

        #region Diagnostic Properties
        int ErrorCount
        {
            get;
            set;
        }
        #endregion


        void Run();
        void RequestStop();
        void DoStopActions();
    }
}
