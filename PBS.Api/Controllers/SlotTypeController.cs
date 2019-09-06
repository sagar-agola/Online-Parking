using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.ApiRoute;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using System.Collections.Generic;

namespace PBS.Api.Controllers
{
    [ApiController]
    public class SlotTypeController : ControllerBase
    {
        private readonly ISlotTypeService _slotTypeService;

        public SlotTypeController (ISlotTypeService slotTypeService)
        {
            _slotTypeService = slotTypeService;
        }

        [HttpGet (ApiRoutes.SlotType.GetAll)]
        public object GetAll ()
        {
            List<SlotTypeViewModel> model = _slotTypeService.GetAll ();

            return new ResponseDetails (true, model);
        }
    }
}