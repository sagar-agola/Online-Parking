using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Database.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [MaxLength (50)]
        public string Title { get; set; }

        #region Navigational Properties
        public List<User> Users { get; set; }
        #endregion
    }
}
