using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.ApiRoute;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using System.Collections.Generic;

namespace PBS.Api.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController (IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet (ApiRoutes.Home.Search)]
        public object Search (string query)
        {
            List<ParkingLotViewModel> model = _homeService.Search (query);

            return new ResponseDetails (true, model);
        }
    }
}