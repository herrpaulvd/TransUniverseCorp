using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Our loved client
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// DB PK
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Company or person name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Commercial address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// The interspace efir mail address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Whether this is a company
        /// </summary>
        public bool IsCorporative { get; set; }
    }
}
