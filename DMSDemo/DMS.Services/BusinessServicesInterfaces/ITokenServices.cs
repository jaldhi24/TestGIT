using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMS.Model.DMSModel;

namespace WebApi.Services
{
    /// <summary>
    /// TokenServices
    /// </summary>
    public interface ITokenServices
    {
        #region Interface member methods.
        /// <summary>
        /// Function to generate unique token with expiry again the provided userId.
        /// Also add a record in database for generated token.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Token
        /// </returns>
        Token GenerateToken(int userId);

        /// <summary>
        /// Function to validate token again expiry and exist in database.
        /// </summary>
        /// <param name="tokenId">The token identifier.</param>
        /// <returns>
        /// Validate Token
        /// </returns>
        bool ValidateToken(string tokenId);

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId">The token identifier.</param>
        /// <returns>
        /// Kill
        /// </returns>
        bool Kill(string tokenId);

        /// <summary>
        /// Delete tokens for the specific deleted user
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Validate Token
        /// </returns>
        bool DeleteByUserId(int userId);
        #endregion
    }
}
