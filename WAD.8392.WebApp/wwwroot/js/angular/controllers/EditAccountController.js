app.controller('EditAccountController', ['$scope', '$http', '$location', 'AuthenticationCheck', function ($scope, $http, $location, AuthenticationCheck) {
    $scope.user = null;
    $scope.DisplayForm = false;
    $scope.message = "";
    $scope.info = "";
    $scope.mode = "Modify";
    AuthenticationCheck.IsLogged(function (result) {
        if (result) {
            $scope.user = JSON.parse(sessionStorage.getItem('current_user'));
            $scope.user.dateOfBirth = new Date($scope.user.dateOfBirth);
            $scope.DisplayForm = true;
        } else {
            $scope.info = "To edit account you should sign in first";
        }
    })
    $scope.Save = function () {
        $http.put(`api/Users/${$scope.user.userId}`, $scope.user).then(function (response) {
            sessionStorage.setItem('current_user', JSON.stringify($scope.user));
            $location.path('/MyAccount');
        },
            function (error) {
                $scope.message = error.data.title;
            }
        )
    }
}])