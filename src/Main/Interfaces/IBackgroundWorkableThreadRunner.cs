using System.ComponentModel;
using USC.GISResearchLab.Common.Threading.ProgressStates;

namespace USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces
{
    public interface IBackgroundWorkableThreadRunner : IThreadRunner
    {
        #region Background Worker Properties
        BackgroundWorker BackgroundWorker
        {
            get;
            set;
        }

        DoWorkEventArgs DoWorkEventArgs
        {
            get;
            set;
        }

        PercentCompletableProgressState ProgressState
        {
            get;
            set;
        }
        #endregion

        void Run(DoWorkEventArgs e);
    }
}
