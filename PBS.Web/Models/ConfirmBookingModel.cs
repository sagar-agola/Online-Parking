using System;
using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Models
{
    public class ConfirmBookingModel
    {
        public int UserId { get; set; }

        public int SlotId { get; set; }

        [Required]
        [Display(Name = "Start Date Time")]
        public DateTime StartDateTime { get; set; }

        [Required]
        [Display (Name = "Hour")]
        public int DurationHour { get; set; }

        [Required]
        [Display (Name = "Minute")]
        public int DurationMinute { get; set; }

        [Required]
        [StringLength (2, MinimumLength = 2, ErrorMessage = "State Code can be only of 2 characters")]
        [Display (Name = "State Code")]
        public string StateCode { get; set; }

        [Required]
        [StringLength (2, MinimumLength = 1, ErrorMessage = "District Code can only be 1 or 2 digit long")]
        [Display (Name = "District Code")]
        [Range (0, 99, ErrorMessage = "Enter valid Distract code.")]
        public string DistrictCode { get; set; }

        [Required]
        [StringLength (2, MinimumLength = 2, ErrorMessage = "Series Code can be only of 2 characters")]
        [Display (Name = "Series Code")]
        public string SeriesCode { get; set; }

        [Required]
        [StringLength (4, MinimumLength = 4, ErrorMessage = "Vehicle number must be 4 digit long")]
        [Range (0, 9999, ErrorMessage = "Enter valid Distract Vehical number.")]
        public string Number { get; set; }
    }
}
