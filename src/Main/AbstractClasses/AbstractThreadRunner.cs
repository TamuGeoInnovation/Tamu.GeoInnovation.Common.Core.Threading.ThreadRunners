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

        private string _RunnerName;
        public string RunnerName
        {
            get { return _RunnerName; }
            set { _RunnerName = value; }
        }

        private DateTime _Created;
        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }

        private Guid _Guid;
        public Guid Guid
        {
            get { return _Guid; }
            set { _Guid = value; }
        }

        private int _ProcessId;
        public int ProcessId
        {
            get { return _ProcessId; }
            set { _ProcessId = value; }
        }

        private object _Arguments;
        public object Arguments
        {
            get { return _Arguments; }
            set { _Arguments = value; }
        }

        private int _RecordsTotal;
        public int RecordsTotal
        {
            get { return _RecordsTotal; }
            set { _RecordsTotal = value; }
        }

        private int _RecordsCompleted;
        public int RecordsCompleted
        {
            get { return _RecordsCompleted; }
            set { _RecordsCompleted = value; }
        }

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
