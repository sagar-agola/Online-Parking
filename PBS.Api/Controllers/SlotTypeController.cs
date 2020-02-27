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

        [HttpGet (ApiRoutes.SlotType.Get)]
        public object Get (int id)
        {
            SlotTypeViewModel model = _slotTypeService.Get (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"Slot type with Id : { id } not found.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpPost (ApiRoutes.SlotType.Add)]
        public object Add (SlotTypeViewModel model)
        {
            model = _slotTypeService.Add (model);

            if (model.Id == 0)
            {
                return new ResponseDetails (false, "Could not add new slot type");
            }

            return new ResponseDetails (true, model);
        }

        [HttpDelete (ApiRoutes.SlotType.Remove)]
        public object Remove (int id)
        {
            bool isDeleted = _slotTypeService.Remove (id);

            if (isDeleted)
            {
                return new ResponseDetails (true, null);
            }

            return new ResponseDetails (false, null);
        }
    }
}