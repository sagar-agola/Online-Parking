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

        #region Home
        public static class Home
        {
            private const string _base = _root + "/home";

            public const string Search = _base + "/search/{query}";
        }
        #endregion

        #region User
        public static class User
        {
            private const string _base = _root + "/user";

            public const string GetAll = _base + "/get-all";
            public const string Get = _base + "/get/{id}";
            public const string Update = _base + "/update";
            public const string Remove = _base + "/remove/{id}";
            public const string ChangePassword = _base + "/change-password";
            public const string ChangeRole = _base + "/change-role";
            public const string ConfirmEmail = _base + "/confirm-email/{id}";
            public const string EmailExists = _base + "/email-exists";
        }
        #endregion

        #region Address
        public static class Address
        {
            private const string _base = _root + "/address";

            public const string Add = _base + "/add/{id}";
            public const string Update = _base + "/update";
        }
        #endregion

        #region Parking Lot
        public static class ParkingLot
        {
            private const string _base = _root + "/parkinglot";

            public const string GetAll = _base + "/get-all";
            public const string GetRequested = _base + "/get-requested";
            public const string GetByUser = _base + "/get/user/{userId}";
            public const string Get = _base + "/get/{id}";
            public const string Add = _base + "/add";
            public const string Update = _base + "/update";
            public const string Aprove = _base + "/aprove/{id}";
            public const string Remove = _base + "/remove/{id}";
            public const string UploadImage = _base + "/upload-image";
            public const string GetImages = _base + "/all-images/{id}";
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
            public const string MakeBooked = _base + "/make-booked/{id}";
            public const string RemoveBooked = _base + "/remove-booked/{id}";
            public const string MakeAvailable = _base + "/make-available/{id}";
            public const string Remove = _base + "/remove/{id}";
        }
        #endregion

        #region Slot Type
        public static class SlotType
        {
            private const string _base = _root + "/slot-type";

            public const string GetAll = _base + "/get-all";
            public const string Get = _base + "/get/{id}";
            public const string Add = _base + "/add";
            public const string Remove = _base + "/remove/{id}";
        }
        #endregion

        #region Role
        public static class Role
        {
            private const string _base = _root + "/role";

            public const string GetAll = _base + "/get-all";
            public const string Get = _base + "/get/{id}";
            public const string Add = _base + "/add";
            public const string Update = _base + "/update";
            public const string Remove = _base + "/remove/{id}";
        }
        #endregion

        #region Booking
        public static class Booking
        {
            private const string _base = _root + "/booking";

            public const string Add = _base + "/add";
            public const string Get = _base + "/get/{id}";
            public const string GetByUser = _base + "/get/user/{id}";
            public const string GetByParkingLot = _base + "/get/parkingLot/{id}";
            public const string GetAll = _base + "/get-all";
            public const string ConfirmBooking = _base + "/confirm-booking/{id}";
        }
        #endregion
    }
}
