app.controller('AddProductController', ['$http', '$scope', '$location', 'AuthenticationService', 'ProductFactory', 'SelectInputHandling', function ($http, $scope, $location, AuthenticationService, ProductFactory, SelectInputHandling) {
    $scope.message = "";
    $scope.IsLogged = false;
    $scope.editMode = false;

    $scope.productInfo = {};
    ProductFactory.prepareProductInfo().then(function (data) {
        $scope.productInfo = data;
    })

    AuthenticationService.IsLogged(function (result) {
        if (result) {
            $scope.IsLogged = true;
        }
    })
    $scope.onChange = SelectInputHandling.onSelectChange;
    $scope.product = {};
    $scope.Save = function () {
        $http.post("api/Products", $scope.product).then(function (response) {
            $location.path('/MyProducts');
        }, function (error) {
            $scope.message = error.data;
        })
    }
}])