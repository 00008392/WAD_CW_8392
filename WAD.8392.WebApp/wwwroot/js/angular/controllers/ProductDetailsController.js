app.controller('ProductDetailsController', ['$scope', '$http', '$routeParams', 'DateConversion', function ($scope, $http, $routeParams, DateConversion) {
    $scope.product = null;
    $scope.message = "";
    $http.get(`api/Products/${$routeParams.ProductId}`).then(function (response) {
        $scope.product = response.data;
        $scope.product.datePublished = DateConversion.ConvertDate($scope.product.datePublished);
    },
        function (error) {
            $scope.message = error.data.title;
        })
}])