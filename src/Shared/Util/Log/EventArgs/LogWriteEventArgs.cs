using Shared.Util.Log.Enums;
using Shared.Util.Log.Interfaces;

namespace Shared.Util.Log.EventArgs {
    public class LogWriteEventArgs : System.EventArgs {
        public LogWriteEventArgs(ILog Log, string Message, LogType Type) {
            this.Log = Log;
            this.Message = Message;
            this.Type = Type;
        }

        public ILog Log { get; }
        public string Name => Log.Name;
        public string Message { get; }
        public LogType Type { get; }
    }
}