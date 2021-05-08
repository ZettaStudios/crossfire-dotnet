using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using crossfire_server.enums;
using crossfire_server.model;
using crossfire_server.server;
using crossfire_server.util.log.Factories;

namespace crossfire_server
{
    internal class Loader
    {
        private Thread _thread;
        private ArrayList _servers = new ArrayList();

        public Loader(string[] args) {
            _servers.Add(new LoginServer(args));
            _servers.Add(new RookieServer(args));
        }
        
        public void Start() {
            foreach (Server server in _servers)
            {
                server.Start();
                Thread.Sleep(100);
            }
            _thread = new Thread(Run);
            _thread.Start();
        }

        public void StopAll()
        {
            foreach (Server server in _servers)
            {
                server.Stop();
            }
            _thread.Interrupt();
        }

        private void Run()
        {
            try
            {
                while (true)
                {
                    string args = System.Console.ReadLine();
                    if (args != null && args.ToLower().Equals("info"))
                    {
                        foreach (Server server in _servers)
                        {
                            server.GetServerInfo();
                        }
                    } else if (args != null && args.ToLower().Equals("stop-all"))
                    {
                        StopAll();
                    }
                }
            }
            catch (IOException e) {
                LogFactory.GetLog("Main").LogFatal(e);
            }
        }

        public List<GameServerInfo> GetAllGameServerInfo()
        {
            List<GameServerInfo> result = new List<GameServerInfo>();
            foreach (Server server in _servers)
            {
                if (server.Type.Equals(ServerType.Game))
                {
                    result.Add(((GameServer)server).Info);
                }
            }
            return result;
        }
        
        public static void Main(string[] args)
        {
            LogFactory.OnWrite += util.log.Logger.LogFactory_ConsoleWrite;
            try
            {
                Loader loader = new Loader(args);
                loader.Start();
            }
            catch (Exception e)
            {
                LogFactory.GetLog("Loader").LogError(e.Message);
            }
        }
    }
}