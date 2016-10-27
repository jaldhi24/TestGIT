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
    /// IUserLogin Service
    /// </summary>
    public interface IUserLoginService
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>UserLogin</returns>
        List<UserLogin> GetAllUsers();

        /// <summary>
        /// Gets the user detail by network user identifier.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <returns>UserLoginEntity</returns>
        UserLoginEntity GetUserDetailByNetworkUserId(string serverName);

        /// <summary>
        /// Signs up user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>boolean</returns>
        bool SignUpUser(UserLoginEntity user);

        /// <summary>
        /// Authenticates the specified server name.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <returns>Authenticate</returns>
        UserLogin Authenticate(string serverName);
    }
}
