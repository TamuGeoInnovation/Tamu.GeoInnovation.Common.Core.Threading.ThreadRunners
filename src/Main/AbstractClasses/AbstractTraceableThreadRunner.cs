using System.Diagnostics;

using USC.GISResearchLab.Common.Diagnostics.TraceEvents;
using USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces;

namespace USC.GISResearchLab.Common.Threading.ThreadRunners.AbstractClasses
{

    public abstract class AbstractTraceableThreadRunner : AbstractThreadRunner, ITraceableThreadRunner
    {

        #region Tracing Properties

        private TraceSource _TraceSource;
        public TraceSource TraceSource
        {
            get { return _TraceSource; }
            set { _TraceSource = value; }
        }

        #endregion

        #region Constructors

        public AbstractTraceableThreadRunner() : base()
        {
        }

        public AbstractTraceableThreadRunner(TraceSource traceSource)
            : base()
        {
            TraceSource = traceSource;
        }

        #endregion

        public override void DoStopActions()
        {
            if (TraceSource != null)
            {
                TraceSource.TraceEvent(TraceEventType.Information, (int)ProcessEvents.Cancelling, "Cancelling");
            }
        }

    }
}
