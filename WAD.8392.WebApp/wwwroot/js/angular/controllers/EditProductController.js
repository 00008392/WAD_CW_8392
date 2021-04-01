app.controller('EditProductController', ['$scope', '$http', '$routeParams', '$location', 'AuthenticationCheck', function ($scope, $http, $routeParams, $location, AuthenticationCheck) {
    $scope.product = null;
    $scope.message = "";
    $scope.IsLogged = false;
    $scope.editMode = true;
    $scope.manufacturers = [];
    $scope.productSubcategories = [];

    AuthenticationCheck.IsLogged(function (result) {
        if (result) {
            $scope.IsLogged = true;
            $http.get(`api/Products/${$routeParams.ProductId}`).then(function (response) {
                $scope.product = response.data;
                $scope.selectedCondition = $scope.conditions.find(c => c.condName == $scope.product.condition)
                $scope.selectedStatus = $scope.statuses.find(s => s.statusName == $scope.product.status)
                $http.get("api/Manufacturers").then(function (response) {
                    $scope.manufacturers = response.data;
                    $scope.manufacturerSelected = $scope.manufacturers.find(m => m.manufacturerId == $scope.product.manufacturerId);
                });

                $http.get("api/Subcategories").then(function (response) {
                    $scope.productSubcategories = response.data;
                    $scope.categorySelected = $scope.productSubcategories.find(s => s.productSubcategoryId == $scope.product.productSubcategoryId);
                });
            })
        }
    })


    $scope.conditions = [
        {
            condValue: 0,
            condName: 'New'
        },
        {
            condValue: 1,
            condName: 'Medium'
        },
        {
            condValue: 2,
            condName: 'Old'
        }
    ]


    $scope.statuses = [
        {
            statusValue: 0,
            statusName: 'Available'
        },
        {
            statusValue: 1,
            statusName: 'Booked'
        },
        {
            statusValue: 2,
            statusName: 'Sold'
        }
    ]


    $scope.onChange = function (item, context) {
        $scope.product[context] = item
    }
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