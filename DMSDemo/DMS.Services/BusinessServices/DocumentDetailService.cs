using DMS.Entities.Entities;
using DMS.Model.DMSModel;
using DMS.Model.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Configuration;

namespace DMS.Services.BusinessServices
{
    /// <summary>
    /// Look Up Details
    /// </summary>
    public enum LookUpDetailsEnum
    {
        /// <summary>
        /// The technology
        /// </summary>
        Technology = 1,

        /// <summary>
        /// The file type
        /// </summary>
        FileType = 2
    }

    /// <summary>
    /// DocumentDetailService
    /// </summary>
    public class DocumentDetailService : IDocumentDetailService
    {
        /// <summary>
        /// The _unit of work
        /// </summary>
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// BonusLevelService Constructor
        /// </summary>
        public DocumentDetailService()
        {
            this._unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Gets all document detail.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>DocumentDetailEntity</returns>       
        public IEnumerable<DocumentDetailEntity> GetAllDocumentDetail(int id = 0)
        {
            ////string filePath = ConfigurationManager.AppSettings["FilePath"].ToString();
            ////string codePath = ConfigurationManager.AppSettings["CodePath"].ToString();
            var query = _unitOfWork.DocumentDetailRepository.Table()
                 .Select(c => new DocumentDetailEntity
                 {
                     Id = c.Id,
                     Name = c.Name,
                     Description = c.Description,
                     ContentTypeID = c.ContentTypeID,
                     FilePath = c.FilePath,
                     CodePath = c.CodePath,
                     DocCreatedBy = c.DocCreatedBy,
                     CreatedOn = c.CreatedOn,
                     TechnologyName = c.LookupDetail1.Name,
                     UploadedBy = c.UserLogin.UserName
                 }).ToList();

            return query;
        }

        /// <summary>
        /// Gets all technology.
        /// </summary>
        /// <returns>LookUpDetailsEntity</returns>
        public List<LookUpDetailsEntity> GetAllTechnology()
        {
            int lookupType = Convert.ToInt32(LookUpDetailsEnum.Technology);
            var query = _unitOfWork.LookupDetailRepository.GetWithPredicate(m => m.LookupMasterId == lookupType).Select(
                c => new LookUpDetailsEntity
                 {
                     Id = c.Id,
                     Name = c.Name
                 }).ToList();
            return query;
        }

        /// <summary>
        /// Gets the document by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>DocumentDetailEntity</returns>
        public IEnumerable<DocumentDetailEntity> GetDocumentById(int id)
        {
            int lookupType = Convert.ToInt32(LookUpDetailsEnum.Technology);
            var query = (from document in _unitOfWork.DocumentDetailRepository.Table()
                         join lookUpDetails in _unitOfWork.LookupDetailRepository.Table() on
                         document.TechnologyID equals lookUpDetails.Id
                         join userLogin in _unitOfWork.UserLoginRepository.Table()
                         on document.UserID equals userLogin.Id
                         where document.Id == id
                         select new DocumentDetailEntity
                         {
                             Id = document.Id,
                             Name = document.Name,
                             Description = document.Description,
                             FilePath = document.FilePath,
                             CodePath = document.CodePath,
                             DocCreatedBy = document.DocCreatedBy,
                             CreatedOn = document.CreatedOn,
                             UpdatedOn = document.UpdatedOn,
                             UserName = userLogin.UserName,
                             TechnologyName = lookUpDetails.Name,
                             ContentTypeID = document.ContentTypeID,
                             TechnologyID = document.TechnologyID
                         }).ToList();

            return query;
        }

        /// <summary>
        /// Creates the document.
        /// </summary>
        /// <param name="documentEntity">The document entity.</param>
        /// <returns>
        /// boolean
        /// </returns>
        public bool CreateDocument(DocumentDetailEntity documentEntity)
        {
            bool saveStatus = false;
            using (var scope = new TransactionScope())
            {
                var document = new DocumentDetail
                {
                    Name = documentEntity.Name,
                    Description = documentEntity.Description,
                    FilePath = documentEntity.FilePath,
                    CodePath = documentEntity.CodePath,
                    ContentTypeID = documentEntity.ContentTypeID,
                    DocCreatedBy = documentEntity.DocCreatedBy,
                    UserID = documentEntity.UserID,
                    TechnologyID = documentEntity.TechnologyID,
                    CreatedOn = System.DateTime.Now,
                    UpdatedOn = null
                };
                _unitOfWork.DocumentDetailRepository.Insert(document);
                _unitOfWork.Save();
                saveStatus = true;
                scope.Complete();
            }

            return saveStatus;
        }

        /// <summary>
        /// Gets all document names.
        /// </summary>
        /// <returns>
        /// DocumentDetailEntity
        /// </returns>
        public IEnumerable<DocumentDetailEntity> GetAllDocNames()
        {
            var query = (from docNames in _unitOfWork.DocumentDetailRepository.Table()
                         select new DocumentDetailEntity
                         {
                             Name = docNames.Name
                         }).ToList();

            return query;
        }

        /// <summary>
        /// Deletes the document.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// boolean
        /// </returns>
        public bool DeleteDocument(int id)
        {
            bool deleteStatus = false;
            using (var scope = new TransactionScope())
            {
                _unitOfWork.DocumentDetailRepository.Delete(id);
                _unitOfWork.Save();
                deleteStatus = true;
                scope.Complete();
            }

            return deleteStatus;
        }

        /// <summary>
        /// Updates the document.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="documentEntity">The document entity.</param>
        /// <returns>
        /// boolean
        /// </returns>
        public bool UpdateDocument(int id, DocumentDetailEntity documentEntity)
        {
            bool updateStatus = false;
            using (var scope = new TransactionScope())
            {
                var recordForUpdate = _unitOfWork.DocumentDetailRepository.GetByID(id);
                recordForUpdate.Name = documentEntity.Name;
                recordForUpdate.Description = documentEntity.Description;
                recordForUpdate.DocCreatedBy = documentEntity.DocCreatedBy;
                recordForUpdate.TechnologyID = documentEntity.TechnologyID;
                recordForUpdate.ContentTypeID = documentEntity.ContentTypeID;
                recordForUpdate.FilePath = documentEntity.FilePath;
                recordForUpdate.CodePath = documentEntity.CodePath;
                recordForUpdate.UpdatedOn = System.DateTime.Now;
                recordForUpdate.UserID = documentEntity.UserID;
                _unitOfWork.DocumentDetailRepository.Update(recordForUpdate);
                _unitOfWork.Save();
                scope.Complete();
                updateStatus = true;
            }

            return updateStatus;
        }

        /// <summary>
        /// Clears the path.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="contentTypeId">The content type identifier.</param>
        /// <param name="path">The path.</param>
        /// <returns>boolean</returns>
        public bool ClearPath(int id, int? contentTypeId, string path)
        {
            bool nullStatus = false;
            using (var scope = new TransactionScope())
            {
                var recordForNullColumn = _unitOfWork.DocumentDetailRepository.GetByID(id);
                if (contentTypeId == 16)
                {
                    recordForNullColumn.FilePath = null;
                }

                if (contentTypeId == 17)
                {
                    recordForNullColumn.CodePath = null;
                }

                _unitOfWork.DocumentDetailRepository.Update(recordForNullColumn);
                _unitOfWork.Save();
                scope.Complete();
                nullStatus = true;
            }

            return nullStatus;
        }
    }
}
