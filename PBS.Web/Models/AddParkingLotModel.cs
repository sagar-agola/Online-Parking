using PBS.Business.Core.BusinessModels;
using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Models
{
    public class AddParkingLotModel
    {
        [Required(ErrorMessage = "Parking lot name is required")]
        public string Name { get; set; }

        public AddressViewModel AddressViewModel { get; set; }

        [Required (ErrorMessage = "Number of 2 wheel slots are required")]
        [Display (Name = "Number Of 2 wheel slot")]
        public int? NoOf2WheelSlot { get; set; }

        [Required (ErrorMessage = "Number of 4 wheel slots are required")]
        [Display (Name = "Number Of 4 wheel slot")]
        public int? NoOf4WheelSlot { get; set; }
    }
}
