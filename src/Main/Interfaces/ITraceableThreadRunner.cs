using System.Diagnostics;


namespace USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces
{
    public interface ITraceableThreadRunner : IThreadRunner
    {

        #region Tracing Properties
        TraceSource TraceSource
        {
            get;
            set;
        }
        #endregion

    }
}
