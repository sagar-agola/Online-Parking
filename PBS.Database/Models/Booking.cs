using System;
using System.ComponentModel.DataAnnotations;

namespace PBS.Database.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        [Required]
        [MaxLength(10)]
        public string VehicleNumber { get; set; }

        public bool IsActive { get; set; }

        #region Booking Customer
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        #endregion

        #region Booking Slot
        public int SlotId { get; set; }
        public Slot Slot { get; set; }
        #endregion
    }
}
