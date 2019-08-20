using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Database.Models
{
    public class SlotType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength (50)]
        public string Title { get; set; }

        #region Navigational Properties
        public List<Slot> Slots { get; set; }
        #endregion
    }
}
