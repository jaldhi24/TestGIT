using DMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.BusinessServices
{
    /// <summary>
    /// IDocumentDetailService
    /// </summary>
    public interface IDocumentDetailService
    {
        /// <summary>
        /// Gets all document detail.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>DocumentDetailEntity</returns>
        IEnumerable<DocumentDetailEntity> GetAllDocumentDetail(int id = 0);

        /// <summary>
        /// Gets all technology.
        /// </summary>
        /// <returns>LookUpDetailsEntity</returns>
        List<LookUpDetailsEntity> GetAllTechnology();

        /// <summary>
        /// Gets the document by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>DocumentDetailEntity</returns>
        IEnumerable<DocumentDetailEntity> GetDocumentById(int id);

        /// <summary>
        /// Creates the document.
        /// </summary>
        /// <param name="documentEntity">The document entity.</param>
        /// <returns>boolean</returns>
        bool CreateDocument(DocumentDetailEntity documentEntity);

        /// <summary>
        /// Gets all document names.
        /// </summary>
        /// <returns>DocumentDetailEntity</returns>
        IEnumerable<DocumentDetailEntity> GetAllDocNames();

        /// <summary>
        /// Deletes the document.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// boolean
        /// </returns>
        bool DeleteDocument(int id);

        /// <summary>
        /// Updates the document.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="documentEntity">The document entity.</param>
        /// <returns>
        /// boolean
        /// </returns>
        bool UpdateDocument(int id,DocumentDetailEntity documentEntity);

        /// <summary>
        /// Clears the path.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="contentTypeId">The content type identifier.</param>
        /// <param name="path">The path.</param>
        /// <returns>ClearPath</returns>
        bool ClearPath(int id, int? contentTypeId, string path);
    }
}
