using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crossfire_server.util.log.Factories
{
    using Interfaces;

    public class SingletonFactory
    {
        private static object syncLock = new object();
        private static Dictionary<string, ISingleton> Singletons = new Dictionary<string, ISingleton>();

        public static ISingleton GetSingleton(Type SingletonType)
        {
            if (typeof(ISingleton).IsAssignableFrom(SingletonType))
            {
                string ID = SingletonType.FullName;
                if (!Singletons.ContainsKey(ID))
                {
                    ISingleton Singleton = (ISingleton)Activator.CreateInstance(SingletonType);
                    Singleton.Initalize();

                    Singletons.Add(ID, Singleton);
                }
                return Singletons[ID];
            }
            throw new NotSupportedException(string.Format("{0} type not implements ISingleton interface!", SingletonType.Name));
        }

        public static T GetSingleton<T>() where T : ISingleton
        {
            return (T)GetSingleton(typeof(T));
        }
    }
}
