using System;
using System.Collections.Generic;

using crossfire_server.util.log.Enums;
using crossfire_server.util.log.EventArgs;
using crossfire_server.util.log.Interfaces;
using crossfire_server.util.log.Abstracts;


namespace crossfire_server.util.log.Factories
{
    public class LogFactory : ASingleton<LogFactory>
    {
        private Dictionary<string, ILog> _logs;
        public static event EventHandler<LogWriteEventArgs> OnWrite;

        public override void Initalize()
        {
            _logs = new Dictionary<string, ILog>();
            int width = (Console.LargestWindowWidth * 50) / 100;
            int height = (Console.LargestWindowHeight * 50) / 100;

            if (width > 0 && height > 0)
                Console.SetWindowSize(width, height);
        }

        public override void Destroy()
        {

        }

        public static ILog GetLog(string name)
        {
            if (!Instance._logs.ContainsKey(name))
            {
                ILog log = new Log(name);
                Instance._logs.Add(name, log);
            }
            return Instance._logs[name];
        }

        public static ILog GetLog(Type logType)
        {
            return GetLog(logType.Name);
        }

        public static ILog GetLog(object instance)
        {
            return GetLog(instance.GetType());
        }

        public static ILog GetLog<T>()
        {
            return GetLog(typeof(T));
        }

        private static void CallOnWrite(ILog log, string message, LogType type) 
        {
            if (OnWrite != null)
            {
                LogWriteEventArgs args = new LogWriteEventArgs(log, message, type); 
                OnWrite(log, args);
            }
        }

        private class Log : ILog
        {
            public string Name { get; private set; }
            public Log(string name)
            {
                Name = name;
            }

            public void LogInfo(string message, params object[] args)
            {
                message = string.Format(message, args);
                CallOnWrite(this, message, LogType.Information);
            }

            public void LogSuccess(string message, params object[] args)
            {
                message = string.Format(message, args);
                CallOnWrite(this, message, LogType.Success);
            }

            public void LogWarning(string message, params object[] args)
            {
                message = string.Format(message, args);
                CallOnWrite(this, message, LogType.Warning);
            }

            public void LogError(string message, params object[] args)
            {
                message = string.Format(message, args);
                CallOnWrite(this, message, LogType.Error);
            }

            public void LogFatal(Exception e)
            {
                string message = string.Format("Name: {1}{0}Message: {2}{0}Stack trace:{3}", Environment.NewLine, e.GetType().Name, e.Message, e.StackTrace);
                CallOnWrite(this, message, LogType.Fatal);
            }
        }
    }
}
