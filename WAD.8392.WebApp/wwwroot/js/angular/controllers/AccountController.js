//controller for account manipulations
app.controller('AccountController', ['$scope', '$http', '$location', 'FacadeService', function ($scope, $http, $location, FacadeService) {
    //the message is displayed when account is deleted or when user is not signed in
    $scope.message = "";
    $scope.user = {};
    //when this variable is set to false, no content except the message is displayed in view
    $scope.IsLogged = false;
    //if user is signed in, then get the current user from local storage and display it in the view
    FacadeService.IsLogged(function (result) {
        if (result) {
            $scope.user = FacadeService.GetCurrentUser();
            //display date of birth in user friendly format
            $scope.user.dateOfBirth = FacadeService.ConvertDate($scope.user.dateOfBirth);
            //display view content
            $scope.IsLogged = true;
        } else {
            $scope.message = "Sign in to see your account details";
        }
    });
    //function for account deletion
    $scope.Delete = function () {
        //delete user
        $http.delete(`api/Users`).then(function (response) {
            //clear http headers and session storage from current user info
            FacadeService.Logout();
            $scope.user = null;
            $scope.message = "Account is deleted";
            //disallow content display except the message
            $scope.IsLogged = false;
        })
    }
    //function for signing out
    $scope.SignOut = function () {
        //clear session storage from current user and go to the main page
        FacadeService.Logout();
        $location.path('/');
    }
}])