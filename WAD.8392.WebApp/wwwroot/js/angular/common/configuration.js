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
        .when('/About', {
            templateUrl: 'pages/about.html'
        })
        .otherwise({
            redirectTo: '/'
        })
}]);