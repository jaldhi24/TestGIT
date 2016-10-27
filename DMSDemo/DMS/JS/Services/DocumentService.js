app.service("DocumentService", function ($http) {

    this.getAllDocuments = function () {
        return $http.get("api/Document/GetAllDocumnets");
    }

    this.getAllTechnology = function () {
        return $http.get("api/Document/GetAllTechnology");
    }

    this.getAllFileName = function () {
        return $http.get("api/Document/GetAllFileName");
    }

    this.getDocumentById = function (id) {

        return $http.get("api/Document/GetDocumentById?id=" + id);
    }

    this.FileUploadDetail = function (data) {

        var ajaxRequest = $.ajax({
            type: 'POST',
            url: 'api/Document/SaveUpload',
            data: data,
            // contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processData: false,
            contentType: false// not json            
        });

        return ajaxRequest;
    }

    this.downloadFileAttachment = function (fileName, ContentType) {
        debugger;
        // return $http.get("api/Document/DownloadAttachment?filePath=" + filePath);
        return window.open("api/Document/DownloadAttachment?fileName=" + fileName + "&ContentType=" + ContentType, "_blank");
        //  return $http.post('api/Document/DownloadAttachment', filePath);
    }

    this.DeleteDocument = function (path, Id, isupdateOperation, contentTypeId) {
        return $http.get("api/Document/FileDelete?path=" + path + "&id=" + Id + "&isupdateOperation=" + isupdateOperation + "&contentTypeId=" + contentTypeId);
    }

    //this.Generate = function (path) {

    //    return $http.get("api/Document/Generate?path=" + path);
    //}
});