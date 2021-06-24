using Shared.Util.Log.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shared.Config
{
    /*
     * ConfigLoader
     * Author: SkelletonX
     * Version: 0.1
     */
    class ConfigLoader
    {
        private FileInfo ConfigFile;
        public SortedList<string, string> _topics;

        public ConfigLoader(string Path)
        {
            ConfigFile = new FileInfo(Path);
            _topics = new SortedList<string, string>();
            Load();
        }

        public void Load()
        {
            StreamReader stream = new StreamReader(ConfigFile.FullName);
            while (!stream.EndOfStream)
            {
                string line = stream.ReadLine();
                if (line.Length == 0) continue;
                if (line.StartsWith(";")) continue;
                _topics.Add(line.Split('=')[0], line.Split('=')[1]);
            }
            LogFactory.GetLog("ConfigLoad").LogInfo("[Config] Loaded {0} parametrs", _topics.Count);
        }

        public string getValue(string value, string defaultprop)
        {
            string ret;
            try
            {
                ret = _topics[value];
            }
            catch
            {
                return defaultprop;
            }
            return ret == null ? defaultprop : ret;
        }
    }
}
