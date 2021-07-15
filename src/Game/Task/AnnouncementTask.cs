using Game.Network.packet;
using Game.Session;
using Shared;
using Shared.Constants;
using Shared.Scheduler;
using Shared.Util.Log.Factories;

namespace Game.Task
{
    public class AnnouncementTask : Shared.Scheduler.Task
    {
        private int _seconds = 8;
        private int _current = 0;
        private int _actual = 0;

        private string[] _messages = new[] { 
            "You are playing with %players players on %server!", 
            "Zetta Project - Join us our discord server!",
            "Very nice! You need only %expNeed exp to reach next level!"
        };
        
        public AnnouncementTask(Scheduler scheduler) : base(scheduler)
        {
        }

        public override void OnRun()
        {
            if (_current >= _seconds)
            {
                if (_actual >= _messages.Length)
                {
                    _actual = 0;
                }
                else
                {
                    _actual++;
                }
                int count = Scheduler.Server.Sessions.Count;
                if (count > 0)
                {
                    string message = _messages[_actual]
                        .Replace("%players", count.ToString())
                        .Replace("%server", Scheduler.Server.Name);
                    foreach (GameSession session in Scheduler.Server.Sessions)
                    {
                        AnnouncementPacket packet = new AnnouncementPacket { Message = message };
                        packet.Message = packet.Message.Replace("%expNeed", (RankExperience.GetExperienceFor(session.User.Rank) - session.User.Experience).ToString());
                        session.SendPacket(packet);
                    }
                    LogFactory.GetLog(Scheduler.Server.Name).LogError($"[ANNOUNCEMENT SEND] [TOTAL: {count}] [MSG:{message}]");
                }
                _current = 0;
            }
            else
            {
                _current++;
            }
        }
    }
}