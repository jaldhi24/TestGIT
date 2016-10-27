using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TIA.HR.Api.Common;
using TIA.HR.Api.Entities;
using TIA.HR.Api.Model.HRModel;
using TIA.HR.Api.Model.UnitOfWork;

namespace TIA.HR.Api.Services
{
    /// <summary>
    /// user service class
    /// </summary>
    public class UserService : IUser
    {
        /// <summary>
        /// The _unit of work
        /// </summary>
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionService"/> class.
        /// </summary>
        public UserService()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Gets the employee language preference.
        /// </summary>
        /// <param name="internalId">The internal identifier.</param>
        /// <returns>user entity</returns>
        public UserEntity GetEmployeeLanguagePreference(int internalId)
        {
            var userLangPreference = _unitOfWork.EmpLanguageRepository.GetAll().Where(m => m.InternalId == internalId).FirstOrDefault();

            UserEntity userEntity = new UserEntity();

            if (userLangPreference != null)
            {
                userEntity.LanguageCd = userLangPreference.LanguageCd;
                userEntity.CanSpeak = userLangPreference.CanSpeak;
                userEntity.CanWrite = userLangPreference.CanReadWrite;
                userEntity.InternalId = userLangPreference.InternalId;
            }
            else
            {
                tblQSR qsrObj = new tblQSR();
                qsrObj = _unitOfWork.QsrRepository.GetByID(internalId);
                userEntity.LanguageCd = qsrObj.PreferredCultureCd;
                userEntity.InternalId = internalId;
            }

            return userEntity;
        }

        /// <summary>
        /// Posts the employee language preference.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void PostEmployeeLanguagePreference(LoggedInUserDetail entity)
        {
            tblQSR qsrObj = new tblQSR();
            qsrObj = _unitOfWork.QsrRepository.GetByID(entity.InternalId);
            qsrObj.PreferredCultureCd = entity.LanguageCd;
            _unitOfWork.QsrRepository.Update(qsrObj);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Gets the user detail by network user identifier.
        /// </summary>
        /// <param name="networkUserId">The network user identifier.</param>
        /// <returns>
        /// UserEntity
        /// </returns>
        public UserEntity GetUserDetailByNetworkUserId(string networkUserId)
        {
            var user = (from qsr in _unitOfWork.QsrRepository.Table()
                        join sf in _unitOfWork.StoredFileRepository.Table()
                        on qsr.PictureFileId equals sf.FileId into userDetail
                        from sf in userDetail.DefaultIfEmpty()
                        join au in _unitOfWork.AspNetUserRepository.Table()
                        on qsr.NetworkUserId equals au.UserName into userrole
                        from sd in userrole.DefaultIfEmpty()
                        where (qsr.NetworkUserId.Equals(networkUserId))
                        select new UserEntity
                        {
                            NetworkUserId = qsr.NetworkUserId,
                            InternalId = qsr.InternalId,
                            FirstName = qsr.FirstName,
                            LastName = qsr.LastName,
                            LanguageCd = string.IsNullOrEmpty(qsr.PreferredCultureCd) ? ProjectSession._DefaultLanguageCd : qsr.PreferredCultureCd,
                            Image = sf.FileData,
                            Roles = sd.AspNetRoles.Select(x => x.Name)
                        }).FirstOrDefault();

            return user;
        }

        /// <summary>
        /// Gets the user detail by internal identifier.
        /// </summary>
        /// <param name="internalId">The internal identifier.</param>
        /// <returns>UserEntity</returns>
        public UserEntity GetUserDetailByInternalId(int internalId)
        {
            var user = (from qsr in _unitOfWork.QsrRepository.Table()
                        join sf in _unitOfWork.StoredFileRepository.Table()
                        on qsr.PictureFileId equals sf.FileId into userDetail
                        from sf in userDetail.DefaultIfEmpty()
                        join au in _unitOfWork.AspNetUserRepository.Table()
                        on qsr.NetworkUserId equals au.UserName into userrole
                        from sd in userrole.DefaultIfEmpty()
                        where (qsr.InternalId.Equals(internalId))
                        select new UserEntity
                        {
                            NetworkUserId = qsr.NetworkUserId,
                            InternalId = qsr.InternalId,
                            FirstName = qsr.FirstName,
                            LastName = qsr.LastName,
                            LanguageCd = string.IsNullOrEmpty(qsr.PreferredCultureCd) ? ProjectSession._DefaultLanguageCd : qsr.PreferredCultureCd,
                            Image = sf.FileData,
                            Roles = sd.AspNetRoles.Select(x => x.Name)
                        }).FirstOrDefault();

            return user;
        }
    }
}
