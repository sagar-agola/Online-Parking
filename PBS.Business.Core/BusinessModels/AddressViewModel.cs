using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Business.Core.BusinessModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string LandMark { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [MinLength (6)]
        [MaxLength (6)]
        public string PinCode { get; set; }

        public string State { get; set; }

        #region Navigational Properties
        public List<UserViewModel> UserViewModels { get; set; }

        public List<ParkingLotViewModel> ParkingLotViewModels { get; set; }
        #endregion

    }
}
