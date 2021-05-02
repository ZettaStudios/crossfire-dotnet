using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crossfire_server.util.log.Abstracts
{
    using Interfaces;
    using Factories;

    public abstract class ASingleton<T> : ISingleton where T : ISingleton
    {
        public abstract void Initalize();
        public abstract void Destroy();

        public static T Instance
        {
            get
            {
                return SingletonFactory.GetSingleton<T>();
            }
        }
        
    }
}
