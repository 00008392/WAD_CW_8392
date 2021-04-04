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