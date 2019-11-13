using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PBS.Business.Core.AuthorizeNetApiModels.GetBatchList.Request;
using PBS.Business.Core.AuthorizeNetApiModels.GetBatchList.Response;
using PBS.Business.Core.AuthorizeNetApiModels.GetTransactionDetails.Request;
using PBS.Business.Core.AuthorizeNetApiModels.GetTransactionDetails.Response;
using PBS.Business.Core.AuthorizeNetApiModels.GetTransactions.Request;
using PBS.Business.Core.AuthorizeNetApiModels.GetTransactions.Response;
using PBS.Business.Core.AuthorizeNetApiModels.GetUnsettledTransacions.Request;
using PBS.Business.Core.AuthorizeNetApiModels.GetUnsettledTransacions.Response;
using PBS.Business.Core.AuthorizeNetApiModels.Request;
using PBS.Business.Core.Models;
using PBS.Web.Areas.Admin.Models;
using PBS.Web.Helpers;
using PBS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Web.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class TransactionsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApiHelper _apiHelper;
        private readonly DataProtector _dataProtector;

        public TransactionsController (IConfiguration configuration,
            IApiHelper apiHelper,
            DataProtector dataProtector)
        {
            _configuration = configuration;
            _apiHelper = apiHelper;
            _dataProtector = dataProtector;
        }

        #region Index (List of Batches)
        [HttpGet]
        public IActionResult Index ()
        {
            ErrorViewModel errorModel = new ErrorViewModel ();
            TransactionsIndexModel model = new TransactionsIndexModel ();
            GetUnsettledTransactionsRequestBody transactionsRequestBody = BuildGetUnsettledTransactionsRequestBody ();
            ResponseDetails response = _apiHelper.SendPaymentApiRequest (transactionsRequestBody);

            if (response.Success)
            {
                GetUnsettledTransactionsResponseBody ResponseBody = JsonConvert.DeserializeObject<GetUnsettledTransactionsResponseBody> (response.Data.ToString ());

                if (ResponseBody.Messages.ResultCode.ToLower () == "ok")
                {
                    if (ResponseBody.Transactions != null)
                    {
                        model.Transactions = ResponseBody.Transactions;
                        model.Transactions = ProtectTransactionId (model.Transactions);
                    }

                    return View (model);
                }
                else
                {
                    errorModel.Message = ResponseBody.Messages.Message.First ().Text;
                }
            }
            else
            {
                errorModel.Message = response.Data.ToString ();
            }

            return View ("Error", errorModel);
        }

        [HttpPost]
        public IActionResult Index (TransactionsIndexModel model)
        {
            if (!Validate (model))
            {
                return View (model);
            }

            ErrorViewModel errorModel = new ErrorViewModel ();
            GetBatchRequestBody requestBody = BuildGetSattledBatchRequestBody (model);

            // Get Batches
            ResponseDetails response = _apiHelper.SendPaymentApiRequest (requestBody);

            if (response.Success)
            {
                GetBatchResponseBody responseBody = JsonConvert.DeserializeObject<GetBatchResponseBody> (response.Data.ToString ());

                if (responseBody.Messages.ResultCode.ToLower () == "ok")
                {
                    model.BatchItems = responseBody.BatchList;

                    if (model.BatchItems != null)
                    {
                        model.BatchItems = ProtectBatchId (model.BatchItems);
                    }
                }
                else
                {
                    errorModel.Message = responseBody.Messages.Message.First ().Text;
                }
            }
            else
            {
                errorModel.Message = response.Data.ToString ();
            }

            // Get Unsettled transaction
            GetUnsettledTransactionsRequestBody transactionsRequestBody = BuildGetUnsettledTransactionsRequestBody ();
            response = _apiHelper.SendPaymentApiRequest (transactionsRequestBody);

            if (response.Success)
            {
                GetUnsettledTransactionsResponseBody transactionsResponseBody = JsonConvert.DeserializeObject<GetUnsettledTransactionsResponseBody> (response.Data.ToString ());

                if (transactionsResponseBody.Messages.ResultCode.ToLower () == "ok")
                {
                    if (transactionsResponseBody.Transactions != null)
                    {
                        model.Transactions = transactionsResponseBody.Transactions;
                        model.Transactions = ProtectTransactionId (model.Transactions);
                    }

                    // got all data now return view
                    return View (model);
                }
                else
                {
                    errorModel.Message = transactionsResponseBody.Messages.Message.First ().Text;
                }
            }
            else
            {
                errorModel.Message = response.Data.ToString ();
            }

            return View ("Error", errorModel);
        }
        #endregion

        #region Batch Details (List of Transactions)
        public IActionResult BatchDetails (string id)
        {
            id = _dataProtector.UnprotectString (id);

            ErrorViewModel errorModel = new ErrorViewModel ();
            GetTransactionRequestBody requestBody = BuildGetTransactionsRequestBody (id);

            ResponseDetails response = _apiHelper.SendPaymentApiRequest (requestBody);

            if (response.Success)
            {
                GetTransactionResponseBody responseBody = JsonConvert.DeserializeObject<GetTransactionResponseBody> (response.Data.ToString ());

                if (responseBody.Messages.ResultCode.ToLower () == "ok")
                {
                    BatchDetailsModel model = new BatchDetailsModel ()
                    {
                        SettledTransactions = responseBody.Transactions
                    };

                    model.SettledTransactions = ProtectTransactionId (model.SettledTransactions);

                    return View (model);
                }
                else
                {
                    errorModel.Message = "No records found";
                }
            }
            else
            {
                errorModel.Message = response.Data.ToString ();
            }

            return View ("Error", errorModel);
        }
        #endregion

        #region Transaction Details
        public IActionResult Details (string id)
        {
            id = _dataProtector.UnprotectString (id);

            ErrorViewModel errorModel = new ErrorViewModel ();
            GetTransactionDetailsRequestBody requestBody = BuildGetTransactionDetailsModel (id);

            ResponseDetails response = _apiHelper.SendPaymentApiRequest (requestBody);

            if (response.Success)
            {
                GetTransactionDetailsResponseBody responseBody = JsonConvert.DeserializeObject<GetTransactionDetailsResponseBody> (response.Data.ToString ());

                if (responseBody.Messages.ResultCode.ToLower () == "ok")
                {
                    TransactionDetailsModel model = new TransactionDetailsModel ()
                    {
                        Transaction = responseBody.Transaction
                    };

                    model.Transaction.EncryptedTransactionId = _dataProtector.Protect (model.Transaction.TransId);

                    return View (model);
                }
                else
                {
                    errorModel.Message = responseBody.Messages.Message.First ().Text;
                }
            }
            else
            {
                errorModel.Message = response.Data.ToString ();
            }

            return View ("Error", errorModel);
        }
        #endregion

        #region Private Methods
        private GetBatchRequestBody BuildGetSattledBatchRequestBody (TransactionsIndexModel model)
        {
            return new GetBatchRequestBody ()
            {
                GetGetSettledBatchListRequest = new GetSettledBatchListRequest ()
                {
                    FirstSettlementDate = model.StartDate,
                    lastSettlementDate = model.StartDate.AddDays (model.Days),
                    MerchantAuthentication = BuildMerchantAuthenticationModel ()
                }
            };
        }

        private GetUnsettledTransactionsRequestBody BuildGetUnsettledTransactionsRequestBody ()
        {
            return new GetUnsettledTransactionsRequestBody ()
            {
                GetUnsettledTransactionListRequest = new GetUnsettledTransactionListRequest ()
                {
                    MerchantAuthentication = BuildMerchantAuthenticationModel (),
                    Sorting = new Sorting (),
                    Paging = new Paging ()
                }
            };
        }

        private GetTransactionRequestBody BuildGetTransactionsRequestBody (string batchId)
        {
            return new GetTransactionRequestBody ()
            {
                GetTransactionListRequest = new GetTransactionListRequest ()
                {
                    MerchantAuthentication = BuildMerchantAuthenticationModel (),
                    BatchId = batchId,
                    Sorting = new Sorting (),
                    Paging = new Paging ()
                }
            };
        }

        private GetTransactionDetailsRequestBody BuildGetTransactionDetailsModel (string id)
        {
            return new GetTransactionDetailsRequestBody ()
            {
                GetTransactionDetailsRequest = new GetTransactionDetailsRequest ()
                {
                    MerchantAuthentication = BuildMerchantAuthenticationModel (),
                    TransId = id
                }
            };
        }

        private MerchantAuthentication BuildMerchantAuthenticationModel ()
        {
            return new MerchantAuthentication ()
            {
                Name = _configuration.GetSection ("AppSettings:LoginId").Value,
                TransactionKey = _configuration.GetSection ("AppSettings:TransactionKey").Value
            };
        }

        private bool Validate (TransactionsIndexModel model)
        {
            if (model.Days > 30)
            {
                ModelState.AddModelError ("", "sattlement can be retrived for 30 days at max");

                if ((DateTime.Now - model.StartDate).TotalDays < 1)
                {
                    ModelState.AddModelError ("", "Start date should older than today");
                }

                return false;
            }

            return true;
        }

        private List<BatchItem> ProtectBatchId (List<BatchItem> model)
        {
            return model.Select (x =>
            {
                x.EncryptedBatchId = _dataProtector.Protect (x.BatchId);

                return x;
            }).ToList ();
        }

        private List<Transaction> ProtectTransactionId (List<Transaction> model)
        {
            return model.Select (x =>
            {
                x.EncryptedTransactionId = _dataProtector.Protect (x.TransId);

                return x;
            }).ToList ();
        }
        #endregion
    }
}