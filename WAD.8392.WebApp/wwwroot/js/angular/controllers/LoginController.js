app.controller('LoginController', ['$scope', '$location', 'AuthenticationService', "AuthenticationCheck", function ($scope, $location, AuthenticationService, AuthenticationCheck) {
    $scope.login = {
        userName: null,
        password: null
    };
    $scope.message = "";
    $scope.IsLogged = false;
    AuthenticationCheck.IsLogged(function (result) {
        if (result) {
            $scope.IsLogged = true;
        }
    })
    $scope.SignIn = function () {
        AuthenticationService.Login($scope.login, function (result) {
            if (result) {
                $location.path('/MyAccount');
            } else {
                $scope.message = "Login failed";
            }
        })
    }
}]);