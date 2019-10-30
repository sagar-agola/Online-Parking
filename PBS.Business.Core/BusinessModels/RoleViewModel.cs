using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Business.Core.BusinessModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string EncryptedId { get; set; }

        [Required]
        [MaxLength (50)]
        public string Title { get; set; }

        #region Navigational Properties
        public List<UserViewModel> UserViewModels { get; set; }
        #endregion
    }
}
