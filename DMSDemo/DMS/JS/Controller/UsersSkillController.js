app.controller("UsersSkillController", function ($scope, $compile, $q, UsersSkillService, SkillService, HomeService, $filter, notificationFactory, $rootScope) {
    if (angular.isDefined($rootScope.UserName)) {
    }

    var selected = $scope.selected = [];
    var Skillselected = $scope.Skillselected = [];
    $scope.AllTechnology = [];
    $scope.AllSkillByTech = [];
    var dataRadioTechnology = [];
    $scope.UserSkill = [];
    $scope.addNewUserSkillDisplay = false;
    $scope.formUserSkill = false;

    $scope.SkillDiv = false;

    AllSkillDetails();
    AllUserSkillDetails();

    function AllSkillDetails() {
        var Skills = SkillService.getAllSkills();
        Skills.success(function (pl) {
            $scope.AllSkills = pl;

            for (var i = 0; i < $scope.AllSkills.length; i++) {
                dataRadioTechnology.push(
                    { id: $scope.AllSkills[i].Id, TechName: $scope.AllSkills[i].Name });
            }
            $scope.AllTechnology = dataRadioTechnology;

        });
    }

    $scope.AddNewUserSkills = function () {
        $scope.formUserSkill = true;
        $scope.userSkillList = false;
    }



    function AllUserSkillDetails() {
        var getAllUserSkills = UsersSkillService.GetAllUserSkill();
        getAllUserSkills.success(function (pl) {
            debugger;
            $scope.UserSkill = pl;
            $scope.userSkillList = true;
            if ($scope.UserSkill != null) {
                $scope.addNewUserSkillDisplay = false;
                $scope.formUserSkill = false;
            }
            else {
                $scope.addNewUserSkillDisplay = true;
                $scope.formUserSkill = false;
            }

            if ($.fn.DataTable.isDataTable("#tblUserSkills")) {
                $('#tblUserSkills').DataTable().destroy();
            }

            $('#tblUserSkills').DataTable({
                data: pl,
                "bDestroy": true,
                //"dom": '<"top"f><"table-responsive"rt><"bottom"lip<"clear">>',
                "aLengthMenu": [10, 20, 50, 100, 200],
                "pageLength": 20,
                "columns": [
                { "title": "Technology Name", "data": "TechName" },
                { "title": "Skills", "data": "SkillName" }
                ],
                "initComplete": function () {
                    var dataTable = $('#tblUserSkills').DataTable();
                    //BindCustomerSearchBar($scope, $compile, dataTable);
                    $compile(angular.element("#tblUserSkills").contents())($scope);
                }
            });
        });
        getAllUserSkills.error(function (pl) {
        });
    }

    $scope.EditUserSkills = function () {
        $scope.formUserSkill = true;
        $scope.userSkillList = false;
        debugger;
        var action = 'add';
        for (var i = 0; i < $scope.UserSkill.length; i++) {
            var entity = $scope.UserSkill[i];
            updateSelected(action, entity.TechID);
        }

        GetSkillList($scope.selected);

        var eachSkillIds;
        for (var i = 0; i < $scope.UserSkill.length; i++) {
            debugger;
            if ($scope.UserSkill[i].SkillId != null) {
                eachSkillIds = $scope.UserSkill[i].SkillId.split(",");
                for (var j = 0; j < eachSkillIds.length; j++) {
                    var entity = eachSkillIds[j];
                    updateSkillSelected(action, parseInt(entity));
                }
            }
        }
    }

    $scope.Cancel = function () {
        $scope.userSkillList = true;
        $scope.formUserSkill = false;

        //return $scope.isSelected(AllTechnology.id) ? 'selected' : '';
    }

    var updateSelected = function (action, id) {
        debugger;
        if (action === 'add' && $scope.selected.indexOf(id) === -1) {
            $scope.selected.push(id);
        }
        if (action === 'remove' && $scope.selected.indexOf(id) !== -1) {
            $scope.selected.splice($scope.selected.indexOf(id), 1);

            if ($scope.UserSkill != null) {
                for (var i = 0; i < $scope.UserSkill.length; i++) {
                    if ($scope.UserSkill[i].TechID == id) {
                        if ($scope.UserSkill[i].SkillId != null) {
                            var eachSkillIds;
                            eachSkillIds = $scope.UserSkill[i].SkillId.split(",");
                            for (var j = 0; j < eachSkillIds.length; j++) {
                                $scope.Skillselected.splice($scope.Skillselected.indexOf(id), 1);
                            }
                        }
                    }
                }
            }


        }
    };

    $scope.updateSelection = function ($event, id) {
        debugger;
        var checkbox = $event.target;
        var action = (checkbox.checked ? 'add' : 'remove');
        updateSelected(action, id);
        GetSkillList($scope.selected);
    };

    $scope.selectAll = function ($event) {
        debugger;
        var checkbox = $event.target;
        var action = (checkbox.checked ? 'add' : 'remove');
        for (var i = 0; i < $scope.AllTechnology.length; i++) {
            var entity = $scope.AllTechnology[i];
            updateSelected(action, entity.id);
        }
        GetSkillList($scope.selected);
    };

    $scope.getSelectedClass = function (AllTechnology) {
        debugger;
        return $scope.isSelected(AllTechnology.id) ? 'selected' : '';
    };

    $scope.isSelected = function (id) {

        debugger;
        return $scope.selected.indexOf(id) >= 0;
    };

    //something extra I couldn't resist adding :):)
    $scope.isSelectedAll = function () {
        debugger;
        return $scope.selected.length === $scope.AllTechnology.length;
    };

    var GetSkillList = function (TechIds) {
        debugger;
        var SkillPromise = $q.defer();
        var SkillPromise = UsersSkillService.getSkillByTechnology(TechIds);
        SkillPromise.success(function (pl) {
            $scope.AllSkillByTech = pl;
        });
        SkillPromise.error(function (pl) {
        });
    };



    var updateSkillSelected = function (action, id) {
        debugger;
        if (action === 'add' && $scope.Skillselected.indexOf(id) === -1) {
            $scope.Skillselected.push(id);
        }
        if (action === 'remove' && $scope.Skillselected.indexOf(id) !== -1) {
            $scope.Skillselected.splice($scope.Skillselected.indexOf(id), 1);
        }
    };

    $scope.updateSkillSelection = function ($event, id) {
        debugger;
        var checkbox = $event.target;
        var action = (checkbox.checked ? 'add' : 'remove');
        updateSkillSelected(action, id);
    };

    $scope.SkillselectAll = function ($event) {
        debugger;
        var checkbox = $event.target;
        var action = (checkbox.checked ? 'add' : 'remove');
        for (var i = 0; i < $scope.AllSkillByTech.length; i++) {
            var entity = $scope.AllSkillByTech[i];
            updateSkillSelected(action, entity.Id);
        }

    };

    $scope.getSkillSelectedClass = function (AllSkillByTech) {
        debugger;
        return $scope.isSkillSelected(AllSkillByTech.Id) ? 'selected' : '';
    };

    $scope.isSkillSelected = function (id) {

        debugger;
        return $scope.Skillselected.indexOf(id) >= 0;
    };

    //something extra I couldn't resist adding :):)
    $scope.isSkillSelectedAll = function () {
        debugger;
        if ($scope.AllSkillByTech.length > 0) {
            $scope.SkillDiv = true;
            return $scope.Skillselected.length === $scope.AllSkillByTech.length;
        }
        else {
            $scope.SkillDiv = false;
            return false;
        }
    };
});