using DMS.Entities.Entities;
using DMS.Model.DMSModel;
using DMS.Model.UnitOfWork;
using DMS.Services.BusinessServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DMS.Services.BusinessServices
{
    /// <summary>
    /// User Login Service
    /// </summary>
    public class UserLoginService : IUserLoginService
    {
        /// <summary>
        /// The _unit of work
        /// </summary>
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoginService"/> class.
        /// </summary>
        public UserLoginService()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>UserLogin</returns>
        public List<UserLogin> GetAllUsers()
        {
            var query = _unitOfWork.UserLoginRepository.GetAll().ToList();
            return query;
        }

        /// <summary>
        /// Gets the user detail by network user identifier.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <returns>UserLoginEntity</returns>
        public UserLoginEntity GetUserDetailByNetworkUserId(string serverName)
        {
            var query = (from user in _unitOfWork.UserLoginRepository.Table()
                         where user.ServerName.ToLower().Equals(serverName.ToLower())
                         select new UserLoginEntity
                         {
                             Id = user.Id,
                             ServerName = user.ServerName,
                             UserName = user.UserName
                         }).SingleOrDefault();
            return query;
        }

        /// <summary>
        /// Signs up user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>boolean</returns>
        public bool SignUpUser(UserLoginEntity user)
        {
             bool saveStatus = false;
             using (var scope = new TransactionScope())
             {
                 var newUser = new UserLogin
                 {
                    ServerName=user.ServerName,
                    UserName=user.UserName
                 };
                 _unitOfWork.UserLoginRepository.Insert(newUser);
                 _unitOfWork.Save();
                 saveStatus = true;
                 scope.Complete();
             }

             return saveStatus;
        }

        /// <summary>
        /// Authenticates the specified user name.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <returns>
        /// UserLogin
        /// </returns>
        public UserLogin Authenticate(string serverName)
        {
            var user = _unitOfWork.UserLoginRepository.Get(u => u.ServerName == serverName);
            return user;
        }
    }
}
