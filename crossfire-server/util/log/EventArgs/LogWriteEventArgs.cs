using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crossfire_server.util.log.EventArgs
{
    using Interfaces;
    using Enums;

    public class LogWriteEventArgs : System.EventArgs
    {
        public ILog Log { get; private set; }
        public string Name { get { return Log.Name; } }
        public string Message { get; private set; }
        public LogType Type { get; private set; }

        public LogWriteEventArgs(ILog Log, string Message, LogType Type)
        {
            this.Log = Log;
            this.Message = Message;
            this.Type = Type;
        }
    }
}
