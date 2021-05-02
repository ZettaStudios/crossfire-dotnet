using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using crossfire_server.util.log.Enums;
using crossfire_server.util.log.EventArgs;
using crossfire_server.util.log.Interfaces;
using crossfire_server.util.log.Abstracts;


namespace crossfire_server.util.log.Factories
{


    public class LogFactory : ASingleton<LogFactory>
    {
        private Dictionary<string, ILog> Logs;
        public static event EventHandler<LogWriteEventArgs> OnWrite;

        public override void Initalize()
        {
            this.Logs = new Dictionary<string, ILog>();
            int Width = (Console.LargestWindowWidth * 50) / 100;
            int Height = (Console.LargestWindowHeight * 50) / 100;

            if (Width > 0 && Height > 0)
                Console.SetWindowSize(Width, Height);
        }

        public override void Destroy()
        {

        }

        public static ILog GetLog(string Name)
        {
            if (!Instance.Logs.ContainsKey(Name))
            {
                ILog Log = new Log(Name);
                Instance.Logs.Add(Name, Log);
            }
             return Instance.Logs[Name];
        }

        public static ILog GetLog(Type LogType)
        {
            return GetLog(LogType.Name);
        }

        public static ILog GetLog(object Instance)
        {
            return GetLog(Instance.GetType());
        }

        public static ILog GetLog<T>()
        {
            return GetLog(typeof(T));
        }

        private static void CallOnWrite(ILog Log, string Message, LogType Type) 
        {
            if (OnWrite != null)
            {
                LogWriteEventArgs Args = new LogWriteEventArgs(Log, Message, Type); 
                OnWrite(Log, Args);
            }
        }

        private class Log : ILog
        {
            public string Name { get; private set; }
            public Log(string Name)
            {
                this.Name = Name;
            }

            public void LogInfo(string Message, params object[] Args)
            {
                Message = string.Format(Message, Args);
                CallOnWrite(this, Message, LogType.Information);
            }

            public void LogSuccess(string Message, params object[] Args)
            {
                Message = string.Format(Message, Args);
                CallOnWrite(this, Message, LogType.Success);
            }

            public void LogWarning(string Message, params object[] Args)
            {
                Message = string.Format(Message, Args);
                CallOnWrite(this, Message, LogType.Warning);
            }

            public void LogError(string Message, params object[] Args)
            {
                Message = string.Format(Message, Args);
                CallOnWrite(this, Message, LogType.Error);
            }

            public void LogFatal(Exception e)
            {
                string Message = string.Format("Name: {1}{0}Message: {2}{0}Stack trace:{3}", Environment.NewLine, e.GetType().Name, e.Message, e.StackTrace);
                CallOnWrite(this, Message, LogType.Fatal);
            }
        }
    }
}
