using PBS.Business.Core.AuthorizeNetApiModels.GetBatchList.Response;
using PBS.Business.Core.AuthorizeNetApiModels.GetTransactions.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Areas.Admin.Models
{
    public class TransactionsIndexModel
    {
        public List<BatchItem> BatchItems { get; set; } = new List<BatchItem> ();

        public List<Transaction> Transactions { get; set; } = new List<Transaction> ();

        [Display (Name = "Start Date")]
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        public int Days { get; set; }
    }
}
