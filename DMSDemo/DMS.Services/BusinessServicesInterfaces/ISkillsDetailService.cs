using DMS.Entities.Entities;
using DMS.Model.DMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.BusinessServicesInterfaces
{
    /// <summary>
    /// Skills Detail Service Interface
    /// </summary>
    public interface ISkillsDetailService
    {
        /// <summary>
        /// Gets all skills.
        /// </summary>
        /// <returns>SkillDetailsEntity</returns>
        IEnumerable<SkillDetailsEntity> GetAllSkills();

        /// <summary>
        /// Deletes the selected skill.
        /// </summary>
        /// <param name="skillId">The skill identifier.</param>
        /// <returns>boolean</returns>
        bool DeleteSelectedSkill(int skillId);

        /// <summary>
        /// Saves the skills.
        /// </summary>
        /// <param name="technologyId">The technology identifier.</param>
        /// <param name="skillNames">The skill names.</param>
        /// <returns>string</returns>
        string SaveSkills(int technologyId, string skillNames);
    }
}
