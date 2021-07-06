using System;
using System.Threading;
using Shared.Util.Log;
using Shared.Util.Log.Factories;

namespace Game
{
    internal class Loader
    {
        static void Main(string[] args)
        {
            LogFactory.OnWrite += Logger.LogFactory_ConsoleWrite;
            try
            {
                LogFactory.GetLog("GameServer:Loader").LogInfo("Trying to connect into Rest API to get server settings..");
                GameServer server = new GameServer(args);
                if (server.LoadedSettings)
                {
                    LogFactory.GetLog("GameServer:Loader").LogInfo("Starting the game server with collected settings..");
                    server.Start();
                }
                else
                {
                    LogFactory.GetLog("GameServer:Loader").LogError("Not could connect to the Rest API to start Game Server.");
                }
            }
            catch (Exception e)
            {
                LogFactory.GetLog("GameServer:Loader").LogError(e.Message);
            }
        }
    }
}