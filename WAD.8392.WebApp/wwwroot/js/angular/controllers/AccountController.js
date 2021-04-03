app.controller('AccountController', ['$scope', '$http', '$location', 'AuthenticationService', 'DateConversion', function ($scope, $http, $location, AuthenticationService, DateConversion) {
    $scope.message = "";
    $scope.user = {};
    $scope.IsLogged = false;
    AuthenticationService.IsLogged(function (result) {
        if (result) {
            $scope.user = AuthenticationService.getCurrentUser();
            $scope.user.dateOfBirth = DateConversion.ConvertDate($scope.user.dateOfBirth);
            $scope.IsLogged = true;
        } else {
            $scope.message = "Sign in to see your account details";
        }
    });
    $scope.Delete = function () {
        $http.delete(`api/Users/${$scope.user.userId}`, $scope.user.userId).then(function (response) {
            AuthenticationService.Logout();
            $scope.user = null;
            $scope.message = "Account is deleted";
            $scope.IsLogged = false;
        })
    }
    $scope.SignOut = function () {
        AuthenticationService.Logout();
        $location.path('/');
    }
}])