using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIA.HR.Api.Model.HRModel;
using TIA.HR.Api.Entities;
using TIA.HR.Api.Common;

namespace TIA.HR.Api.Services
{
    /// <summary>
    /// User Interface
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets the employee language preference.
        /// </summary>
        /// <param name="internalId">The internal identifier.</param>
        /// <returns>UserEntity</returns>
        UserEntity GetEmployeeLanguagePreference(int internalId);

        /// <summary>
        /// Posts the employee language preference.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void PostEmployeeLanguagePreference(LoggedInUserDetail entity);

        /// <summary>
        /// Gets the user detail by network user identifier.
        /// </summary>
        /// <param name="networkUserId">The network user identifier.</param>
        /// <returns>UserEntity</returns>
        UserEntity GetUserDetailByNetworkUserId(string networkUserId);

        /// <summary>
        /// Gets the user detail by internal identifier.
        /// </summary>
        /// <param name="internalId">The internal identifier.</param>
        /// <returns>UserEntity</returns>
        UserEntity GetUserDetailByInternalId(int internalId);
    }
}
