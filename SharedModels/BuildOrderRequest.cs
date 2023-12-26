using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class BuildOrderRequest
    {
        public string LoadingPortName { get; set; }
        public string UnloadingPortName { get; set; }
        public long LoadingTime { get; set; }
        public long UnloadingTime { get; set; }
        public long Volume { get; set; }
        public int Customer { get; set; }
    }
}
