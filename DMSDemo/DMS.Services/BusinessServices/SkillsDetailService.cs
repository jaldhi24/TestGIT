using DMS.Entities.Entities;
using DMS.Model.UnitOfWork;
using DMS.Services.BusinessServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DMS.Services.BusinessServices
{
    /// <summary>
    /// Skills Detail Service
    /// </summary>
    public class SkillsDetailService : ISkillsDetailService
    {
        /// <summary>
        /// The _unit of work
        /// </summary>
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillsDetailService"/> class.
        /// </summary>
        public SkillsDetailService()
        {
            this._unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Gets all skills.
        /// </summary>
        /// <returns>SkillDetailsEntity</returns>
        public IEnumerable<SkillDetailsEntity> GetAllSkills()
        {
            var queryStr = "EXEC [SkillsList]";
            var skillsTechnology = _unitOfWork.SQLQuery<SkillDetailsEntity>(queryStr).ToList();
            return skillsTechnology;
        }

        /// <summary>
        /// Deletes the selected skill.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>boolean</returns>
        public bool DeleteSelectedSkill(int id)
        {
            bool deleteStatus = false;
            using (var scope = new TransactionScope())
            {
                ////var deleteRecord = _unitOfWork.SkillDetailRepository.GetByID(skillId);
                //// var deleteRecord = _unitOfWork.SkillDetailRepository.GetSingle(m => m.Id == skillId);
                ////if (deleteRecord != null)
                ////{
                _unitOfWork.SkillDetailRepository.Delete(id);
                _unitOfWork.Save();
                scope.Complete();
                deleteStatus = true;
                ////}
            }

            return deleteStatus;
        }

        /// <summary>
        /// Saves the skills.
        /// </summary>
        /// <param name="technologyId">The technology identifier.</param>
        /// <param name="skillNames">The skill names.</param>
        /// <returns>string</returns>
        public string SaveSkills(int technologyId, string skillNames)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("TechId", technologyId);
            param[1] = new SqlParameter("SkillNames", skillNames);

            var queryStr = "EXEC [InsertUpdateSkills] @TechId, @SkillNames";
            _unitOfWork.SQLQuery<SkillDetailsEntity>(queryStr, param).ToList();
            return queryStr;
        }
    }
}
