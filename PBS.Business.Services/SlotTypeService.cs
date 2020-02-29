using AutoMapper;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Utilities.Mappings;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Business.Services
{
    public class SlotTypeService : ISlotTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISlotTypeMapping _slotTypeMapping;

        public SlotTypeService (IUnitOfWork unitOfWork, IMapper mapper, ISlotTypeMapping slotTypeMapping)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _slotTypeMapping = slotTypeMapping;
        }

        public SlotTypeViewModel Add (SlotTypeViewModel model)
        {
            SlotType modelMapping = _mapper.Map<SlotType> (model);

            _unitOfWork.SlotTypeRepository.Add (modelMapping);
            _unitOfWork.SaveChanges ();

            return Get (modelMapping.Id);
        }

        public SlotTypeViewModel Get (int id)
        {
            if (_unitOfWork.SlotTypeRepository.SlotTypeExists (id))
            {
                SlotType model = _unitOfWork.SlotTypeRepository.Get (id);

                return _slotTypeMapping.MapSlotType (model);
            }

            return null;
        }

        public List<SlotTypeViewModel> GetAll ()
        {
            List<SlotType> model = _unitOfWork.SlotTypeRepository.GetAll ();

            return _slotTypeMapping.MapSlotTypes (model);
        }

        public bool Remove (int id)
        {
            if (_unitOfWork.SlotTypeRepository.SlotTypeExists (id))
            {
                SlotType model = _unitOfWork.SlotTypeRepository.Get (id);

                if (model.Slots.Any ())
                {
                    return false;
                }

                _unitOfWork.SlotTypeRepository.Remove (model);
                _unitOfWork.SaveChanges ();

                return true;
            }

            return false;
        }
    }
}
