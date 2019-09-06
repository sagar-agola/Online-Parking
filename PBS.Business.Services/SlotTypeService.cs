using AutoMapper;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;
using System.Collections.Generic;

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

        public List<SlotTypeViewModel> GetAll ()
        {
            List<SlotType> model = _unitOfWork.SlotTypeRepository.GetAll ();

            return _mapper.Map<List<SlotTypeViewModel>> (model);
        }
    }
}
