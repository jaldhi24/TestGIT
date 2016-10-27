appLogin.service("RegistrationService", function ($http) {

    this.GetLoggedIn = function () {

        var request = $http({
            method: "GET",
            url: "GetLoggedInUser",
            async: false
        });
        return request;
    }

    //this.GetLoggedIn=function()
    //{    
    //    return $http.get('Home/GetLoggedInUser');
    //}

    this.GetUserDetailByUsername = function (username) {
        return $http.get('api/UserApi/GetUserDetailByNetworkUserId?id=' + username);
    }

    this.postUser = function (userEntity) {
        
        var request = $http({
            method: "post",
            url: "http://localhost/DMS/api/UserApi/PostUser",
            data: userEntity,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
            }
        });
        return request;
        //var ajaxRequest = $.ajax({
        //    url: 'api/UserApi/PostUser',
        //    type: 'POST',
        //    dataType: 'json',
        //    data: userEntity,
        //    processData: false,
        //    contentType: false,// not json            
        //});

        //return ajaxRequest;
    }
});