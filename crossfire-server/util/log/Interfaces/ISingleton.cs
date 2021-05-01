using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crossfire_server.util.log.Interfaces
{
    public interface ISingleton
    {
        void Initalize();
        void Destroy();
    }
}
