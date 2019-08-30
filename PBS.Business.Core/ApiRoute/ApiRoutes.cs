namespace PBS.Business.Core.ApiRoute
{
    public static class ApiRoutes
    {
        private const string _root = "api";

        #region Auth
        public static class Auth
        {
            private const string _base = _root + "/auth";

            public const string Register = _base + "/register";
            public const string Login = _base + "/login";
        }
        #endregion

        #region User
        public static class User
        {
            private const string _base = _root + "/user";

            public const string GetAll = _base + "/get-all";
            public const string Get = _base + "/get/{id}";
            public const string Update = _base + "/update";
            public const string ChangePassword = _base + "/change-password";
        }
        #endregion

        #region Address
        public static class Address
        {
            private const string _base = _root + "/address";

            public const string Add = _base + "/add";
            public const string Update = _base + "/update";
        }
        #endregion
    }
}
