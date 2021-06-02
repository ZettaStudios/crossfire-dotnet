namespace Shared.Scheduler
{
    public abstract class Task
    {
        
        private readonly Scheduler _scheduler;
        private readonly long _taskId;
        
        private int _delay = -1;
        private int _maxDelay = -1;
        private bool _repeat = false;

        protected Task(Scheduler scheduler)
        {
            _scheduler = scheduler;
            _taskId = Scheduler.TaskId++;
        }

        public abstract void OnRun();

        public void Cancel()
        {
            Scheduler.RemoveTask(TaskId);
        }

        public Scheduler Scheduler => _scheduler;

        public long TaskId => _taskId;

        public int Delay
        {
            get => _delay;
            set => _delay = value;
        }

        public int MaxDelay
        {
            get => _maxDelay;
            set => _maxDelay = value;
        }

        public bool Repeat
        {
            get => _repeat;
            set => _repeat = value;
        }
    }
}