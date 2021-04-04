//controller that displays products of logged user
app.controller('MyProductsController', ['$scope', '$http', 'FacadeService', function ($scope, $http, FacadeService) {
    //message is displayed when user is not signed in
    $scope.message = "";
    $scope.products = [];
    //if user is not signed in, content of the view is not displayed
    $scope.IsLogged = false;
    FacadeService.IsLogged(function (result) {
        //if user is signed in, get current user from session storage
        if (result) {
            $scope.IsLogged = true;
            $scope.CurrentUser = FacadeService.GetCurrentUser();
            //get products of the current user
            $http.get(`api/Products?user=${$scope.CurrentUser.userId}`).then(function (response) {
                $scope.products = response.data;
                //display date published in user friendly format
                $scope.products.forEach(product => {
                    product.datePublished = FacadeService.ConvertDate(product.datePublished);
                })
            })
        } else {
            $scope.message = "Sign in to see your products";
        }
    })
    //function for product deletion
    $scope.Delete = function (product) {
        $http.delete(`api/Products/${product.productId}`).then(function (response) {
            //in case of success, remove the product from product list
            var index = $scope.products.indexOf(product);
            $scope.products.splice(index, 1);
        })
    }
}])