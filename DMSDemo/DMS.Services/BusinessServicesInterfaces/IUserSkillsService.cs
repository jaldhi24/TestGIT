using DMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.BusinessServicesInterfaces
{
    /// <summary>
    /// Interface User Skills Service
    /// </summary>
    public interface IUserSkillsService
    {
        /// <summary>
        /// Gets the user skills.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// UserSkillEntity
        /// </returns>
        IEnumerable<UserSkillEntity> GetUserSkills(int userId);

        /// <summary>
        /// Inserts the update skill.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="techId">The tech identifier.</param>
        /// <param name="skillsId">The skills identifier.</param>
        void InsertUpdateSkill(int userId, string techId, string skillsId);
    }
}
