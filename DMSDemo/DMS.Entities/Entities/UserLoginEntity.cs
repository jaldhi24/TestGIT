using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.Entities
{
    /// <summary>
    /// User Login Entity
    /// </summary>
    public class UserLoginEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string UserName { get; set; }
    }
}
