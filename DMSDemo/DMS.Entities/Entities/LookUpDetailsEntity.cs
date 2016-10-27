using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.Entities
{
    /// <summary>
    /// Look Up Details Entity
    /// </summary>
    public class LookUpDetailsEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the lookup master identifier.
        /// </summary>
        /// <value>
        /// The lookup master identifier.
        /// </value>
        public int? LookupMasterId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LookUpDetailsEntity"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
