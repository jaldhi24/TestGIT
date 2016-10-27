using DMS.Entities.Entities;
using DMS.Model.UnitOfWork;
using DMS.Services.BusinessServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.BusinessServices
{
    /// <summary>
    /// User Skills Service
    /// </summary>
    public class UserSkillsService : IUserSkillsService
    {
        /// <summary>
        /// The _unit of work
        /// </summary>
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSkillsService"/> class.
        /// </summary>
        public UserSkillsService()
        {
            this._unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Gets the user skills.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>UserSkillEntity</returns>
        public IEnumerable<UserSkillEntity> GetUserSkills(int userId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("UserId", userId);

            var query = "EXEC [ShowuserSkillDetails] @UserId";
            var skillsOfUser = _unitOfWork.SQLQuery<UserSkillEntity>(query, param).ToList();
            return skillsOfUser;
        }

        /// <summary>
        /// Gets the skill by technology.
        /// </summary>
        /// <param name="techIds">The tech ids.</param>
        /// <returns>User Skill Entity</returns>
        public IEnumerable<SkillDetailsEntity> GetSkillByTechnology(string techIds)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("TechID", (object)techIds ?? DBNull.Value);

            var query = "EXEC [GetSkillsByTechIds] @TechID";
            var skillsOfUser = _unitOfWork.SQLQuery<SkillDetailsEntity>(query, param).ToList();
            return skillsOfUser;
        }

        /// <summary>
        /// Inserts the update skill.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="techId">The tech identifier.</param>
        /// <param name="skillsId">The skills identifier.</param>
        public void InsertUpdateSkill(int userId, string techId, string skillsId)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("UserId", userId);
            param[0] = new SqlParameter("TechIds", techId);
            param[0] = new SqlParameter("SkillIds", skillsId);

            var query = "EXEC [InsertUpdateUserSkills] @UserId @TechIds @SkillIds";
            var insertSkill = _unitOfWork.SQLQuery<UserSkillEntity>(query, param).ToList();
        }
    }
}
