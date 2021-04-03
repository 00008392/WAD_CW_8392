app.controller('RegisterController', ['$scope', '$http', '$location', 'AuthenticationService', function ($scope, $http, $location, AuthenticationService) {
    $scope.user = {};
    $scope.mode = "Register";
    $scope.DisplayForm = true;
    $scope.info = "";
    $scope.message = "";
    AuthenticationService.IsLogged(function (result) {
        if (result) {
            $scope.info = "You are already registered";
            $scope.DisplayForm = false;
        }
    })
    $scope.Save = function () {
        $http.post("api/Users", $scope.user).then(function (response) {
            $scope.login = {
                userName: $scope.user.userName,
                password: $scope.user.password
            }
            AuthenticationService.Login($scope.login, function (result) {
                if (result) {
                    $location.path('/MyAccount');
                } else {
                    $scope.message = "Something went wrong";
                }
            })
        }, function (error) {
            $scope.message = error.data;
        });
    }
}])