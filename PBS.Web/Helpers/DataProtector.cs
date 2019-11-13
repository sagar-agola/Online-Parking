using Microsoft.AspNetCore.DataProtection;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Utilities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Web.Helpers
{
    public class DataProtector
    {
        private readonly IDataProtector _dataProtector;

        public DataProtector (IDataProtectionProvider dataProtectionProvider,
            DataProtectionPurposeStrings purposeStrings)
        {
            _dataProtector = dataProtectionProvider.CreateProtector (purposeStrings.MasterPurposeString);
        }

        public int Unprotect (string input)
        {
            return Convert.ToInt32 (_dataProtector.Unprotect (input));
        }

        public string UnprotectString (string input)
        {
            return _dataProtector.Unprotect (input);
        }

        public string Protect (int input)
        {
            return _dataProtector.Protect (input.ToString ());
        }

        public string Protect (string input)
        {
            return _dataProtector.Protect (input);
        }

        #region User
        public void ProtectUserRouteValues (UserViewModel model)
        {
            model.EncryptedId = _dataProtector.Protect (model.Id.ToString ());
            model.EncryptedRoleId = _dataProtector.Protect (model.RoleId.ToString ());
        }

        public void ProtectUserRouteValues (List<UserViewModel> model)
        {
            model = model.Select (x =>
            {
                ProtectUserRouteValues (x);

                return x;
            }).ToList ();
        }
        #endregion

        #region Role
        public void ProtectRoleRouteValues (RoleViewModel model)
        {
            model.EncryptedId = _dataProtector.Protect (model.Id.ToString ());
        }

        public void ProtectRoleRouteValues (List<RoleViewModel> model)
        {
            model = model.Select (x =>
            {
                ProtectRoleRouteValues (x);

                return x;
            }).ToList ();
        }
        #endregion

        #region Parking Lot
        public void ProtectParkingLotRouteValues (ParkingLotViewModel model)
        {
            model.EncryptedId = _dataProtector.Protect (model.Id.ToString ());
            model.EncryptedOwnerId = _dataProtector.Protect (model.OwnerId.ToString ());
            model.EncryptedAddressId = _dataProtector.Protect (model.AddressId.ToString ());
        }

        public void ProtectParkingLotRouteValues (List<ParkingLotViewModel> model)
        {
            model = model.Select (x =>
            {
                ProtectParkingLotRouteValues (x);

                return x;
            }).ToList ();
        }
        #endregion

        #region Booking
        public void ProtectBookingRouteValues (BookingViewModel model)
        {
            model.EncryptedId = _dataProtector.Protect (model.Id.ToString ());
            model.EncryptedSlotId = _dataProtector.Protect (model.SlotId.ToString ());
            model.EncryptedCustomerId = _dataProtector.Protect (model.CustomerId.ToString ());

            if (model.SlotViewModel != null)
            {
                model.SlotViewModel.EncryptedId = _dataProtector.Protect (model.SlotViewModel.Id.ToString ());
                model.SlotViewModel.EncryptedSlotTypeId = _dataProtector.Protect (model.SlotViewModel.SlotTypeId.ToString ());
                model.SlotViewModel.EncryptedParkingLotId = _dataProtector.Protect (model.SlotViewModel.ParkingLotId.ToString ());
            }
        }

        public void ProtectBookingRouteValues (List<BookingViewModel> model)
        {
            model = model.Select (x =>
            {
                ProtectBookingRouteValues (x);

                return x;
            }).ToList ();
        }
        #endregion

        #region Slot
        public void ProtectSlotRouteValues (SlotViewModel model)
        {
            model.EncryptedId = _dataProtector.Protect (model.Id.ToString ());
            model.EncryptedParkingLotId = _dataProtector.Protect (model.ParkingLotId.ToString ());
            model.EncryptedSlotTypeId = _dataProtector.Protect (model.SlotTypeId.ToString ());
        }

        public void ProtectSlotRouteValues (List<SlotViewModel> model)
        {
            model = model.Select (x =>
            {
                ProtectSlotRouteValues (x);

                return x;
            }).ToList ();
        }
        #endregion

        #region Slot Type
        public void ProtectSlotTypeRouteValues (SlotTypeViewModel model)
        {
            model.EncryptedId = _dataProtector.Protect (model.Id.ToString ());
        }

        public void ProtectSlotTypeRouteValues (List<SlotTypeViewModel> model)
        {
            model = model.Select (x =>
            {
                ProtectSlotTypeRouteValues (x);

                return x;
            }).ToList ();
        }
        #endregion
    }
}