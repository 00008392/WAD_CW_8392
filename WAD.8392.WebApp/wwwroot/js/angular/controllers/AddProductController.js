app.controller('AddProductController', ['$http', '$scope', '$location', 'AuthenticationCheck', function ($http, $scope, $location, AuthenticationCheck) {
    $scope.message = "";
    $scope.IsLogged = false;
    $scope.editMode = false;
    $scope.manufacturers = [];
    $scope.productSubcategories = [];
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
    $http.get("api/Manufacturers").then(function (response) {
        $scope.manufacturers = response.data;
    })
    $http.get("api/Subcategories").then(function (response) {
        $scope.productSubcategories = response.data;
    })
    AuthenticationCheck.IsLogged(function (result) {
        if (result) {
            $scope.IsLogged = true;
        }
    })
    $scope.onChange = function (item, context) {
        $scope.product[context] = item
    }
    $scope.product = {
        productName: null,
        productDescription: null,
        price: null,
        location: null,
        manufacturerId: null,
        condition: null,
        productSubcategoryId: null
    };
    $scope.Save = function () {
        console.log($http.defaults.headers.common.Authorization);
        $http.post("api/Products", $scope.product).then(function (response) {
            $location.path('/MyProducts');
        }, function (error) {
            $scope.message = error.data.title;
        })
    }
}])