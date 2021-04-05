//controller handling user registration
app.controller('RegisterController', ['$scope', '$http', '$location', 'FacadeService', function ($scope, $http, $location, FacadeService) {
    $scope.user = {};
    //mode is displayed as value in save button in the view
    $scope.mode = "Register";
    //form is displayed if user is not signed in
    $scope.DisplayForm = true;
    //message that is displayed when user is signed in
    $scope.info = "";
    //message to display error
    $scope.message = "";
    //if user is signed in, form is not displayed
    FacadeService.IsLogged(function (result) {
        if (result) {
            $scope.info = "You are already registered";
            $scope.DisplayForm = false;
        }
    })
    //function to register user
    $scope.Save = function () {
        $http.post("api/Users", $scope.user).then(function (response) {
            //in case of success, automatically sign in
            $scope.login = {
                userName: $scope.user.userName,
                password: $scope.user.password
            }
            FacadeService.Login($scope.login, function (result) {
                if (result) {
                    //in case of success, redirect to account page
                    $location.path('/MyAccount');
                } else {
                    //display error
                    $scope.message = "Something went wrong";
                }
            })
        }, function (error) {
                //display error
                $scope.message = error.data;
                //response data can be either an object (which will be displayed in ng-repeat block) or just a string
                $scope.isObject = angular.isObject(error.data);
        });
    }
}])