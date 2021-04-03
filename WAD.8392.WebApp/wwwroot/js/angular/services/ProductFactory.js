app.factory('ProductFactory', ['$http', '$q', function ($http, $q) {
    return {
        prepareProductInfo: function () {
            var promiseList = [$http.get('api/Manufacturers'), $http.get('api/Subcategories'), $http.get('api/Enums/Conditions'), $http.get('api/Enums/Statuses')];

            return $q.all(promiseList).then(function (response) {
                return productInfo = {
                    manufacturers: response[0].data, subcategories: response[1].data,
                    conditions: response[2].data, statuses: response[3].data
                }
            })
        }


    }
}])