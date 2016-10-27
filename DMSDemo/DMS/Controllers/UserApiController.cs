using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using DMS.Services.BusinessServices;
using DMS.Entities.Entities;
using WebApi.Services;
using System.Configuration;
using DMS.Model.DMSModel;

namespace TIA.HR.Api.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// The User API controller
    /// </summary>
    public class UserApiController : ApiController
    {
        /// <summary>
        /// The _user service
        /// </summary>
        private readonly UserLoginService _userService;        
        private readonly ITokenServices _tokenServices;
                    

        /// <summary>
        /// Initializes a new instance of the <see cref="UserApiController"/> class.
        /// </summary>
        public UserApiController()
        {
            _userService = new UserLoginService();
            _tokenServices = new TokenServices();
        }

        /// <summary>
        /// Gets the user detail by network user identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Http Response Message</returns>
        [HttpGet]
        public HttpResponseMessage GetUserDetailByNetworkUserId(string id)
        {
            UserLoginEntity userentity = _userService.GetUserDetailByNetworkUserId(id);
            return Request.CreateResponse(HttpStatusCode.OK, userentity);
        }

        /// <summary>
        /// Posts the user.
        /// </summary>
        /// <param name="userEntity">The user entity.</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        public HttpResponseMessage PostUser(UserLoginEntity userEntity)
        {
            bool saveStatus = false;
            saveStatus = _userService.SignUpUser(userEntity);
            return Request.CreateResponse(HttpStatusCode.OK, saveStatus);
        }

        [HttpPost]
        public HttpResponseMessage Authenticate(string serverName)
        {
            var userAuth = _userService.Authenticate(serverName);
            if (userAuth != null)
                return GetAuthToken(userAuth);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found");
        }

        private HttpResponseMessage GetAuthToken(UserLogin user)
        {
            var token = _tokenServices.GenerateToken(user.Id);
            var response = Request.CreateResponse(HttpStatusCode.OK, user.Id);
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
            return response;
        }
    }
}
