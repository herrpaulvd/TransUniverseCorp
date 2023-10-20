using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Possible route part (non-bidirected) between two space objects
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// DB PK
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Start object
        /// </summary>
        public SpaceObject Start { get; set; }
        /// <summary>
        /// End object
        /// </summary>
        public SpaceObject End { get; set; }
        /// <summary>
        /// Time in Earth sec
        /// </summary>
        public TimeSpan Time { get; set; }
        /// <summary>
        /// Required spaceship classes
        /// </summary>
        public SpaceshipClass RequiredSpaceshipClasses { get; set; }
        /// <summary>
        /// Required minimum qualification class
        /// </summary>
        public DriverQualificationClass RequiredMinQualificationClass { get; set; }
    }
}
