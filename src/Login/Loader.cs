using System;
using System.Linq;
using System.Threading;
using Shared.Util.Log;
using Shared.Util.Log.Factories;

namespace Login
{
    internal class Loader
    {
        private static Thread _commandPreProcessor;
        static void Main(string[] args)
        {
            LogFactory.OnWrite += Logger.LogFactory_ConsoleWrite;
            try
            {
                LoginServer server = new LoginServer(args);
                server.Start();
                _commandPreProcessor = new Thread(() =>
                {
                    while (server.IsAlive)
                    {
                        string[] args = Console.ReadLine()?.Split(" ");
                        if (args != null && args.Length > 0)
                        {
                            string command = args[0];
                            args = args[1..];
                            switch (command.ToLower())
                            {
                                case "info":
                                    server.GetServerInfo();
                                    break;
                                case "stop":
                                    for (int i = 3; i > 0; i--)
                                    {
                                        LogFactory.GetLog("Login Server").LogInfo($"Closing server in {i} seconds...");
                                        Thread.Sleep(1000);
                                    }
                                    server.Stop();
                                    break;
                                default:
                                    LogFactory.GetLog("Login Server").LogWarning($"The command '{command}' doesn't exists.");
                                    break;
                            }
                        }
                    }
                });
                _commandPreProcessor.Start();
            }
            catch (Exception e)
            {
                LogFactory.GetLog("Loader").LogError(e.Message);
            }
        }
    }
}