using Login.Enum;
using Login.Network.packet;
using Shared.Scheduler;

namespace Login.Task
{
    public class KickInactiveSession : Shared.Scheduler.Task
    {
        private int _inactive = 0;
        private int _limit = 60;
        private Shared.Session.Session _session;
        public KickInactiveSession(Shared.Session.Session session, Scheduler scheduler) : base(scheduler)
        {
            _session = session;
        }

        public override void OnRun()
        {
            if (!_session.IsRunning)
            {
                Scheduler.RemoveTask(TaskId);
            } else if (_inactive > _limit)
            {
                _session.SendPacket(new LoginErrorResponsePacket { Error = ErrorsType.ConnectionExpired });
                Scheduler.RemoveTask(TaskId);
            }
            else
            {
                _inactive++;
            }
        }

        public int Inactive
        {
            get => _inactive;
            set => _inactive = value;
        }

        public int Limit
        {
            get => _limit;
            set => _limit = value;
        }
    }
}