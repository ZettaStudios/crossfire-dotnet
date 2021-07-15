using System;
using System.Collections.Generic;
using System.Threading;
using Shared.Util.Log.Factories;

namespace Shared.Scheduler
{
    public class Scheduler
    {
        private Server _server;
        public static long TaskId = -1;
        private Dictionary<long, Task> _queue = new Dictionary<long, Task>();
        private Thread _thread;

        public Scheduler(Server server)
        {
            _server = server;
        }

        public void Start()
        {
            _thread = new Thread(() =>
            {
                while (_server.IsAlive)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        Run();
                    }
                    catch (Exception e)
                    {
                        LogFactory.GetLog(_server.Name).LogError(e.Message);
                    }
                }
            });
            _thread.Start();
        }

        private void Run()
        {
            foreach (var keyValuePair in _queue)
            {
                Task task = keyValuePair.Value;
                task.Delay--;
                if (task.Repeat && task.Delay <= 0)
                {
                    task.OnRun();
                    task.Delay = task.MaxDelay;
                } else if (!task.Repeat && task.Delay <= 0)
                {
                    task.OnRun();
                    RemoveTask(keyValuePair.Key);
                }
            }
        }

        public void AddTask(Task task, int delay = 0, bool repeat = false)
        {
            task.Delay = delay;
            task.MaxDelay = delay;
            task.Repeat = repeat;
            _queue.Add(task.TaskId, task);
        }

        public void RemoveTask(long id)
        {
            _queue.Remove(id);
        }

        public void CallAfter(Action callback, int milliseconds)
        {
            System.Threading.Tasks.Task.Delay(milliseconds).ContinueWith(t =>
            {
                callback();
            });
        }

        public Server Server => _server;
    }
}