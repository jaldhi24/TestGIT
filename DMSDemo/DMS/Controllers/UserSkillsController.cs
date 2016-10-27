using DMS.Models;
using DMS.Services.BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DMS.Controllers
{
    public class UserSkillsController : ApiController
    {
        #region Private Variables
        /// <summary>
        /// The _user skill service
        /// </summary>
        private readonly UserSkillsService _userSkillService;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSkillsController"/> class.
        /// </summary>
        public UserSkillsController()
        {
            _userSkillService = new UserSkillsService();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the user skills.
        /// </summary>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        [HttpGet]
        public HttpResponseMessage GetUserSkills()
        {
            int userId = ProjectSession.LoggedInUserId;
            var skillOfUser = _userSkillService.GetUserSkills(userId);
            return Request.CreateResponse(HttpStatusCode.OK, skillOfUser);
        }

        [HttpGet]
        public HttpResponseMessage GetSkillByTechnology(string TechIds)
        {
            var skill = _userSkillService.GetSkillByTechnology(TechIds);
            return Request.CreateResponse(HttpStatusCode.OK, skill);
        }

        /// <summary>
        /// Inserts the update user skill.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="techId">The tech identifier.</param>
        /// <param name="skillId">The skill identifier.</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        public HttpResponseMessage InsertUpdateUserSkill(int userId, string techId, string skillId)
        {
            _userSkillService.InsertUpdateSkill(userId, techId, skillId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        #endregion
    }
}