using PBS.Business.Core.AuthorizeNetApiModels.Request;
using PBS.Business.Core.BusinessModels;
using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Models
{
    public class PaymentModel
    {
        public BookingViewModel Booking { get; set; }

        public BillTo BillTo { get; set; }

        public int Amount { get; set; }

        [Required]
        [Display (Name = "Credit Card Number")]
        [DataType (DataType.CreditCard)]
        public string CardNumber { get; set; }

        [Required (ErrorMessage = "Expiration date is required")]
        [Display (Name = "Month")]
        [StringLength(2, ErrorMessage = "Date must be 2 character long")]
        [Range (1, 12, ErrorMessage = "Invalid Expiration date")]
        public string ExpMonth { get; set; }

        [Required (ErrorMessage = "Expiration date is required")]
        [Display (Name = "Year")]
        [StringLength(4, ErrorMessage = "Year must be 4 character long")]
        [Range (2019, 9999, ErrorMessage = "Invalid Expiration date")]
        public string ExpYear { get; set; }

        [Required (ErrorMessage = "Card Code(cvv) is required")]
        [Display (Name = "Card Code")]
        [Range (111, 9999, ErrorMessage = "Invalid Card Code")]
        public string CardCode { get; set; }
    }
}
