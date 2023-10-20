using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Class of spaceship and classes set of driver
    /// </summary>
    [Flags]
    public enum SpaceshipClass
    {
        None = 0b0,
        C1 = 0b1,
        C2 = 0b10,
        C3 = 0b100,
    }

    /// <summary>
    /// Space ship
    /// </summary>
    public class Spaceship
    {
        /// <summary>
        /// DB PK
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Ship name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Model name
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Spaceship class
        /// </summary>
        public SpaceshipClass Class { get; set; }
        /// <summary>
        /// Usage per sec cost
        /// </summary>
        public long UsageCost { get; set; }
        /// <summary>
        /// Max cargo volume in m^3
        /// </summary>
        public long Volume { get; set; }
        /// <summary>
        /// Current position info
        /// </summary>
        public ScheduleElement CurrentState { get; set; }
    }
}
