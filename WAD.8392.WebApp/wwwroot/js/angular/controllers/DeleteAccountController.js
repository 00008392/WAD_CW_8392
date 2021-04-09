//controller for account deletion
//this controller is a child controller of AccountController, since delete action is in the same view and route
app.controller('DeleteAccountController', ['$http', '$scope', 'FacadeService', function ($http, $scope, FacadeService) {
    //function for account deletion
    $scope.Delete = function () {
        //delete user
        $http.delete(`api/users/account`).then(function (response) {
            //clear http headers and session storage from current user info
            FacadeService.Logout();
            //$parent is AccountController
            $scope.$parent.user = null;
            $scope.$parent.message = "Account is deleted";
            //disallow content display except the message
            $scope.$parent.IsLogged = false;
        })
    }
}])