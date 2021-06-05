using System;
using Shared.Util.Log;
using Shared.Util.Log.Factories;

namespace Login
{
    internal class Loader
    {
        static void Main(string[] args)
        {
            LogFactory.OnWrite += Logger.LogFactory_ConsoleWrite;
            LoginServer server = new LoginServer(args);
            try
            {
                server.Start();
            }
            catch (Exception e)
            {
                LogFactory.GetLog($"{server.Name}:Loader").LogError(e.Message);
            }
        }
    }
}