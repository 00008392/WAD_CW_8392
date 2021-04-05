//controller for modifying account
app.controller('EditAccountController', ['$scope', '$http', '$location', 'FacadeService', function ($scope, $http, $location, FacadeService) {
    $scope.user = {};
    //form is not displayed in the view if user is not signed in
    $scope.DisplayForm = false;
    //message displaying error
    $scope.message = "";
    //this message is displayed if user is not signed in
    $scope.info = "";
    //this is the value for save button displayed in view 
    $scope.mode = "Modify";
    FacadeService.IsLogged(function (result) {
        if (result) {
            //if user is signed in, get the current user from session storage and display user info
            $scope.user = FacadeService.GetCurrentUser();
            $scope.user.dateOfBirth = new Date($scope.user.dateOfBirth);
            $scope.DisplayForm = true;
        } else {
            $scope.info = "To edit account you should sign in first";
        }
    })
    //function for editing user
    $scope.Save = function () {
        $http.put(`api/Users`, $scope.user).then(function (response) {
            //in case of success, update the current user info in session storage and redirect to account page
            FacadeService.SetCurrentUser($scope.user);
            $location.path('/MyAccount');
        },
            function (error) {
                //display error
                $scope.message = error.data;
                //response data can be either an object (which will be displayed in ng-repeat block) or just a string
                $scope.isObject = angular.isObject(error.data);
            }
        )
    }

}])