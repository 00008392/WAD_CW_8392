//service that checks whether the user is signed in
app.service('AuthenticationCheck', ['HttpHeadersCheck', function (HttpHeadersCheck) {
    var service = {};
    service.IsLogged = function (callback) {
        //if session storage is not empty, user is signed in
        if (sessionStorage.getItem('current_user')) {
            HttpHeadersCheck.CheckHeaders();
            callback(true);
        } else {
            callback(false);
        }
    }
    return service;
}]);