using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;

namespace PBS.Web.Areas.Admin.Models
{
    public class ParkingLotRequestdetailsModel
    {
        public ParkingLotViewModel ParkingLot { get; set; }

        public List<string> Images { get; set; } = new List<string> ();
    }
}
