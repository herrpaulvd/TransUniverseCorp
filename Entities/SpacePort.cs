using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Describes a space port
    /// </summary>
    public class SpacePort
    {
        /// <summary>
        /// DB PK
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Port name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// About
        /// </summary>
        public string Description { get; set; }

        // location:
        /// <summary>
        /// The planet where it is located
        /// </summary>
        public SpaceObject Planet { get; set; }
        /// <summary>
        /// Longtitude coordinate in sec
        /// </summary>
        public float Longtitude { get; set; }
        /// <summary>
        /// Latitude coordinate in sec
        /// </summary>
        public float Latitude { get; set; }
        /// <summary>
        /// Altitude in m
        /// </summary>
        public float Altitude { get; set; }
    }
}
