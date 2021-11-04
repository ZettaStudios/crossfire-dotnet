using System;
using System.Diagnostics;

namespace Shared.Task
{
    public class ServerInformationToConsoleTask : Scheduler.Task
    {
        private string _pattern = "%serverName | Online %online/%maxOnline | Memory %mem @ %threads threads.";
        public ServerInformationToConsoleTask(Scheduler.Scheduler scheduler) : base(scheduler)
        {
        }

        public override void OnRun()
        {
            int threads = Process.GetCurrentProcess().Threads.Count;
            var memory = 0.0;
            using (Process proc = Process.GetCurrentProcess())
            {
                // The proc.PrivateMemorySize64 will returns the private memory usage in byte.
                // Would like to Convert it to Megabyte? divide it by 2^20
                memory = Math.Round((double) (proc.PrivateMemorySize64 / (1024*1024)), 2);
            }
            string information = _pattern
                .Replace("%serverName", Scheduler.Server.Name)
                .Replace("%online", Scheduler.Server.Sessions.Count.ToString())
                .Replace("%maxOnline", Scheduler.Server.MaxConnections.ToString())
                .Replace("%mem", memory.ToString("0.0"))
                .Replace("%threads", threads.ToString());
            Console.Title = information;
        }
    }
}