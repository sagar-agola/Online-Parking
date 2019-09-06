using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Models
{
    public class CreateSlotModel
    {
        [Required]
        [Display(Name = "Slot Type")]
        public int SlotTypeId { get; set; }

        public int ParkingLotId { get; set; }

        public List<SlotTypeViewModel> SlotTypes { get; set; }
    }
}
