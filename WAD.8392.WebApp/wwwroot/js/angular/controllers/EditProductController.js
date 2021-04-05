//controller for modifying a product
app.controller('EditProductController', ['$scope', '$http', '$routeParams', '$location', '$q', 'FacadeService', function ($scope, $http, $routeParams, $location, $q, FacadeService) {
    $scope.product = {};
    //message displaying the error
    $scope.message = "";
    //if user is not signed in, form for product editing is not displayed
    $scope.IsLogged = false;
    //if edit mode is set to true, user can select product status 
    $scope.editMode = true;
    //information from tables related to product (manufacturer, subcategory, conditionm status)
    $scope.productInfo = {};
    //if user is signed in, edit form is displayed
    FacadeService.IsLogged(function (result) {
        if (result) {
 
            //get the information related to rpoduct and then the product itself and display it in the form
            $q.all([FacadeService.PrepareProductInfo(), $http.get(`api/Products/${$routeParams.ProductId}`)]).then(function (response) {
                $scope.productInfo = response[0];
                $scope.product = response[1].data;
                //if user is logged in, but is trying to modify not his own product, edit form is not displayed
                if ($scope.product.userId == FacadeService.GetCurrentUser().userId) {

                    $scope.IsLogged = true;
                }
                //correctly display information in select inputs
                $scope.selectedCondition =$scope.productInfo.conditions.find(c => c.condName == $scope.product.condition)
                $scope.selectedStatus = $scope.productInfo.statuses.find(s => s.statusName == $scope.product.status)
                $scope.manufacturerSelected = $scope.productInfo.manufacturers.find(m => m.manufacturerId == $scope.product.manufacturerId);
                $scope.categorySelected = $scope.productInfo.subcategories.find(s => s.productSubcategoryId == $scope.product.productSubcategoryId);
            })
        }
    })
    //grab info from select input
    $scope.onChange = FacadeService.OnSelectChange;
    //function for editing product
    $scope.Save = function () {
        $http.put(`api/Products/${$routeParams.ProductId}`, $scope.product).then(function (response) {
            //in case of success redirect to the page with products of the user
            $location.path('/MyProducts');
        },
            function (error) {
        
                //display error
                $scope.message = error.data;
                //response data can be either an object (which will be displayed in ng-repeat block) or just a string
                $scope.isObject = angular.isObject(error.data);
            }
        )
    }
}])