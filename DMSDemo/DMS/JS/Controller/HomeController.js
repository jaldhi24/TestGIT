app.controller("HomeController", function ($scope, $http, $state, HomeService, $rootScope, localStorageService) {    
    $rootScope.UserName = null;
    $scope.UserImage = null;
    $scope.NetworkUserId = null;
    $scope.UserLoginId = null;
    $scope.user = {};
    //$scope.IsAdmin = true;

    if (localStorageService.get("loggedInUser") === null) {
        var result = HomeService.GetLoggedIn();
        
        result.success(function (data) {
            
            var userresult = HomeService.GetUserDetailByUsername(data);
            userresult.success(function (data) {
                debugger;
                if (localStorageService.isSupported) {
                    localStorageService.remove("loggedInUser");
                    localStorageService.set("loggedInUser", data);
                }
                debugger;
                $rootScope.UserName = data.UserName;
                $scope.NetworkUserId = data.ServerName;
                $scope.UserLoginId = data.Id;

                var userAuth = HomeService.GetUserDetails($scope.NetworkUserId);
                userAuth.success(function (data, textStatus, xhr) {
           
                    var tkn = xhr("Token");

                    if (localStorageService.isSupported) {
                        localStorageService.remove("ApiToken");
                        localStorageService.set("ApiToken", tkn);
                    }

                    $http.defaults.headers.common['Token'] = tkn;
                    $state.go('DocumentDetail');
                });
                userAuth.error(function () {
                    alert("Kaik Locha :P ");
                });
            });
            userresult.error(function (data, statusCode) {
                //exceptionService.ShowException(data, statusCode);
            });
        });

        result.error(function (data, statusCode) {
            //exceptionService.ShowException(data, statusCode);
        });
    }
    else {
        var loggedInUser = localStorageService.get("loggedInUser");
        if (localStorage != null) {
            $scope.UserName = loggedInUser.UserName;
        }
    }

  
   

});

  