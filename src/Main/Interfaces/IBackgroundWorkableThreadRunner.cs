using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
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
