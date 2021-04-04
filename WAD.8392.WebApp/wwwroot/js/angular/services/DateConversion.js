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