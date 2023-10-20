using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Order to deliver a cargo
    /// </summary>
    public class Order
    {
        /// <summary>
        /// DB ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Time of cargo loading, EarthUTC
        /// </summary>
        public DateTime LoadingTime { get; set; }
        /// <summary>
        /// Port where the cargo will be/is loaded
        /// </summary>
        public SpacePort LoadingPort { get; set; }
        /// <summary>
        /// Time of cargo unloading, EarthUTC
        /// </summary>
        public DateTime UnloadingTime { get; set; }
        /// <summary>
        /// Port where the cargo will be/is unloaded
        /// </summary>
        public SpacePort UnloadingPort { get; set; }
        /// <summary>
        /// Cargo volume
        /// </summary>
        public long Volume { get; set; }
        /// <summary>
        /// Total delivery cost
        /// </summary>
        public long TotalCost { get; set; }
        /// <summary>
        /// Total delivery time
        /// </summary>
        public TimeSpan TotalTime { get; set; }
        /// <summary>
        /// Spaceship containing cargo
        /// </summary>
        public Spaceship Spaceship { get; set; }
        /// <summary>
        /// Driver of the spaceship
        /// </summary>
        public Driver Driver { get; set; }
        /// <summary>
        /// Current spaceship and driver position info
        /// </summary>
        public ScheduleElement CurrentState { get; set; }
    }
}
