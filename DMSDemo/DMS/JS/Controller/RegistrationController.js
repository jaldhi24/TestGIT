appLogin.controller("RegistrationController", function ($scope, RegistrationService, $state, $window) {    

    getServerName();

    function getServerName() {        
        var result = RegistrationService.GetLoggedIn();
        result.success(function (data) {
            $scope.ServerName = data;
        });
        result.error(function (data) {
            alert(Error);
        });
    }

    $scope.signUpUser = function (form) {        
        if (form.$valid) {
            var dataUser = $.param({
                UserName: $scope.UserName,
                ServerName: $scope.ServerName
            });            
            var createUser = RegistrationService.postUser(dataUser);
            createUser.success(function (data) {
                if (data) {                    
                    var loadingUrl = "http://localhost/DMS/#/DocumentDetail";
                    $window.location.href = loadingUrl;
                }
            });
            createUser.error(function (data) {
            });
        }
    }


});