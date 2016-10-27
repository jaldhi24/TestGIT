using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.Entities
{
    /// <summary>
    /// DocumentDetail Entity
    /// </summary>
    public class DocumentDetailEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the content type identifier.
        /// </summary>
        /// <value>
        /// The content type identifier.
        /// </value>
        public int ContentTypeID { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the code path.
        /// </summary>
        /// <value>
        /// The code path.
        /// </value>
        public string CodePath { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string DocCreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the technology identifier.
        /// </summary>
        /// <value>
        /// The technology identifier.
        /// </value>
        public int TechnologyID { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public System.DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        /// <value>
        /// The updated on.
        /// </value>
        public System.DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the uploaded by.
        /// </summary>
        /// <value>
        /// The uploaded by.
        /// </value>
        public string UploadedBy { get; set; }

        /// <summary>
        /// Gets or sets the name of the technology.
        /// </summary>
        /// <value>
        /// The name of the technology.
        /// </value>
        public string TechnologyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
    }
}
