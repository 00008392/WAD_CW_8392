//controller for product creation
app.controller('AddProductController', ['$http', '$scope', '$location', 'FacadeService', function ($http, $scope, $location, FacadeService) {
    //message to display error
    $scope.message = "";
    //if user is not signed in, content is not displayed
    $scope.IsLogged = false;
    //if edit mode is set to false, product status select input is not siaplyed in view (status is automatically set to available on server side)
    $scope.editMode = false;
    //information from tables related to product (manufacturer, subcategory, conditionm status)
    $scope.productInfo = {};
    FacadeService.PrepareProductInfo().then(function (data) {
        $scope.productInfo = data;
    })
    $scope.product = {};
    //check if user is signed in
    FacadeService.IsLogged(function (result) {
        if (result) {
            //display content
            $scope.IsLogged = true;
        }
    })
    //grab the values from select input
    $scope.onChange = FacadeService.OnSelectChange;
    //create product
    $scope.Save = function () {
        $http.post("api/products", $scope.product).then(function (response) {
            //if created, go to the page with products created by this user
            $location.path('/MyProducts');
        }, function (error) {
            //display the error
                $scope.isObject = angular.isObject(error.data);
            //response data can be either an object (which will be displayed in ng-repeat block) or just a string
            $scope.message = error.data;
        })
    }
}])