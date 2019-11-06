using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Models
{
    public class CreateSlotModel
    {
        [Required]
        [Display (Name = "Slot Type")]
        public int SlotTypeId { get; set; }

        [Display (Name = "Hourly Rate")]
        [Range (0, 99, ErrorMessage = "Slot hourly rate can be 99 INR at max.")]
        public int HourlyRate { get; set; }

        public int ParkingLotId { get; set; }

        public List<SlotTypeViewModel> SlotTypes { get; set; }
    }
}
