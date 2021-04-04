//project initialization
var app = angular.module('MusicInstrumentsSpa', ['ngRoute', 'ngStorage']);
//route settings
app.config(['$routeProvider',function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'pages/index.html',
            controller: 'ProductListController'
        })
        .when('/Users', {
            templateUrl: 'pages/users.html',
            controller: 'UsersController'
        })
        .when('/SignUp', {
            templateUrl: 'pages/save_user.html',
            controller: 'RegisterController'
        })
        .when('/SignIn', {
            templateUrl: 'pages/login.html',
            controller: 'LoginController'
        })
        .when('/AddProduct', {
            templateUrl: 'pages/save_product.html',
            controller: 'AddProductController'
        })
        .when('/EditProduct/:ProductId', {
            templateUrl: 'pages/save_product.html',
            controller: 'EditProductController'
        })
        .when('/MyProducts', {
            templateUrl: 'pages/my_products.html',
            controller: 'MyProductsController'
        })
        .when('/MyAccount', {
            templateUrl: 'pages/account.html',
            controller: 'AccountController'
        })
        .when('/EditAccount', {
            templateUrl: 'pages/save_user.html',
            controller: 'EditAccountController'
        })
        .when('/UserDetails/:UserId', {
            templateUrl: 'pages/user_details.html',
            controller: 'UserDetailsController'
        })
        .when('/ProductDetails/:ProductId', {
            templateUrl: 'pages/product_details.html',
            controller: 'ProductDetailsController'
        })
        .otherwise({
            redirectTo: '/'
        })
}]);
//service for handling logins
app.service('AuthenticationService', ['$http', function ($http) {
    var service = {};
    //Checks if user is signed in and, if so, ensures that http authorization header is not empty
    service.IsLogged = function (callback) {
        //if session storage is not empty, user is signed in
        if (service.GetCurrentUser()) {
            //if the authorization header is empty, JWT token of logged user is assigned to header
            //this manipulation is necessary because when browser page gets refreshed, the information (JWT token) in $http.defaults.headers is lost
            //even if user is signed in (current_user is not null in sessionStorage), which makes it impossible to access api with authorization filter
            if (!$http.defaults.headers.common.Authorization) {
                $http.defaults.headers.common.Authorization = 'Bearer ' + sessionStorage.getItem('token');
            }
            callback(true);
        } else {
            callback(false);
        }
    }
    //function for signing user in
    service.Login = function (login, callback) {
        //if user is already signed in, no actions are taken
        service.IsLogged(function (result) {
            if (result) {
                callback(false);
            } else {
                $http.post("api/Authentication", login).then(
                    function (response) {
                        //if credentials are correct, store user in sessionStorage along with token
                        sessionStorage.setItem('token', response.data);
                        $http.defaults.headers.common.Authorization = 'Bearer ' + response.data;
                        $http.get("api/Users/Account").then(function (response) {
                            service.SetCurrentUser(response.data);
                            callback(true);
                        })
                    },
                    function (error) {
                        callback(false);
                    })
            }
        })
    }
    //function for signing user out
    service.Logout = function () {
        sessionStorage.clear();
        $http.defaults.headers.common.Authorization = '';
    }
    //function for retrieving logged user from session storage
    service.GetCurrentUser = function () {
       return JSON.parse(sessionStorage.getItem('current_user'));
    }
    //function for setting current user to session storage
    service.SetCurrentUser = function (user) {
        sessionStorage.setItem('current_user', JSON.stringify(user))
    }
    return service;
}])
//service for displaying date format in a proper way in UI
app.factory('DateConversion', function () {
    return {
        ConvertDate: function (date) {
            var date = new Date(date);
            var year = date.getFullYear();
            var month = date.getMonth();
            var day = date.getDate();
            return `${day}/${month + 1}/${year}`;
        }
    }
})
//facade for all services; this service will be injected in controllers instead of multiple number of all services
//this service is a wrapper for functions of other services
app.factory('FacadeService', ['AuthenticationService', 'DateConversion', 'ProductFactory', 'SelectInputHandling',
    function (AuthenticationService, DateConversion, ProductFactory, SelectInputHandling) {
        return {
            //AuthenticationService functions
            IsLogged: AuthenticationService.IsLogged,
            Login: AuthenticationService.Login,
            Logout: AuthenticationService.Logout,
            GetCurrentUser: AuthenticationService.GetCurrentUser,
            SetCurrentUser: AuthenticationService.SetCurrentUser,
            //DateConversion service functions
            ConvertDate: DateConversion.ConvertDate,
            //ProductFactory service functions
            PrepareProductInfo: ProductFactory.PrepareProductInfo,
            //SelectInputHandling service funciotns
            OnSelectChange: SelectInputHandling.OnSelectChange
        }
    }])
//service for getting information related to product for product creation and modification
app.factory('ProductFactory', ['$http', '$q', function ($http, $q) {
    return {
        PrepareProductInfo: function () {
            var promiseList = [$http.get('api/Manufacturers'), $http.get('api/Subcategories'), $http.get('api/Enums/Conditions'), $http.get('api/Enums/Statuses')];
            //when all information is received, assign it to a variable that will be used in controller scope
            return $q.all(promiseList).then(function (response) {
                return productInfo = {
                    //info is recieved in the order defined in promiseList
                    manufacturers: response[0].data, subcategories: response[1].data,
                    conditions: response[2].data, statuses: response[3].data
                }
            })
        }


    }
}])
//service for handling the values from select input
app.factory('SelectInputHandling', function () {
    return {
        //item is value of input
        //context is property of object to which the value belongs (manufacturer, condition, status, subcategory)
        OnSelectChange: function (item, context, product) {
            product[context] = item;
        }
    }

})
//controller for displaying account info
app.controller('AccountController', ['$scope', '$http', '$location', 'FacadeService', function ($scope, $http, $location, FacadeService) {
    //the message is displayed when account is deleted or when user is not signed in
    $scope.message = "";
    $scope.user = {};
    //when this variable is set to false, no content except the message is displayed in view
    $scope.IsLogged = false;
    //if user is signed in, then get the current user from local storage and display it in the view
    FacadeService.IsLogged(function (result) {
        if (result) {
            $scope.user = FacadeService.GetCurrentUser();
            //display date of birth in user friendly format
            $scope.user.dateOfBirth = FacadeService.ConvertDate($scope.user.dateOfBirth);
            //display view content
            $scope.IsLogged = true;
        } else {
            $scope.message = "Sign in to see your account details";
        }
    });
}])

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
        $http.post("api/Products", $scope.product).then(function (response) {
            //if created, go to the page with products created by this user
            $location.path('/MyProducts');
        }, function (error) {
            //display the error
            $scope.message = error.data;
        })
    }
}])
//controller for account deletion
//this controller is a child controller of AccountController, since delete action is in the same view and route
app.controller('DeleteAccountController', ['$http', '$scope', 'FacadeService', function ($http, $scope, FacadeService) {
    //function for account deletion
    $scope.Delete = function () {
        //delete user
        $http.delete(`api/Users`).then(function (response) {
            //clear http headers and session storage from current user info
            FacadeService.Logout();
            //$parent is AccountController
            $scope.$parent.user = null;
            $scope.$parent.message = "Account is deleted";
            //disallow content display except the message
            $scope.$parent.IsLogged = false;
        })
    }
}])
//controller for product deletion
//this controller is a child controller of MyProductsController, since delete action is in the same view and route
app.controller('DeleteProductController', ['$scope', '$http', function ($scope, $http) {
    //function for product deletion
    $scope.Delete = function (product) {
        $http.delete(`api/Products/${product.productId}`).then(function (response) {
            //in case of success, remove the product from product list
            var index = $scope.$parent.products.indexOf(product);
            $scope.$parent.products.splice(index, 1);
        })
    }
}])
//controller for modifying account
app.controller('EditAccountController', ['$scope', '$http', '$location', 'FacadeService', function ($scope, $http, $location, FacadeService) {
    $scope.user = {};
    //form is not displayed in the view if user is not signed in
    $scope.DisplayForm = false;
    //message displaying error
    $scope.message = "";
    //this message is displayed if user is not signed in
    $scope.info = "";
    //this is the value for save button displayed in view 
    $scope.mode = "Modify";
    FacadeService.IsLogged(function (result) {
        if (result) {
            //if user is signed in, get the current user from session storage and display user info
            $scope.user = FacadeService.GetCurrentUser();
            $scope.user.dateOfBirth = new Date($scope.user.dateOfBirth);
            $scope.DisplayForm = true;
        } else {
            $scope.info = "To edit account you should sign in first";
        }
    })
    //function for editing user
    $scope.Save = function () {
        $http.put(`api/Users`, $scope.user).then(function (response) {
            //in case of success, update the current user info in session storage and redirect to account page
            FacadeService.SetCurrentUser($scope.user);
            $location.path('/MyAccount');
        },
            function (error) {
                //display error
                $scope.message = error.data;
            }
        )
    }

}])
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
            $scope.IsLogged = true;
            //get the information related to rpoduct and then the product itself and display it in the form
            $q.all([FacadeService.PrepareProductInfo(), $http.get(`api/Products/${$routeParams.ProductId}`)]).then(function (response) {
                $scope.productInfo = response[0];
                $scope.product = response[1].data;
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
            }
        )
    }
}])
//controller for login handling
app.controller('LoginController', ['$scope', '$location', 'FacadeService', function ($scope, $location, FacadeService) {
    $scope.login = {
        userName: null,
        password: null
    };
    //message to display error
    $scope.message = "";
    //login form is displayed when user is not signed in
    $scope.IsLogged = false;
    FacadeService.IsLogged(function (result) {
        if (result) {
            //if user is signed in, form is not displayed
            $scope.IsLogged = true;
        }
    })
    //function for login
    $scope.SignIn = function () {
        FacadeService.Login($scope.login, function (result) {
            if (result) {
                //in case of success, redirect to account page
                $location.path('/MyAccount');
            } else {
                //display error
                $scope.message = "Login failed";
            }
        })
    }
}]);
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
   
}])

//controller for displaying product details
app.controller('ProductDetailsController', ['$scope', '$http', '$routeParams', 'FacadeService', function ($scope, $http, $routeParams, FacadeService) {
    $scope.product = null;
    //message to display error
    $scope.message = "";
    $http.get(`api/Products/${$routeParams.ProductId}`).then(function (response) {
        //if product exists, display product info in the view
        $scope.product = response.data;
        //display date published in user friendly way
        $scope.product.datePublished = FacadeService.ConvertDate($scope.product.datePublished);
    },
        function (error) {
            //display error
            $scope.message = error.data;
        })
}])
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
//controller handling user registration
app.controller('RegisterController', ['$scope', '$http', '$location', 'FacadeService', function ($scope, $http, $location, FacadeService) {
    $scope.user = {};
    //mode is displayed as value in save button in the view
    $scope.mode = "Register";
    //form is displayed if user is not signed in
    $scope.DisplayForm = true;
    //message that is displayed when user is signed in
    $scope.info = "";
    //message to display error
    $scope.message = "";
    //if user is signed in, form is not displayed
    FacadeService.IsLogged(function (result) {
        if (result) {
            $scope.info = "You are already registered";
            $scope.DisplayForm = false;
        }
    })
    //function to register user
    $scope.Save = function () {
        $http.post("api/Users", $scope.user).then(function (response) {
            //in case of success, automatically sign in
            $scope.login = {
                userName: $scope.user.userName,
                password: $scope.user.password
            }
            FacadeService.Login($scope.login, function (result) {
                if (result) {
                    //in case of success, redirect to account page
                    $location.path('/MyAccount');
                } else {
                    //display error
                    $scope.message = "Something went wrong";
                }
            })
        }, function (error) {
                //display error
            $scope.message = error.data;
        });
    }
}])
//controller for signing user out
//this controller is a child controller of AccountController, since sign in action is in the same view and route
app.controller('SignOutController', ['$scope', '$location', 'FacadeService', function ($scope, $location, FacadeService) {
    //function for signing out
    $scope.SignOut = function () {
        //clear session storage from current user and go to the main page
        FacadeService.Logout();
        $location.path('/');
    }
}])
//controller for displaying user details
app.controller('UserDetailsController', ['$scope', '$http', '$routeParams', 'FacadeService', function ($scope, $http, $routeParams, FacadeService) {
    $scope.user = null;
    $scope.products = [];
    //message to display error
    $scope.message = "";
    $http.get(`api/Users/${$routeParams.UserId}`).then(function (response) {
        //in case of success display user info in the view
        $scope.user = response.data;
        //display date of birth in user friendly way
        $scope.user.dateOfBirth = FacadeService.ConvertDate($scope.user.dateOfBirth);
        //get products of the user
        $http.get(`api/Products?user=${$scope.user.userId}`).then(function (response) {
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
//controller for displaying list of users
app.controller('UsersController', ['$scope', '$http', 'FacadeService', function ($scope, $http, FacadeService) {
    $scope.users = [];
    $http.get("api/Users").then(function (response) {
        //get the users and display them in the view
        $scope.users = response.data;
        //display date of birth in user friendly way
        $scope.users.forEach(function (user) {
            user.dateOfBirth = FacadeService.ConvertDate(user.dateOfBirth);
        })
    })
}]);