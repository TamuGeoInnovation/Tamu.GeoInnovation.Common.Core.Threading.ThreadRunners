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

namespace USC.GISResearchLab.Common.Threading.ThreadRunners.AbstractClasses
{

    public abstract class AbstractBackgroundWorkableThreadRunner : AbstractThreadRunner, IBackgroundWorkableThreadRunner
    {


        #region Should Stop Properties
        public new bool ShouldStop
        {
            get
            {
                bool ret = ShouldStopFlag;
                if (BackgroundWorker != null)
                {
                    ret = ret || BackgroundWorker.CancellationPending;
                }
                return ret;
            }
        }

        #endregion

        #region Background Worker Properties
        private BackgroundWorker _BackgroundWorker;
        public BackgroundWorker BackgroundWorker
        {
            get { return _BackgroundWorker; }
            set { _BackgroundWorker = value; }
        }

        private DoWorkEventArgs _DoWorkEventArgs;
        public DoWorkEventArgs DoWorkEventArgs
        {
            get { return _DoWorkEventArgs; }
            set { _DoWorkEventArgs = value; }
        }

        private PercentCompletableProgressState _ProgressState;
        public PercentCompletableProgressState ProgressState
        {
            get { return _ProgressState; }
            set { _ProgressState = value; }
        }
        #endregion

        #region Constructors

        public AbstractBackgroundWorkableThreadRunner()
            : base()
        {
        }

        public AbstractBackgroundWorkableThreadRunner(BackgroundWorker backgroundWorker)
            : base()
        {
            BackgroundWorker = backgroundWorker;
        }

        #endregion


        public void Run(DoWorkEventArgs e)
        {
            DoWorkEventArgs = e;
            Arguments = e.Argument;
            Run();
        }

        public override void DoStopActions()
        {
            if (DoWorkEventArgs != null)
            {
                DoWorkEventArgs.Cancel = true;
            }
        }
    }
}
