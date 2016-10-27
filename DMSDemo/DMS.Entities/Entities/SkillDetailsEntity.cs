using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.Entities
{
    /// <summary>
    /// Skill Details Entity
    /// </summary>
    public class SkillDetailsEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the lookup details identifier.
        /// </summary>
        /// <value>
        /// The lookup details identifier.
        /// </value>
        public int LookupDetailsId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SkillDetailsEntity"/> is active.
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

        /// <summary>
        /// Gets or sets the name of the technology.
        /// </summary>
        /// <value>
        /// The name of the technology.
        /// </value>
        public string TechnologyName { get; set; }

        /// <summary>
        /// Gets or sets the skills names string.
        /// </summary>
        /// <value>
        /// The skills names string.
        /// </value>
        public string SkillsNamesString { get; set; }

        /// <summary>
        /// Gets or sets the identifier of skill names.
        /// </summary>
        /// <value>
        /// The identifier of skill names.
        /// </value>
        public string IdOfSkillNames { get; set; }
    }
}
