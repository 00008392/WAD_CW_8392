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