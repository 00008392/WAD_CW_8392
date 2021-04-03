app.controller('UserDetailsController', ['$scope', '$http', '$routeParams', 'DateConversion', function ($scope, $http, $routeParams, DateConversion) {
    $scope.user = {};
    $scope.products = [];
    $scope.message = "";
    $http.get(`api/Users/${$routeParams.UserId}`).then(function (response) {
        $scope.user = response.data;
        $scope.user.dateOfBirth = DateConversion.ConvertDate($scope.user.dateOfBirth);
        $http.get(`api/Products?user=${$scope.user.userId}`).then(function (response) {
            $scope.products = response.data;
            $scope.products.forEach(product => {
                product.datePublished = DateConversion.ConvertDate(product.datePublished);
            })
        })
    },
        function (error) {
            $scope.message = error.data.title;
        })
}])