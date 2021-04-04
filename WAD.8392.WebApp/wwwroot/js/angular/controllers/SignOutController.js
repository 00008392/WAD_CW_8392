//controller for signing user out
//this controller is a child controller of AccountController, since sign in action is in the same view and route
app.controller('SignOutController', ['$scope', '$location', 'FacadeService', function ($scope, $location, FacadeService) {
    //function for signing out
    $scope.SignOut = function () {
        //clear session storage from current user and go to the main page
        FacadeService.Logout();
        $location.path('/');
    }
}])