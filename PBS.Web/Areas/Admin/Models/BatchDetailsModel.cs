using PBS.Business.Core.AuthorizeNetApiModels.GetTransactions.Response;
using System.Collections.Generic;

namespace PBS.Web.Areas.Admin.Models
{
    public class BatchDetailsModel
    {
        public List<Transaction> SettledTransactions { get; set; } = new List<Transaction> ();
    }
}
