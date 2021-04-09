//controller for displaying product details
app.controller('ProductDetailsController', ['$scope', '$http', '$routeParams', 'FacadeService', function ($scope, $http, $routeParams, FacadeService) {
    $scope.product = null;
    //message to display error
    $scope.message = "";
    $http.get(`api/products/${$routeParams.ProductId}`).then(function (response) {
        //if product exists, display product info in the view
        $scope.product = response.data;
        //display date published in user friendly way
        $scope.product.datePublished = FacadeService.ConvertDate($scope.product.datePublished);
    },
        function (error) {
            //display error
            $scope.message = error.data;
        })
}])