app.controller("SkillController", function ($scope, $compile, $q, SkillService, $filter, notificationFactory) {

    $scope.addnewSkill = false;
    $scope.skillDiv = false;
    $scope.messageDisplay = false;
    $scope.EditMode = false;
    $scope.listDisplay = true;
    $scope.validErrors = false;
    $scope.validationErrors = [];
    $scope.saveBtn = true;

    AllSkillDetails();

    function AllSkillDetails() {
        var Skills = SkillService.getAllSkills();
        Skills.success(function (pl) {            
            $scope.AllSkills = pl;

            var dataRadioTechnology = new Array();
            for (var i = 0; i < $scope.AllSkills.length; i++) {
                dataRadioTechnology.push(
                    { id: $scope.AllSkills[i].Id, TechName: $scope.AllSkills[i].Name });
            }            
            $scope.AllTechnology = dataRadioTechnology;

            if ($.fn.DataTable.isDataTable("#tblSkills")) {
                $('#tblSkills').DataTable().destroy();
            }

            $('#tblSkills').DataTable({
                data: pl,
                "bDestroy": true,
                //"dom": '<"top"f><"table-responsive"rt><"bottom"lip<"clear">>',
                "aLengthMenu": [10, 20, 50, 100, 200],
                "pageLength": 20,
                "columns": [
                     {
                         "title": "Id",
                         "data": "Id"
                     },
                { "title": "Technology Name", "data": "Name" },
                { "title": "Skill Name", "data": "SkillsNamesString" },
                {
                    "title": "Action",
                    "data": null,
                    "sClass": "action",
                    "render": function (data, type, row) {
                        return '<a ng-click=\'EditSkill(' + JSON.stringify(row) + ')\'><img class="cursor-pointer" src="Images/ic-edit.png" data-toggle="tooltip" title="Edit Skills"></a>'
                    }
                }
                ],
                "initComplete": function () {
                    var dataTable = $('#tblSkills').DataTable();                    
                    //BindCustomerSearchBar($scope, $compile, dataTable);
                    $compile(angular.element("#tblSkills").contents())($scope);
                }
            });
        });
        Skills.error(function (pl) {
        });
    }

    $scope.DisplayList = function () {
        AllSkillDetails();
        $scope.listDisplay = true;
        $scope.EditMode = false;
    }

    $scope.EditSkill = function (row) {
        $scope.validationErrors = [];
        $scope.validErrors = false;
        $scope.listDisplay = false;
        $scope.EditMode = true;
        $scope.skillDiv = true;
        //alert(row.IdOfSkillNames);
        var dataSkillsId;
        var dataSkillsName;
        if (row.IdOfSkillNames != null) {
            dataSkillsId = row.IdOfSkillNames.split(",");
        }
        if (row.SkillsNamesString != null) {
            dataSkillsName = row.SkillsNamesString.split(",");
        }
        var wholeArray = new Array();
        if (dataSkillsId != null) {
            for (var i = 0; i < dataSkillsId.length; i++) {
                wholeArray.push({ id: dataSkillsId[i], SkillName: dataSkillsName[i].trim() });
            }
            $scope.messageDisplay = false;
            $scope.saveBtn = true;
        }
        else {
            $scope.messageDisplay = true;
            $scope.skillDiv = false;
            $scope.messageNoRecords = "No Records Found For " + row.Name + " Technology..";
            $scope.saveBtn = false;
        }

        $scope.skillsInTechnology = wholeArray;        
        $scope.selectedTechName = row.Id;
        $scope.selectedTech = row.Name;
    }

    $scope.AddNewSkill = function () {
        $scope.addnewSkill = true;
    }

    $scope.AddNewSkillInTechnology = function () {        
        $scope.skillDiv = true;
        $scope.addnewSkill = true;
        $scope.saveBtn = true;
    }

    $scope.AddNewSkillData = function () {
        if ($scope.NewSkillName != null && $scope.NewSkillName != "" && angular.isDefined($scope.NewSkillName) != false) {            
            var skillAvailable = $filter('filter')($scope.skillsInTechnology, { SkillName: $scope.NewSkillName }, false);
            if (skillAvailable.length > 0) {
                alert("Duplicate value");
                $scope.validationErrors = [];
                $scope.validErrors = true;
                $scope.validationErrors.push("This Skill is already available. Please Enter another Skill.");
            }
            else {
                $scope.validationErrors = [];
                $scope.validErrors = false;
                $scope.skillsInTechnology.push({ id: 0, SkillName: $scope.NewSkillName });
            }
            $scope.addnewSkill = false;
            $scope.NewSkillName = "";            
            //$scope.validErrors = false;
        }
        else {
            $scope.validErrors = true;
            $scope.validationErrors = [];
            $scope.validationErrors.push("Please Enter any Skill.");
        }
        //var dataAdd = $scope.skillsInTechnology;


        // $scope.dataWholeforUpdate = dataAdd;
    }

    $scope.deleteSkill = function (skillName, skillId) {        
        alert(skillName);
        alert(skillId);
        if (skillId != 0) {

            var index = -1;
            var comArr = eval($scope.skillsInTechnology);
            for (var i = 0; i < comArr.length; i++) {
                if (comArr[i].SkillName == skillName) {
                    index = i;
                    break;
                }
            }
            if (index === -1) {
                alert("Something gone wrong");
            }
            $scope.skillsInTechnology.splice(index, 1);

            var deleteSkill = SkillService.DeleteSkill(skillId);
            deleteSkill.success(function (pl) {
                if (pl) {
                    AllSkillDetails();
                    //$scope.listDisplay = true;
                    // $scope.EditMode = false;
                    //$scope.skillDiv = false;
                    // $scope.selectedTechName = "";
                }
            });
            deleteSkill.error(function (pl) {
            });
        }
        else {            
            var index = -1;
            var comArr = eval($scope.skillsInTechnology);
            for (var i = 0; i < comArr.length; i++) {
                if (comArr[i].SkillName == skillName) {
                    index = i;
                    break;
                }
            }
            if (index === -1) {
                alert("Something gone wrong");
            }
            $scope.skillsInTechnology.splice(index, 1);
        }
    }

    $scope.saveSkills = function () {
        var dataFinal = "";
        for (var i = 0; i < $scope.skillsInTechnology.length; i++) {
            
            if ($scope.skillsInTechnology[i].id == 0) {
                dataFinal = dataFinal + $scope.skillsInTechnology[i].SkillName + ",";
            }
        }

        if (dataFinal == "") {
            $scope.validationErrors = [];
            $scope.validErrors = true;
            $scope.validationErrors.push("Please add new Skill in Technology.");
        }
        else {
            var skillsForSave = SkillService.SaveSkills($scope.selectedTechName, dataFinal);
            skillsForSave.success(function () {                
                notificationFactory.customSuccess("Skill Added Successfully.");
                AllSkillDetails();
                $scope.EditMode = false;
                $scope.listDisplay = true;
            });
            skillsForSave.error(function (pl) {
            });
        }
    }
});