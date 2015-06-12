using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces
{
    public interface IBackgroundWorkableStatusReportableThreadRunner : IBackgroundWorkableThreadRunner, IStatusReportableThreadRunner
    {

    }
}
