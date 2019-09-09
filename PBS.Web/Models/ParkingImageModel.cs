using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Models
{
    public class ParkingImageModel
    {
        public int ParkingLotId { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public List<string> Images { get; set; } = new List<string> ();
    }
}
