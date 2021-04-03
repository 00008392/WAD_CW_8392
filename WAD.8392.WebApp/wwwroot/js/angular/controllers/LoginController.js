app.controller('LoginController', ['$scope', '$location', 'AuthenticationService', function ($scope, $location, AuthenticationService) {
    $scope.login = {
        userName: null,
        password: null
    };
    $scope.message = "";
    $scope.IsLogged = false;
    AuthenticationService.IsLogged(function (result) {
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