app.controller('EditProductController', ['$scope', '$http', '$routeParams', '$location', '$q', 'AuthenticationService', 'ProductFactory', 'SelectInputHandling', function ($scope, $http, $routeParams, $location, $q, AuthenticationService, ProductFactory, SelectInputHandling) {
    $scope.product = {};
    $scope.message = "";
    $scope.IsLogged = false;
    $scope.editMode = true;
    $scope.productInfo = {};

    AuthenticationService.IsLogged(function (result) {
        if (result) {
            $scope.IsLogged = true;
            $q.all([ProductFactory.prepareProductInfo(), $http.get(`api/Products/${$routeParams.ProductId}`)]).then(function (response) {
                $scope.productInfo = response[0];
                $scope.product = response[1].data;
                $scope.selectedCondition =$scope.productInfo.conditions.find(c => c.condName == $scope.product.condition)
                $scope.selectedStatus = $scope.productInfo.statuses.find(s => s.statusName == $scope.product.status)
                $scope.manufacturerSelected = $scope.productInfo.manufacturers.find(m => m.manufacturerId == $scope.product.manufacturerId);
                $scope.categorySelected = $scope.productInfo.subcategories.find(s => s.productSubcategoryId == $scope.product.productSubcategoryId);
            })
        }
    })

    $scope.onChange = SelectInputHandling.onSelectChange;
    $scope.Save = function () {
        $http.put(`api/Products/${$routeParams.ProductId}`, $scope.product).then(function (response) {

            $location.path('/MyProducts');
        },
            function (error) {
                $scope.message = error.data.title;
            }
        )
    }
}])