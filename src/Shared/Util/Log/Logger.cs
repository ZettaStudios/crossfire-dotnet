using System;
using Shared.Util.Log.Enums;
using Shared.Util.Log.EventArgs;

namespace Shared.Util.Log {
    public class Logger {
        public static void LogFactory_ConsoleWrite(object sender, LogWriteEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[{0}] ", DateTime.Now);

            switch (e.Type)
            {
                case LogType.Information:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case LogType.Success:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogType.Fatal:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.Write("<{0}>", e.Name);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(": {0}", e.Message);
        }
    }
}