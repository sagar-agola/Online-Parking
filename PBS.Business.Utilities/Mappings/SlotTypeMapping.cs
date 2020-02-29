using AutoMapper;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Utilities.Mappings
{
    public class SlotTypeMapping : ISlotTypeMapping
    {
        private readonly IMapper _mapper;

        public SlotTypeMapping (IMapper mapper)
        {
            _mapper = mapper;
        }

        public SlotTypeViewModel MapSlotType (SlotType model)
        {
            SlotTypeViewModel modelMapping = _mapper.Map<SlotTypeViewModel> (model);

            modelMapping.SlotViewModels = _mapper.Map<List<SlotViewModel>> (model.Slots);

            return modelMapping;
        }

        public List<SlotTypeViewModel> MapSlotTypes (List<SlotType> model)
        {
            List<SlotTypeViewModel> modelMapping = new List<SlotTypeViewModel> ();

            for (int i = 0; i < model.Count; i++)
            {
                modelMapping.Add (MapSlotType (model[i]));
            }

            return modelMapping;
        }
    }
}
