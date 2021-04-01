app.controller('ProductListController', ['$scope', '$http', 'DateConversion', function ($scope, $http, DateConversion) {
    $scope.products = [];
    $scope.subcategories = [];
    $scope.categories = [];
    $scope.filterParams = {
        manufacturer: null,
        subcategory: null,
        category: null
    }

    $scope.manufactuers = [];
    $http.get(`api/Manufacturers`).then(function (response) {
        $scope.manufacturers = response.data;
    })
    $http.get(`api/Categories`).then(function (response) {
        $scope.categories = response.data;
    })
    $scope.GetProducts = function () {

        $http.get(`api/Subcategories?${$scope.filterParams.category == null ? '' : `&category=${$scope.filterParams.category}`}`).then(function (response) {
            $scope.subcategories = response.data;
        })

        var queryString = $scope.filterParams.manufacturer == null ? '' : `manufacturer=${$scope.filterParams.manufacturer}`;
        $scope.filterParams.category == null ? queryString += '' : queryString += `&category=${$scope.filterParams.category}`;
        $scope.filterParams.subcategory == null ? queryString += '' : queryString += `&subcategory=${$scope.filterParams.subcategory}`;
        $http.get(`api/Products?status=0&${queryString}`).then(function (response) {
            $scope.products = response.data;
            $scope.products.forEach(function (product) {
                product.datePublished = DateConversion.ConvertDate(product.datePublished);
            })
        })
    }
    $scope.GetProducts();
    $scope.GetProductsFromCategory = function () {
        $scope.filterParams.subcategory = null;
        $scope.GetProducts();
    }
    $scope.Clear = function () {
        $scope.filterParams = {
            manufacturer: null,
            subcategory: null,
            category: null
        }
        $scope.GetProducts();
    }
}]);