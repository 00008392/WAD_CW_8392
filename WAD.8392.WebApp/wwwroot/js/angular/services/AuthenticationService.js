//service for handling logins
app.service('AuthenticationService', ['$http', function ($http) {
    var service = {};
    //Checks if user is signed in and, if so, ensures that http authorization header is not empty
    service.IsLogged = function (callback) {
        //if session storage is not empty, user is signed in
        if (service.getCurrentUser()) {
            //if the authorization header is empty, JWT token of logged user is assigned to header
            //this manipulation is necessary because when browser page gets refreshed, the information (JWT token) in $http.defaults.headers is lost
            //even if user is signed in (current_user is not null in sessionStorage), which makes it impossible to access api with authorization filter
            if (!$http.defaults.headers.common.Authorization) {
                $http.defaults.headers.common.Authorization = 'Bearer ' + sessionStorage.getItem('token');
            }
            callback(true);
        } else {
            callback(false);
        }
    }
    //function for signing user in
    service.Login = function (login, callback) {
        //if user is already signed in, no actions are taken
        service.IsLogged(function (result) {
            if (result) {
                callback(false);
            } else {
                $http.post("api/Authentication", login).then(
                    function (response) {
                        //if credentials are correct, store user in sessionStorage along with token
                        sessionStorage.setItem('token', response.data);
                        $http.defaults.headers.common.Authorization = 'Bearer ' + response.data;
                        $http.get("api/Users/Account").then(function (response) {
                            service.setCurrentUser(response.data);
                            callback(true);
                        })
                    },
                    function (error) {
                        callback(false);
                    })
            }
        })
    }
    //function for signing user out
    service.Logout = function () {
        sessionStorage.clear();
        $http.defaults.headers.common.Authorization = '';
    }
    service.getCurrentUser = function () {
       return JSON.parse(sessionStorage.getItem('current_user'));
    }
    service.setCurrentUser = function (user) {
        sessionStorage.setItem('current_user', JSON.stringify(user))
    }
    return service;
}])