using System;
using Newtonsoft.Json;

namespace Shared.Model
{
    public class FeverData
    {
        [JsonProperty("percent")]
        private byte _percent = 96;
        [JsonProperty("progress")]
        public byte Progress;
        [JsonProperty("activated")]
        public bool Activated;
        [JsonProperty("duration")]
        public ushort Duration = 0;
        [JsonProperty("activatedAt")]
        public DateTime ActivatedAt;
        public Action Callback;
        
        public byte Percent
        {
            get
            {
                return _percent;
            }
            set
            {
                _percent = value;
                if (_percent <= 100) return;
                _percent = 0;
                Progress++;
                if (Progress > 2)
                {
                    Progress = 2;
                    _percent = 100;
                }
                switch (Progress)
                {
                    case 1:
                        _percent = 0;
                        Activated = true; 
                        ActivatedAt = DateTime.Now; 
                        Duration = 40;
                        Callback();
                        // Game.PacketHandler.SendFeverReward(Owner, 30, 0);
                        break;
                    case 2:
                        _percent = 0; 
                        Activated = true; 
                        ActivatedAt = DateTime.Now; 
                        Duration = 40; 
                        Callback();
                        // Game.PacketHandler.SendFeverReward(Owner, 50, 0);
                        break;
                    case 3:
                        _percent = 0;
                        Activated = true; 
                        ActivatedAt = DateTime.Now; 
                        Duration = 1440; 
                        Callback();
                        // Game.PacketHandler.SendFeverReward(Owner, 100, 0);
                        break;
                }
            }
        }
    }
}