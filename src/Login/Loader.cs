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
            try
            {
                LoginServer server = new LoginServer(args);
                server.Start();
            }
            catch (Exception e)
            {
                LogFactory.GetLog("Loader").LogError(e.Message);
            }
        }
    }
}