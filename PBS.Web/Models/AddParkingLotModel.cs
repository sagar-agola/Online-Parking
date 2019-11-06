using PBS.Business.Core.BusinessModels;
using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Models
{
    public class AddParkingLotModel
    {
        [Required (ErrorMessage = "Parking lot name is required")]
        public string Name { get; set; }

        public AddressViewModel AddressViewModel { get; set; }

        [Required]
        [Display (Name = "Number Of 2 wheel slot")]
        public int? NoOf2WheelSlot { get; set; }

        [Required]
        [Display (Name = "2 Wheel Hourly Rate")]
        [Range (0, 99, ErrorMessage = "Slot hourly rate can be 99 INR at max.")]
        public int TwoWheelerHourlyRate { get; set; }

        [Required]
        [Display (Name = "Number Of 4 wheel slot")]
        public int? NoOf4WheelSlot { get; set; }

        [Required]
        [Display (Name = "4 Wheel Hourly Rate")]
        [Range (0, 99, ErrorMessage = "Slot hourly rate can be 99 INR at max.")]
        public int FourWheelerHourlyRate { get; set; }
    }
}
