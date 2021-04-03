//service for displaying date format in a proper way in UI
app.service('DateConversion', function () {
    var service = {};
    service.ConvertDate = function (date) {
        var date = new Date(date);
        var year = date.getFullYear();
        var month = date.getMonth();
        var day = date.getDate();
        return `${day}/${month + 1}/${year}`;
    }
    return service;
})