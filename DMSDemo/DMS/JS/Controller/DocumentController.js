app.controller("DocumentController", function ($scope, $compile, $q, DocumentService, $filter, notificationFactory) {

    $scope.gridOptions = {
    };
    $scope.technologies = [];
    $scope.AllFileNames = [];
    getAllDocuments();
    AllTechnology();
    AllFileName();

    $scope.validErrors = false;
    $scope.Doc = [];
    ResertDefaults();

    function ResertDefaults() {
        $scope.Doc.Id = 0;
        $scope.Doc.Name = "";
        $scope.Doc.Description = "";
        $scope.Doc.CreayedBy = "";
        $scope.Doc.SelectedTechId = 2;
        $scope.Doc.ContentTypeID = 0;
        $scope.Doc.EditOp = false;
        $scope.FileExists = false;
        $scope.CodeExists = false;
        $scope.Doc.BothFileExists = false;
    }

    $scope.Doc.IsFile = false;
    $scope.Doc.IsCode = false;


    function AllFileName() {
        var FileNamePromise = DocumentService.getAllFileName();
        FileNamePromise.success(function (pl) {
            $scope.AllFileNames = pl;
        });
        FileNamePromise.error(function (pl) {
        });
    }

    function getAllDocuments() {

        var getDocuments = DocumentService.getAllDocuments();
        getDocuments.success(function (pl) {


            if ($.fn.DataTable.isDataTable("#tblDocuments")) {
                $('#tblDocuments').DataTable().destroy();
            }

            $('#tblDocuments').DataTable({
                data: pl,
                "bDestroy": true,
                //"dom": '<"top"f><"table-responsive"rt><"bottom"lip<"clear">>',
                "aLengthMenu": [10, 20, 50, 100, 200],
                "pageLength": 20,
                "columns": [
                     {
                         "title": "Id",
                         "data": "Id"
                         //"render": function (data, type, row) {
                         //    return '<a href="#/EditJobGrade/' + row.JobGradeId + '" >' + data + '</a>';
                         //}
                     },
               // { "title": "JobBandNumber", "data": "JobBandNumber" },
                { "title": "File Name", "data": "Name" },
                { "title": "Technology Name", "data": "TechnologyName" },
                { "title": "Doc Created By", "data": "DocCreatedBy" },
                { "title": "Uploaded By", "data": "UploadedBy" },
                { "title": "Created Date", "data": "CreatedOn" },
                {
                    "title": "Action",
                    "data": null,
                    "sClass": "action",
                    "render": function (data, type, row) {
                        return '<a ng-click=\'EditDocument(' + JSON.stringify(row) + ')\' ><img class="cursor-pointer" src="Images/ic-edit.png" data-toggle="tooltip" title="Edit Document"></a>' +
                            '<a ng-click=\'DeleteDocument(' + JSON.stringify(row) + ')\' ><i style="color:black" class="glyphicon glyphicon-trash cursor-pointer" src="Images/ic-delete.png" data-toggle="tooltip" title="Delete Document"></i></a>' +
                            '<a ng-click=\'DownloadFile(' + JSON.stringify(row.FilePath) + ')\'><img class="cursor-pointer" src="Images/ic-down.png" data-toggle="tooltip" title="Download File"></a>' +
                            '<a ng-click=\'DownloadCode(' + JSON.stringify(row.CodePath) + ')\'><img class="cursor-pointer" src="Images/ic-down.png" data-toggle="tooltip" title="Download Code"></a>'

                    }
                }
                ],
                "initComplete": function () {
                    var dataTable = $('#tblDocuments').DataTable();
                    //BindCustomerSearchBar($scope, $compile, dataTable);
                    $compile(angular.element("#tblDocuments").contents())($scope);
                }
            });
        });
    }

    $scope.AddNewDeleteExistingFile = function (FilePath, Id) {

        alert(FilePath);
        var deleteDocument = DocumentService.DeleteDocument(FilePath, Id, true, 16);
        deleteDocument.success(function (data) {
            if (data == true) {
                $scope.FileExists = false;
                if ($scope.CodeExists == true || $scope.FileExists == true) {
                    $scope.Doc.BothFileExists = true;
                }
                else {
                    $scope.Doc.BothFileExists = false;
                }
            }
            else {
                $scope.FileExists = true;
            }
        });
        deleteDocument.error(function () {
        });
    }

    $scope.AddNewDeleteExistingCodeFile = function (CodePath, Id) {
        alert(CodePath);
        var deleteDocument = DocumentService.DeleteDocument(CodePath, Id, true, 17);
        deleteDocument.success(function (data) {
            if (data == true) {
                $scope.CodeExists = false;
                if ($scope.CodeExists == true || $scope.FileExists == true) {
                    $scope.Doc.BothFileExists = true;
                }
                else {
                    $scope.Doc.BothFileExists = false;
                }
            }
            else {
                $scope.CodeExists = true;
            }
        });
        deleteDocument.error(function () {
        });
    }

    $scope.DownloadFile = function (fileName) {
        debugger;
        if (fileName != null) {
            var getFile = DocumentService.downloadFileAttachment(fileName, 16);
            debugger;
        }
        else {
            notificationFactory.WarningNoFile("There is no file for this record.");
        }
    }

    $scope.DownloadCode = function (codeName) {
        debugger;
        if (codeName != null) {
            var getFileCode = DocumentService.downloadFileAttachment(codeName, 17);
        }
        else {
            notificationFactory.WarningNoFile("There is no file for this record.");
        }
    }

    function AllTechnology() {
        var Technologies = DocumentService.getAllTechnology();
        Technologies.success(function (pl) {
            $scope.technologies = pl;
            $scope.Doc.SelectedTechId = $scope.technologies[0].Id;
        });
        Technologies.error(function (pl) {
        });
    }

    $scope.EditDocument = function (data) {
        $scope.validErrors = false;
        var DocumentById = DocumentService.getDocumentById(data.Id);
        DocumentById.success(function (pl) {
            var document = pl;
            $scope.Doc.Id = document[0].Id;
            $scope.Doc.Name = document[0].Name;
            $scope.Doc.Description = document[0].Description;
            $scope.Doc.CreayedBy = document[0].DocCreatedBy;
            $scope.Doc.SelectedTechId = document[0].TechnologyID;
            if (document[0].FilePath != null) {
                $scope.selectedFileDisplay = document[0].FilePath;
                $scope.FileExists = true;
            }
            else {
                $scope.FileExists = false;
            }
            if (document[0].CodePath != null) {
                $scope.selectedCodeDisplay = document[0].CodePath;
                $scope.CodeExists = true;
            }
            else {
                $scope.CodeExists = false;
            }
            if ($scope.Doc.Id > 0) {
                $scope.Doc.EditOp = true;
                $scope.Doc.BothFileExists = true;
            }
            if ($scope.FileExists == true || $scope.CodeExists == true) {
                $scope.Doc.BothFileExists = true;
            }
            else {
                $scope.Doc.BothFileExists = false;
            }
        });
        DocumentById.error(function () {
        });
    }

    $scope.Cancel = function (form) {
        form.$submitted = false;
        form.$valid = true;
        $scope.validationErrors = [];
        $scope.validErrors = false;
        $scope.Doc.Name = "";
        $scope.Doc.Description = "";
        $scope.Doc.CreayedBy = "";
        $scope.Doc.SelectedTechId = 2;
        $scope.Doc.BothFileExists = false;
        $('#FileUpload').val('');
        $('#CodeUpload').val('');
        $scope.FileExists = false;
        $scope.CodeExists = false;
        $scope.Doc.EditOp = false;
        $scope.Doc.ContentTypeID = 0;
        //userForm.$valid = true;
        //userForm.$submitted = false;
        //$scope.validErrors = false;
        //ResertDefaults();
    }

    $scope.save = function (form) {
        form.$submitted = true;
        if ($scope.Doc.Name == "" || $scope.Doc.Description == "" || $scope.Doc.CreayedBy == "") {
            form.$valid = false;
        }
        debugger;
        $scope.validationErrors = [];
        $scope.validErrors = false;
        var isFileExist = $filter('filter')($scope.AllFileNames, { Text: $scope.Doc.Name }, true);

        if (isFileExist.length > 0 && parseInt($scope.Doc.Id) == 0) {
            $scope.validErrors = true;
            $scope.validationErrors.push("A file name already exist.");
        }

        if (form.$valid) {
            var data = new FormData();
            var Filefiles = $("#FileUpload").get(0).files;
            var Codefiles = $("#CodeUpload").get(0).files;

            if (Filefiles.length > 0) {
                data.append("UploadedFile", Filefiles[0]);
                $scope.Doc.ContentTypeID = 16;
                if (Filefiles[0].size > 5242880) {  // 1MB = 1048576 Byte
                    $scope.validErrors = true;
                    $scope.validationErrors.push("Maximum size of File is 5 MB.");
                    return false;
                }
            }
            if (Codefiles.length > 0) {
                data.append("UploadedCode", Codefiles[0]);
                $scope.Doc.ContentTypeID = 17;
                var codefilename = Codefiles[0].name;
                var extention = codefilename.substr(codefilename.lastIndexOf(".") + 1);
                if (extention != "zip") {
                    $scope.validErrors = true;
                    $scope.validationErrors.push("Only zip files are allowed for uploading code.");
                    return false;
                }
                if (Codefiles[0].size > 209715200) { // 1MB = 1048576 Byte
                    $scope.validErrors = true;
                    $scope.validationErrors.push("Maximum size of Code File is 200 MB.");
                    return false;
                }
            }

            if (Filefiles.length == 0 && Codefiles.length == 0) {
                $scope.validErrors = true;
                $scope.validationErrors.push("A file upload is required.");
                $scope.validationErrors.push("A code upload is required.");
                return false;
            }
            else if (Filefiles.length != 0 && Codefiles.length != 0) {
                $scope.Doc.ContentTypeID = 18;
            }

            //if ($scope.Doc.IsFile == true) {
            //    $scope.Doc.ContentTypeID = 16;
            //}
            //if ($scope.Doc.IsCode == true) {
            //    $scope.Doc.ContentTypeID = 17;
            //}
            //if ($scope.Doc.IsCode == true && $scope.Doc.IsFile == true) {
            //    $scope.Doc.ContentTypeID = 18;
            //}

            data.append('Id', parseInt($scope.Doc.Id));
            data.append('Name', $scope.Doc.Name);
            data.append('Description', $scope.Doc.Description);
            data.append('DocCreatedBy', $scope.Doc.CreayedBy);
            data.append('TechnologyID', parseInt($scope.Doc.SelectedTechId));
            data.append('ContentTypeID', parseInt($scope.Doc.ContentTypeID));

            var promiseGet = DocumentService.FileUploadDetail(data);

            promiseGet.success(function (result) {

                if (result == true) {
                    if ($scope.Doc.Id != 0) {
                        //notifications.showSuccess({
                        //    message: 'Your File Updated Successfully',
                        //    //hide: true //bool
                        //});
                        notificationFactory.customSuccess("Your File Updated Successfully.");
                    }
                    else {
                        //notifications.showSuccess({
                        //    message: 'Your File Added Successfully',
                        //    //hide: true //bool
                        //});
                        notificationFactory.customSuccess("Your File Added Successfully.");
                    }
                    AllFileName();
                    getAllDocuments();
                    form.$submitted = false;
                    form.$valid = true;
                    $scope.Doc.ContentTypeID = 0;
                    ClearTextDetail();
                }
            });

            promiseGet.error(function (data, statusCode) {
                //notifications.showSuccess({
                //    message: 'Your task posted successfully',
                //    hideDelay: 1500, //ms
                //    hide: true //bool
                //});
            });
        }
    }

    $scope.Clear = function (form) {
        debugger;
        form.$submitted = false;
        form.$valid = true;
        $scope.validationErrors = [];
        $scope.validErrors = false;
        if ($scope.Doc.Id == 0) {
            $scope.Doc.ContentTypeID = 0;
            $scope.Doc.Name = "";
            $scope.Doc.Description = "";
            $scope.Doc.CreayedBy = "";
            $scope.Doc.SelectedTechId = 2;
            $('#FileUpload').val('');
            $('#CodeUpload').val('');
        }
        else {
            $scope.Doc.Name = "";
            $scope.Doc.Description = "";
            $scope.Doc.CreayedBy = "";
            $scope.Doc.SelectedTechId = 2;
        }
        //ResertDefaults();
        //$scope.validErrors = false;
        //if ($scope.Doc.Id != 0) {
        //    $scope.Doc.Name = "";
        //    $scope.Doc.Description = "";
        //    $scope.Doc.CreayedBy = "";
        //    $scope.Doc.SelectedTechId = 2;
        //}
        //else {
        //    $scope.Doc.Id = 0;
        //    $scope.Doc.Name = "";
        //    $scope.Doc.Description = "";
        //    $scope.Doc.CreayedBy = "";
        //    $scope.Doc.SelectedTechId = 2;
        //    $scope.Doc.ContentTypeID = 0;
        //    $scope.Doc.EditOp = false;
        //    $scope.FileExists = false;
        //    $scope.CodeExists = false;
        //    $scope.Doc.BothFileExists = false;
        //    //ClearTextDetail();
        //}
    }

    function ClearTextDetail() {
        ResertDefaults();
        //$scope.Doc.Name = "";
        //$scope.Doc.Description = "";
        //$scope.Doc.CreayedBy = "";
        //$scope.Doc.CreayedBy = "";
        $('#FileUpload').val('');
        $('#CodeUpload').val('');
    }
    $scope.DeleteDocument = function (data) {
        var filePath = data.FilePath;
        var codePath = data.CodePath;
        var path;
        if (filePath != null && codePath != null) {
            path = filePath + "," + codePath;
        }
        else if (filePath != null && codePath == null) {
            path = filePath;
        }
        else if (filePath == null && codePath != null) {
            path = codePath;
        }

        var deleteDocument = DocumentService.DeleteDocument(path, data.Id, false, 0);
        deleteDocument.success(function (pl) {
            if (pl == true) {
                //notifications.showSuccess({
                //    message: 'Your File Deleted Successfully',
                //    //hide: true //bool
                //});
                notificationFactory.customSuccess("File Deteletd Successfully.");
                AllFileName();
                getAllDocuments();
                // ClearTextDetail();
            }
        });
        deleteDocument.error(function () {

        });
    }

});



