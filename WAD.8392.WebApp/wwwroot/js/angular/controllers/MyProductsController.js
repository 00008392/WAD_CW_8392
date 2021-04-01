app.controller('MyProductsController', ['$scope', '$http', 'AuthenticationCheck', 'DateConversion', function ($scope, $http, AuthenticationCheck, DateConversion) {
    $scope.message = "";
    $scope.products = [];
    $scope.IsLogged = false;
    $scope.deleted = false;
    AuthenticationCheck.IsLogged(function (result) {
        if (result) {
            $scope.IsLogged = true;
            $scope.CurrentUser = JSON.parse(sessionStorage.getItem('current_user'));
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
            $scope.deleted = true;
            var index = $scope.products.indexOf(product);
            $scope.products.splice(index, 1);
        })
    }
}])