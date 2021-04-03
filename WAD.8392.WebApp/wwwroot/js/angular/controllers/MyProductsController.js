app.controller('MyProductsController', ['$scope', '$http', 'AuthenticationService', 'DateConversion', function ($scope, $http, AuthenticationService, DateConversion) {
    $scope.message = "";
    $scope.products = [];
    $scope.IsLogged = false;
    AuthenticationService.IsLogged(function (result) {
        if (result) {
            $scope.IsLogged = true;
            $scope.CurrentUser = AuthenticationService.getCurrentUser();
            $http.get(`api/Products?user=${$scope.CurrentUser.userId}`).then(function (response) {
                $scope.products = response.data;
                $scope.products.forEach(product => {
                    product.datePublished = DateConversion.ConvertDate(product.datePublished);
                })
            })
        } else {
            $scope.message = "Sign in to see your products";
        }
    })
    $scope.Delete = function (product) {
        $http.delete(`api/Products/${product.productId}`).then(function (response) {
            var index = $scope.products.indexOf(product);
            $scope.products.splice(index, 1);
        })
    }
}])