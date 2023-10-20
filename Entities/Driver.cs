using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// How best the driver actually drives
    /// </summary>
    public enum DriverQualificationClass
    {
        D = 0,
        C = 1,
        B = 2,
        A = 3,
        S = 4
    }

    /// <summary>
    /// Space ship driver
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// DB PK
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Their name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Current assigned qualification
        /// </summary>
        public DriverQualificationClass QualificationClass { get; set; }
        /// <summary>
        /// Current classes of spaceships they are allowed to drive
        /// </summary>
        public SpaceshipClass SpaceshipClasses { get; set; }
        /// <summary>
        /// Hiring cost per sec
        /// </summary>
        public long HiringCost { get; set; }
        /// <summary>
        /// Current position info
        /// </summary>
        public ScheduleElement CurrentState { get; set; }
    }
}
