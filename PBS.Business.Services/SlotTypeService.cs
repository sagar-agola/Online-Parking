using AutoMapper;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Business.Services
{
    public class SlotTypeService : ISlotTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SlotTypeService (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

                return _mapper.Map<SlotTypeViewModel> (model);
            }

            return null;
        }

        public List<SlotTypeViewModel> GetAll ()
        {
            List<SlotType> model = _unitOfWork.SlotTypeRepository.GetAll ();

            return _mapper.Map<List<SlotTypeViewModel>> (model);
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
