using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class ShortOrderModel
    {
        public string DriverName { get; set; }
        public string SpaceshipName { get; set; }
        public long Cost { get; set; }
        public int Index { get; set; }
        public long ID { get; set; }
        public bool Ready { get; set; }
        public bool Discardable { get; set; }
        public string Error { get; set; }
    }
}
