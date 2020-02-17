using System;

using USC.GISResearchLab.Common.Threading.ThreadRunners.Interfaces;

namespace USC.GISResearchLab.Common.Threading.ThreadRunners.AbstractClasses
{

    public abstract class AbstractThreadRunner : IThreadRunner
    {

        #region Properties

        private volatile bool _ShouldStopFlag;
        public bool ShouldStopFlag
        {
            get { return _ShouldStopFlag; }
            set { _ShouldStopFlag = value; }
        }

        public virtual bool ShouldStop
        {
            get { return ShouldStopFlag; }
        }

        public string RunnerName { get; set; }
        public DateTime Created { get; set; }
        public Guid Guid { get; set; }
        public int ProcessId { get; set; }
        public object Arguments { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsCompleted { get; set; }
        public bool AbortOnError { get; set; }
        public bool AbortOnRepeatedErrors { get; set; }
        public bool IsAborting { get; set; }

        #endregion

        #region ErrorCount Properties

        private int _ErrorCountBeforeAbort;
        public int ErrorCountBeforeAbort
        {
            get { return _ErrorCountBeforeAbort; }
            set { _ErrorCountBeforeAbort = value; }
        }

        private int _ErrorCount;
        public int ErrorCount
        {
            get { return _ErrorCount; }
            set { _ErrorCount = value; }
        }

        private int _RepeatedErrorCount;
        public int RepeatedErrorCount
        {
            get { return _RepeatedErrorCount; }
            set { _RepeatedErrorCount = value; }
        }

        private string _PrevErrMsg;
        public string PrevErrMsg
        {
            get { return _PrevErrMsg; }
            set { _PrevErrMsg = value; }
        }

        #endregion

        #region Constructors

        public AbstractThreadRunner()
        {
            Created = DateTime.Now;
            RecordsCompleted = -1;
            RecordsTotal = -1;
            PrevErrMsg = String.Empty;
        }
        #endregion

        public abstract void Run();
        public abstract void DoStopActions();

        public void RequestStop()
        {
            ShouldStopFlag = true;
        }

    }
}
