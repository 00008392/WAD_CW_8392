//controller for displaying and filtering of product list
app.controller('ProductListController', ['$scope', '$http', '$q','FacadeService', function ($scope, $http, $q,FacadeService) {
    //data that will be displayed in the view
    $scope.data = {
        manufacturers: [],
        products: [],
        subcategories: [],
        categories:[]
    }
    //parameters by which products will be filtered
    $scope.filterParams = {
        manufacturer: null,
        subcategory: null,
        category: null
    }
    //getting product related data for filters (excpet subcategories, because they are also filtered as products)
    $http.get(`api/Manufacturers`).then(function (response) {
        $scope.data.manufacturers = response.data;
    })
    $http.get(`api/Categories`).then(function (response) {
        $scope.data.categories = response.data;
    })

    //getting subcategories filtered by category
    //when user filters products by category, subcategories are getting filtered by that category as well
    $scope.GetSubcategories = function () {
        $http.get(`api/Subcategories?${$scope.filterParams.category == null ? '' : `&category=${$scope.filterParams.category}`}`).then(function (response) {
            $scope.data.subcategories = response.data;
        })
    }
    //forming a query string from filter parameters
    $scope.BuildQueryString = function () {
        //the view displays only available products, so status is set to 0 by default
        var queryString = 'status=0&';
        //if manufacturer is chosen, add it to query string
        $scope.filterParams.manufacturer == null ? queryString += '' : queryString += `manufacturer=${$scope.filterParams.manufacturer}&`;
        //if category is chosen, add it to query string
        $scope.filterParams.category == null ? queryString += '' : queryString += `category=${$scope.filterParams.category}&`;
        //if subcategory is chosen, add it to query string
        $scope.filterParams.subcategory == null ? queryString += '' : queryString += `subcategory=${$scope.filterParams.subcategory}&`;
        return queryString;
    }
    //getting filtered products
    $scope.GetProducts = function () {
        var queryString = $scope.BuildQueryString();
        $http.get(`api/Products?${queryString}`).then(function (response) {
            $scope.data.products = response.data;
            $scope.data.products.forEach(function (product) {
                product.datePublished = FacadeService.ConvertDate(product.datePublished);
            })
        })
    }
    //function for getting filtered products and subcategories
    $scope.GetData = function () {
        $scope.GetSubcategories();
        $scope.GetProducts();
    }
    //getting initial values
    $scope.GetData();
    //this function is invoked when user filters data by category
    $scope.GetDataFromCategory = function () {
        //every time when user chooses some category value, previously chosen subcategory filter is cleared
        //this is done in order to prevent filtering products by a category and previously chosen subcategory at the same time,
        //since this subcategory may not belong to chosen category, which would produce confusing results
        $scope.filterParams.subcategory = null;
        $scope.GetData();
    }
    //clearing all filters
    $scope.Clear = function () {
        $scope.filterParams = {
            manufacturer: null,
            subcategory: null,
            category: null
        }
        //getting data again
        $scope.GetData();
    }
}]);