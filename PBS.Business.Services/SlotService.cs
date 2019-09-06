using AutoMapper;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Utilities.Mappings;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Services
{
    public class SlotService : ISlotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISlotMapping _slotMapping;

        public SlotService (IUnitOfWork unitOfWork, IMapper mapper, ISlotMapping slotMapping)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _slotMapping = slotMapping;
        }

        public SlotViewModel Add (SlotViewModel model)
        {
            Slot modelMapping = _mapper.Map<Slot> (model);

            modelMapping = _unitOfWork.SlotRepository.Add (modelMapping);

            if (modelMapping != null)
            {
                _unitOfWork.SaveChanges ();
                return Get (modelMapping.Id);
            }

            return null;
        }

        public SlotViewModel Get (int id)
        {
            if (_unitOfWork.SlotRepository.SlotExists (id))
            {
                Slot model = _unitOfWork.SlotRepository.Get (id);

                return _slotMapping.MapSlot (model);
            }

            return null;
        }

        public List<SlotViewModel> GetAll ()
        {
            List<Slot> model = _unitOfWork.SlotRepository.GetAll ();
            return _slotMapping.MapSlotList (model);
        }

        public List<SlotViewModel> GetByParkingLot (int parkingLotId)
        {
            if (_unitOfWork.ParkingLotRepository.ParkingLotExists (parkingLotId))
            {
                List<Slot> model = _unitOfWork.SlotRepository.GetByParkingLot (parkingLotId);

                return _slotMapping.MapSlotList (model);
            }

            return null;
        }

        public bool Remove (int id)
        {
            if (_unitOfWork.SlotRepository.SlotExists (id))
            {
                Slot model = _unitOfWork.SlotRepository.Get (id);
                _unitOfWork.SlotRepository.Remove (model);
                _unitOfWork.SaveChanges ();

                return true;
            }

            return false;
        }

        public bool Update (SlotViewModel model)
        {
            if (_unitOfWork.SlotRepository.SlotExists (model.Id))
            {
                Slot modelMapping = _mapper.Map<Slot> (model);

                _unitOfWork.SlotRepository.Update (modelMapping);
                _unitOfWork.SaveChanges ();

                return true;
            }

            return false;
        }
    }
}
