app.controller('RegisterController', ['$scope', '$http', '$location', 'AuthenticationService', 'AuthenticationCheck', function ($scope, $http, $location, AuthenticationService, AuthenticationCheck) {
    $scope.user = {
        id: 0,
        firstName: '',
        lastName: '',
        phoneNumber: null,
        dateOfBirth: null,
        email: null,
        userName: '',
        password: ''
    }
    $scope.mode = "Register";
    $scope.DisplayForm = true;
    $scope.info = "";
    $scope.message = "";
    AuthenticationCheck.IsLogged(function (result) {
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