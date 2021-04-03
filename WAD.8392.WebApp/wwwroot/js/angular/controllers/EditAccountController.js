app.controller('EditAccountController', ['$scope', '$http', '$location', 'AuthenticationService', function ($scope, $http, $location, AuthenticationService) {
    $scope.user = {};
    $scope.DisplayForm = false;
    $scope.message = "";
    $scope.info = "";
    $scope.mode = "Modify";
    AuthenticationService.IsLogged(function (result) {
        if (result) {
            $scope.user = AuthenticationService.getCurrentUser();
            $scope.user.dateOfBirth = new Date($scope.user.dateOfBirth);
            $scope.DisplayForm = true;
        } else {
            $scope.info = "To edit account you should sign in first";
        }
    })
    $scope.Save = function () {
        $http.put(`api/Users/${$scope.user.userId}`, $scope.user).then(function (response) {
            AuthenticationService.setCurrentUser($scope.user);
            $location.path('/MyAccount');
        },
            function (error) {
                $scope.message = error.data;
            }
        )
    }

}])