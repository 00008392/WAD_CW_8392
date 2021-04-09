//controller for displaying list of users
app.controller('UsersController', ['$scope', '$http', 'FacadeService', function ($scope, $http, FacadeService) {
    $scope.users = [];
    $http.get("api/users").then(function (response) {
        //get the users and display them in the view
        $scope.users = response.data;
        //display date of birth in user friendly way
        $scope.users.forEach(function (user) {
            user.dateOfBirth = FacadeService.ConvertDate(user.dateOfBirth);
        })
    })
}]);