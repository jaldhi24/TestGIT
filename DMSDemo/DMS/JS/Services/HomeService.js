app.service("HomeService", function ($http) {

    this.GetLoggedIn = function () {
        var request = $http({
            method: "GET",
            url: "Home/GetLoggedInUser",
            async: false
        });
        return request;
    }    

    this.GetUserDetailByUsername = function (username) {
        return $http.get('api/UserApi/GetUserDetailByNetworkUserId?id=' + username);
    }

    this.GetUserDetails = function (servername) {
        return $http.post('api/UserApi/Authenticate?serverName=' + servername);
    }
});