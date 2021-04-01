app.service('AuthenticationService', ['$http', 'AuthenticationCheck', function ($http, AuthenticationCheck) {
    var service = {};
    service.Login = function (login, callback) {
        AuthenticationCheck.IsLogged(function (result) {
            if (result) {
                callback(false);
            } else {
                $http.post("api/Authentication", login).then(
                    function (response) {
                        sessionStorage.setItem('token', response.data);
                        $http.defaults.headers.common.Authorization = 'Bearer ' + response.data;
                        $http.get("api/Users/Account").then(function (response) {
                            sessionStorage.setItem('current_user', JSON.stringify(response.data));
                            callback(true);
                        })
                    },
                    function (error) {
                        callback(false);
                    })
            }
        })
    }
    service.Logout = function () {
        sessionStorage.clear();
        $http.defaults.headers.common.Authorization = '';
    }
    return service;
}])