using Microsoft.AspNetCore.Http;

namespace PBS.Business.Core.Models
{
    public class UploadLotImageModel
    {
        public int ParkingLotId { get; set; }

        public IFormFile Image { get; set; }
    }
}
