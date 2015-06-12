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
