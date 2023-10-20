using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// An account
    /// </summary>
    public class User
    {
        /// <summary>
        /// DB PK
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Account login
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Account password hash
        /// </summary>
        public long PasswordHash { get; set; }
        /// <summary>
        /// Customer info. NULL if they ain't a customer (e.g. admin or driver)
        /// </summary>
        public Customer? Customer { get; set; }
        /// <summary>
        /// Driver info. NULL if they ain't a driver (e.g. admin or customer)
        /// </summary>
        public Driver? Driver { get; set; }
    }
}
