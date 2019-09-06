﻿namespace PBS.Business.Core.ApiRoute
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

        #region Parking Lot
        public static class ParkingLot
        {
            private const string _base = _root + "/parkinglot";

            public const string GetAll = _base + "/get-all";
            public const string GetByUser = _base + "/get/user/{userId}";
            public const string Get = _base + "/get/{id}";
            public const string Add = _base + "/add";
            public const string Update = _base + "/update";
            public const string Remove = _base + "/remove/{id}";
        }
        #endregion

        #region Slot
        public static class Slot
        {
            private const string _base = _root + "/slot";

            public const string GetAll = _base + "/get-all";
            public const string GetByParkingLot = _base + "/parkinglot/{id}";
            public const string Get = _base + "/get/{id}";
            public const string Add = _base + "/add";
            public const string Update = _base + "/update";
            public const string Remove = _base + "/remove/{id}";
        }
        #endregion

        #region Slot Type
        public static class SlotType
        {
            private const string _base = _root + "/slot-type";

            public const string GetAll = _base + "/get-all";
        }
        #endregion
    }
}
