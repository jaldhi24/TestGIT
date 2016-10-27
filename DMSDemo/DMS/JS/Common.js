app.factory('notificationFactory', function () {
    /// <summary>
    /// s the specified local storage messeage.
    /// </summary>
    /// <param name="localStorageMesseage">The local storage messeage.</param>
    /// <returns></returns>
    return {
        customSuccess: function (text) {
            toastr.success(text);
        },
        successSave: function (text) {
            toastr.success(text + "Added Successfully");
        },
        successUpdate: function (text) {
            toastr.success("Hello");
        },
        successDelete: function (text) {
            toastr.success(text, "Hi");
        },
        WarningNoFile: function (text) {
            toastr.warning(text);
        }
    }
});

app.directive('dropdownMultiselect', function () {
    return {
        restrict: 'E',
        scope: {
            model: '=',
            options: '=',
            selected: '='
        },
        template:
                "<div class='btn-group multiselectDropdown' data-ng-class='{open: open}'>" +
                    "<button class='btn btn-small' data-ng-click='openDropdown()' type='button'>Select...</button>" +
                    "<button class='btn btn-smalldropdown-toggle'  type='button' data-ng-click='openDropdown()'><span class='caret'></span></button>" +
    "<ul class='dropdown-menu' aria-labelledby='dropdownMenu' style='max-height: 300px;overflow: scroll;'>" + "<li><a data-ng-click='selectAll()'><span class='glyphicon glyphicon-ok green' aria-hidden='true'></span> Check All</a></li>" +
    "<li><a data-ng-click='deselectAll();'><span class='glyphicon glyphicon-remove red' aria-hidden='true'></span> Uncheck All</a></li>" +
    "<li class='divider'></li>" + "<li data-ng-repeat='option in options'><a data-ng-click='toggleSelectItem(option)'><span data-ng-class='getClassName(option)' aria-hidden='true'></span> {{option.Name}}</a></li>" +
    "</ul>" +
"</div><strong class='margin-left-5'>{{selected}} Selected</strong>",
        controller: function ($scope) {

            $scope.openDropdown = function () {
                $scope.open = !$scope.open;
            };

            $scope.selectAll = function () {
                $scope.model = [];
                angular.forEach($scope.options, function (item, index) {
                    $scope.model.push(item.Id);
                });
            };

            $scope.deselectAll = function () {
                $scope.model = [];
            };

            $scope.toggleSelectItem = function (option) {

                var intIndex = -1;
                angular.forEach($scope.model, function (item, index) {
                    if (item == option.Id) {
                        intIndex = index;
                    }
                });

                if (intIndex >= 0) {
                    $scope.model.splice(intIndex, 1);
                }
                else {
                    $scope.model.push(option.Id);
                }
            };

            $scope.getClassName = function (option) {

                var varClassName = 'glyphicon glyphicon-remove red';
                angular.forEach($scope.model, function (item, index) {
                    if (item == option.Id) {
                        varClassName = 'glyphicon glyphicon-ok green';
                    }
                });
                return (varClassName);
            };
        }
    }
});
