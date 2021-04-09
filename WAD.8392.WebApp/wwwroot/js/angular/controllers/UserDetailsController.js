//controller for displaying user details
app.controller('UserDetailsController', ['$scope', '$http', '$routeParams', 'FacadeService', function ($scope, $http, $routeParams, FacadeService) {
    $scope.user = null;
    $scope.products = [];
    //message to display error
    $scope.message = "";
    $http.get(`api/users/${$routeParams.UserId}`).then(function (response) {
        //in case of success display user info in the view
        $scope.user = response.data;
        //display date of birth in user friendly way
        $scope.user.dateOfBirth = FacadeService.ConvertDate($scope.user.dateOfBirth);
        //get products of the user
        $http.get(`api/products?user=${$scope.user.userId}`).then(function (response) {
            //display products in the view 
            $scope.products = response.data;
            //display date published in user friendly way
            $scope.products.forEach(product => {
                product.datePublished = FacadeService.ConvertDate(product.datePublished);
            })
        })
    },
        function (error) {
            //display error
            $scope.message = error.data;
        })
}])