app.factory('SelectInputHandling', function () {
    return {
        onSelectChange: function (item, context, product) {
            product[context] = item;
        }
    }

})