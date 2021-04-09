//controller for product deletion
//this controller is a child controller of MyProductsController, since delete action is in the same view and route
app.controller('DeleteProductController', ['$scope', '$http', function ($scope, $http) {
    //function for product deletion
    $scope.Delete = function (product) {
        $http.delete(`api/products/${product.productId}`).then(function (response) {
            //in case of success, remove the product from product list
            var index = $scope.$parent.products.indexOf(product);
            $scope.$parent.products.splice(index, 1);
        })
    }
}])