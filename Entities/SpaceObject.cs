using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// What actually the space object is
    /// </summary>
    public enum SpaceObjectKind
    {
        Galaxy = 0,
        Star = 1,
        Planet = 2,
        Other = 3,
    }

    /// <summary>
    /// Describes a space object like star, planet, etc.
    /// </summary>
    public class SpaceObject
    {
        /// <summary>
        /// DB PK
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Interspace name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// About
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Object kind
        /// </summary>
        public SpaceObjectKind Kind { get; set; }
        /// <summary>
        /// The System Center to which the object belongs, 
        /// NULL if this object is a galaxy
        /// </summary>
        public SpaceObject? SystemCenter { get; set; }
        /// <summary>
        /// The position in the system (1-indexed).
        /// Interspace Object Index if this is a star or galaxy.
        /// Position of this object's orbit from the system center otherwise.
        /// </summary>
        public long SystemPosition { get; set; }
    }
}
