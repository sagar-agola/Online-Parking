using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;

namespace PBS.Web.Models
{
    public class ManageSlotModel
    {
        public List<SlotViewModel> Slots { get; set; } = new List<SlotViewModel> ();

        public List<SlotTypeViewModel> SlotTypes { get; set; } = new List<SlotTypeViewModel> ();
    }
}
