//service that checks if the authorization header is empty and assigns JWT token of logged user if empty
//this manipulation is necessary because when browser page gets refreshed, the information (JWT token) in $http.defaults.headers is lost
//even if user is signed in (current_user is not null in sessionStorage), which makes it impossible to access api with authorization filter
app.service('HttpHeadersCheck', ['$http', function ($http) {
    var service = {};
    service.CheckHeaders = function () {
        if (!$http.defaults.headers.common.Authorization) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + sessionStorage.getItem('token');
        }
    }

    return service;
}]);