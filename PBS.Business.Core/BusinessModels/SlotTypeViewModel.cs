using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Business.Core.BusinessModels
{
    public class SlotTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength (50)]
        public string Title { get; set; }

        #region Navigational Properties
        public List<SlotViewModel> SlotViewModels { get; set; }
        #endregion
    }
}
