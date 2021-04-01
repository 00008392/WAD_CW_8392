app.controller('UsersController', ['$scope', '$http', 'DateConversion', function ($scope, $http, DateConversion) {
    $scope.users = [];
    $http.get("api/Users").then(function (response) {
        $scope.users = response.data;
        $scope.users.forEach(function (user) {
            user.dateOfBirth = DateConversion.ConvertDate(user.dateOfBirth);
        })
    })
}]);