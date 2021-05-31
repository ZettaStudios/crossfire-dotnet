using Shared.Util.Log.Factories;
using Shared.Util.Log.Interfaces;

namespace Shared.Util.Log.Abstracts {
    public abstract class ASingleton<T> : ISingleton where T : ISingleton {
        public static T Instance => SingletonFactory.GetSingleton<T>();

        public abstract void Initalize();
        public abstract void Destroy();
    }
}