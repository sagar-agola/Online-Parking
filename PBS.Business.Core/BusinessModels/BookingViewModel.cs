using System;
using System.ComponentModel.DataAnnotations;

namespace PBS.Business.Core.BusinessModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "Vehicle number is required field")]
        [MaxLength (10, ErrorMessage = "Invalid Vehicle number pattern")]
        public string VehicleNumber { get; set; }

        public bool IsActive { get; set; }

        #region Booking Customer
        public int CustomerId { get; set; }
        public UserViewModel CustomerViewModel { get; set; }
        #endregion

        #region Booking Slot
        public int SlotId { get; set; }
        public SlotViewModel SlotViewModel { get; set; }
        #endregion
    }
}
