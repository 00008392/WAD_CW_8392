//controller for login handling
app.controller('LoginController', ['$scope', '$location', 'FacadeService', function ($scope, $location, FacadeService) {
    $scope.login = {
        userName: null,
        password: null
    };
    //message to display error
    $scope.message = "";
    //login form is displayed when user is not signed in
    $scope.IsLogged = false;
    FacadeService.IsLogged(function (result) {
        if (result) {
            //if user is signed in, form is not displayed
            $scope.IsLogged = true;
        }
    })
    //function for login
    $scope.SignIn = function () {
        FacadeService.Login($scope.login, function (result) {
            if (result) {
                //in case of success, redirect to account page
                $location.path('/MyAccount');
            } else {
                //display error
                $scope.message = "Login failed";
            }
        })
    }
}]);