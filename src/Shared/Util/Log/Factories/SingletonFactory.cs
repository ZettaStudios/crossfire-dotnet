using System;
using System.Collections.Generic;
using Shared.Util.Log.Interfaces;

namespace Shared.Util.Log.Factories {
    public class SingletonFactory {
        private static object syncLock = new object();
        private static readonly Dictionary<string, ISingleton> Singletons = new Dictionary<string, ISingleton>();

        public static ISingleton GetSingleton(Type SingletonType) {
            if (typeof(ISingleton).IsAssignableFrom(SingletonType)) {
                var ID = SingletonType.FullName;
                if (!Singletons.ContainsKey(ID)) {
                    var Singleton = (ISingleton) Activator.CreateInstance(SingletonType);
                    Singleton.Initalize();

                    Singletons.Add(ID, Singleton);
                }

                return Singletons[ID];
            }

            throw new NotSupportedException(string.Format("{0} type not implements ISingleton interface!",
                SingletonType.Name));
        }

        public static T GetSingleton<T>() where T : ISingleton {
            return (T) GetSingleton(typeof(T));
        }
    }
}