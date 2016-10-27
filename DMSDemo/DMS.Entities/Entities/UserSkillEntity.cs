using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.Entities
{
    /// <summary>
    /// User Skill Entity
    /// </summary>
    public class UserSkillEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the technology identifier.
        /// </summary>
        /// <value>
        /// The technology identifier.
        /// </value>
        public string TechnologyId { get; set; }

        /// <summary>
        /// Gets or sets the skill identifier.
        /// </summary>
        /// <value>
        /// The skill identifier.
        /// </value>
        public string SkillId { get; set; }

        /// <summary>
        /// Gets or sets the technology identifier.
        /// </summary>
        /// <value>
        /// The technology identifier.
        /// </value>
        public int TechID { get; set; }

        /// <summary>
        /// Gets or sets the name of the technology.
        /// </summary>
        /// <value>
        /// The name of the technology.
        /// </value>
        public string TechName { get; set; }

        /// <summary>
        /// Gets or sets the name of the skill.
        /// </summary>
        /// <value>
        /// The name of the skill.
        /// </value>
        public string SkillName { get; set; }
    }
}
