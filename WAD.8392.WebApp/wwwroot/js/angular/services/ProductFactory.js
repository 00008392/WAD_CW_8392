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