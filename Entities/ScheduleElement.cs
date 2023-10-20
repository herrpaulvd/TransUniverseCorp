using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Current delivery schedule element
    /// </summary>
    public class ScheduleElement
    {
        /// <summary>
        /// DB PK
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Departure if it is a stop, arrival if fly
        /// </summary>
        public DateTime DepartureOrArrival { get; set; }
        /// <summary>
        /// Planned departure or arrival
        /// </summary>
        public DateTime PlannedDepartureOrArrival { get; set; }
        /// <summary>
        /// Current order or NULL if driver flies to loading place
        /// </summary>
        public Order? Order { get; set; }
        /// <summary>
        /// Current spaceship or NULL if driver flies to the current spaceship location
        /// </summary>
        public Spaceship? Spaceship { get; set; }
        /// <summary>
        /// Current driver
        /// </summary>
        public Driver Driver { get; set; }
        /// <summary>
        /// Destination port if fly, otherwise stop port
        /// </summary>
        public SpacePort DestinationOrStop { get; set; }
        /// <summary>
        /// Whether it is a stop (otherwise fly)
        /// </summary>
        public bool IsStop { get; set; }
        /// <summary>
        /// Stop or fly time
        /// </summary>
        public TimeSpan Time { get; set; }
    }
}
