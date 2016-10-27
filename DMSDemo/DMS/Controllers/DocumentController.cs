using DMS.Entities.Entities;
using DMS.Models;
using DMS.Services.BusinessServices;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.WebPages.Html;
using WebApi.ActionFilters;

namespace DMS.Controllers
{
    //[AuthorizationRequiredAttribute]
    public class DocumentController : ApiController
    {
        #region Private Variables
        /// <summary>
        /// The _document service
        /// </summary>
        private readonly DocumentDetailService _documentService;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentController"/> class.
        /// </summary>
        public DocumentController()
        {
            _documentService = new DocumentDetailService();
        }

        #endregion
        // GET api/<controller>
        /// <summary>
        /// Gets all documnets.
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        //[HttpGet]
        //[ActionName("GetAllDocumnets")]
        [AcceptVerbs("GET", "GetAllDocumnets")]
        public HttpResponseMessage GetAllDocumnets()
        {
            var allDocument = _documentService.GetAllDocumentDetail();
            return Request.CreateResponse(HttpStatusCode.OK, allDocument);
        }

        /// <summary>
        /// Gets all technology.
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        //[HttpGet]
        //[ActionName("GetAllTechnology")]
        //GET api/DocumentController
        [AcceptVerbs("GET", "GetAllTechnology")]
        public HttpResponseMessage GetAllTechnology()
        {
            var allTechnology = _documentService.GetAllTechnology();
            return Request.CreateResponse(HttpStatusCode.OK, allTechnology);
        }

        /// <summary>
        /// Gets the document by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>HttpResponseMessage</returns>
        [AcceptVerbs("GET", "GetDocumentById")]
        public HttpResponseMessage GetDocumentById(int id)
        {
            var Document = _documentService.GetDocumentById(id);
            return Request.CreateResponse(HttpStatusCode.OK, Document);
        }

        [AcceptVerbs("GET", "GetAllFileName")]
        public HttpResponseMessage GetAllFileName()
        {
            var allDocFileName = _documentService.GetAllDocNames();

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var us in allDocFileName)
            {
                items.Add(new SelectListItem { Text = us.Name.Trim(), Value = us.Id.ToString() });
            }
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Saves the upload.
        /// </summary>
        //[ActionName("SaveUpload")]
        [HttpPost]
        public bool SaveUpload()
        {
            bool saveStatus = false;

            // get variables first
            var storeFileInLocation = string.Empty;
            var storeCodeInLocation = string.Empty;

            var httpPostedFile = HttpContext.Current.Request.Files["UploadedFile"];
            var httpPostedCode = HttpContext.Current.Request.Files["UploadedCode"];

            if (httpPostedFile != null || httpPostedCode != null || Convert.ToInt32(HttpContext.Current.Request.Form[0]) != 0)
            {
                NameValueCollection nvc = HttpContext.Current.Request.Form;
                var model = new DocumentDetailEntity();
                model.UserID = ProjectSession.LoggedInUserId;

                // iterate through and map to strongly typed model
                foreach (var kvp in nvc.AllKeys)
                {
                    PropertyInfo pi = model.GetType().GetProperty(kvp, BindingFlags.Public | BindingFlags.Instance);
                    if (pi != null)
                    {
                        if (pi.PropertyType.Name == "Int32")
                        {
                            pi.SetValue(model, Convert.ToInt32(nvc[kvp]), null);
                        }
                        else
                        {
                            pi.SetValue(model, nvc[kvp], null);
                        }
                    }
                }

                string Date = System.DateTime.Now.ToString("ddMMyyyy");


                if (httpPostedFile != null)
                {
                    string httpPostedFileExtension = httpPostedFile.FileName.Split('.')[1];
                    string wholeFileName = httpPostedFile.FileName.Split('.')[0] + "_" + ProjectSession.LoggedInServerName + "_" + Date + "." + httpPostedFile.FileName.Split('.')[1];
                    storeFileInLocation = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploadsFile/" + wholeFileName);
                    //string filePath = "uploadsFile\\" + wholeFileName;
                    model.FilePath = wholeFileName;
                }

                if (httpPostedCode != null)
                {
                    string wholeCodeName = httpPostedCode.FileName.Split('.')[0] + "_" + ProjectSession.LoggedInServerName + "_" + Date + "." + httpPostedCode.FileName.Split('.')[1];
                    storeCodeInLocation = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploadsCode/" + wholeCodeName);
                    string codePath = "uploadsCode\\" + wholeCodeName;
                    model.CodePath = wholeCodeName;
                }

                if (model.Id == 0)
                {
                    model.CreatedOn = System.DateTime.Now;
                    saveStatus = _documentService.CreateDocument(model);
                }
                else
                {
                    var recordSingle = _documentService.GetDocumentById(model.Id).SingleOrDefault();
                    string path = string.Empty;
                    if (recordSingle.FilePath != null && recordSingle.CodePath != null)
                    {
                        path = recordSingle.FilePath + "," + recordSingle.CodePath;
                    }
                    else if (recordSingle.FilePath == null && recordSingle.CodePath != null)
                    {
                        path = recordSingle.CodePath;
                    }
                    else if (recordSingle.FilePath != null && recordSingle.CodePath == null)
                    {
                        path = recordSingle.FilePath;
                    }

                    //FileDelete(path, 0, true, 0);
                    model.UpdatedOn = System.DateTime.Now;
                    saveStatus = _documentService.UpdateDocument(model.Id, model);
                }

                if (saveStatus == true && httpPostedFile != null)
                {
                    httpPostedFile.SaveAs(storeFileInLocation);
                }
                if (saveStatus == true && httpPostedCode != null)
                {
                    httpPostedCode.SaveAs(storeCodeInLocation);
                }
            }

            return saveStatus;
        }

        /// <summary>
        /// Files the delete.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        /// boolean
        /// </returns>
        [HttpGet]
        public bool FileDelete(string path, int id, bool? isupdateOperation, int? contentTypeId)
        {
            bool deleteStatus = false;
            if (contentTypeId == 16)
            {
                string filePath = ConfigurationManager.AppSettings["FilePath"].ToString();
                path = filePath + path;
            }
            if (contentTypeId == 17)
            {
                string codePath = ConfigurationManager.AppSettings["CodePath"].ToString();
                path = codePath + path;
            }
            if (path.Contains(","))
            {
                var data = path.Split(',');
                for (int i = 0; i < data.Length; i++)
                {
                    if (File.Exists(data[i]))
                    {
                        File.Delete(data[i]);
                        deleteStatus = true;
                    }
                    else
                    {
                        deleteStatus = false;
                        break;
                    }
                }
            }
            else
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    deleteStatus = true;
                }
            }

            if (isupdateOperation == false)
            {
                if (deleteStatus)
                {
                    deleteStatus = _documentService.DeleteDocument(id);
                }
            }
            else
            {
                if (deleteStatus)
                {
                    deleteStatus = _documentService.ClearPath(id, contentTypeId, path);
                }
            }

            return deleteStatus;
        }

        /// <summary>
        /// Downloads the attachment.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        public HttpResponseMessage DownloadAttachment(string fileName, int contentType)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            byte[] bytes;
            string filePath = "";
            if (contentType == 16)
            {
                filePath = HttpContext.Current.Server.MapPath("~/App_Data/uploadsFile/" + fileName);
            }

            if (contentType == 17)
            {
                filePath = HttpContext.Current.Server.MapPath("~/App_Data/uploadsCode/" + fileName);
            }
            try
            {
                bytes = System.IO.File.ReadAllBytes(filePath);
            }
            catch (Exception)
            {
                fileName = "NoFileFound.txt";
                filePath = HttpContext.Current.Server.MapPath("~/App_Data/uploadsFile/" + fileName);
                bytes = System.IO.File.ReadAllBytes(filePath);
            }

            var stream = System.IO.MemoryStream.Null;

            // var singleAttachment = _iemployeeService.GetSingleAttachments(fileId);

            if (bytes != null)
            {
                stream = new System.IO.MemoryStream(bytes);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.Add("content-disposition", "attachment;  filename=\"" + fileName + "\"");
                return result;
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "File not found.");
        }
    }
}