using DMS.Services.BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DMS.Controllers
{
    /// <summary>
    /// Skill Detail Controller
    /// </summary>
    public class SkillDetailController : ApiController
    {
        #region Private Variables
        /// <summary>
        /// The _document service
        /// </summary>
        private readonly SkillsDetailService _skillService;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="SkillDetailController"/> class.
        /// </summary>
        public SkillDetailController()
        {
            _skillService = new SkillsDetailService();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets all skills.
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        [AcceptVerbs("GET", "GetAllSkills")]
        public HttpResponseMessage GetAllSkills()
        {
            var allSkills = _skillService.GetAllSkills();
            return Request.CreateResponse(HttpStatusCode.OK, allSkills);
        }

        /// <summary>
        /// Deletes the skill.
        /// </summary>
        /// <param name="skillId">The skill identifier.</param>
        /// <returns>HttpResponseMessage</returns>
        [AcceptVerbs("GET", "DeleteSkill")]
        public HttpResponseMessage DeleteSkill(int skillId)
        {
            bool deleteStatus = _skillService.DeleteSelectedSkill(skillId);
            return Request.CreateResponse(HttpStatusCode.OK, deleteStatus);
        }

        /// <summary>
        /// Saves the skills.
        /// </summary>
        /// <param name="technologyId">The technology identifier.</param>
        /// <param name="skillNames">The skill names.</param>
        /// <returns>HttpResponseMessage</returns>
        [AcceptVerbs("GET", "SaveSkills")]
        public HttpResponseMessage SaveSkills(int technologyId,string skillNames)
        {
            _skillService.SaveSkills(technologyId, skillNames);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        #endregion
    }
}