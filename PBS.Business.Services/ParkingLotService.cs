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
    public class ParkingLotService : IParkingLotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IParkingLotMapping _parkingLotMapping;

        public ParkingLotService (IUnitOfWork unitOfWork, IMapper mapper, IParkingLotMapping parkingLotMapping)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _parkingLotMapping = parkingLotMapping;
        }

        public ParkingLotViewModel Add (ParkingLotViewModel model)
        {
            ParkingLot modelMapping = _mapper.Map<ParkingLot> (model);

            if (model.AddressViewModel != null)
            {
                modelMapping.Address = _mapper.Map<Address> (model.AddressViewModel);
            }

            if (model.SlotViewModels.Any ())
            {
                modelMapping.Slots = _mapper.Map<List<Slot>> (model.SlotViewModels);
            }

            modelMapping.IsActive = true;
            modelMapping.IsAproved = false;

            modelMapping = _unitOfWork.ParkingLotRepository.Add (modelMapping);
            _unitOfWork.SaveChanges ();

            return Get (modelMapping.Id);
        }

        public ParkingLotViewModel Get (int id)
        {
            if (_unitOfWork.ParkingLotRepository.ParkingLotExists (id))
            {
                ParkingLot model = _unitOfWork.ParkingLotRepository.Get (id);

                return _parkingLotMapping.MapParkingLot (model);
            }

            return null;
        }

        public List<ParkingLotViewModel> GetAll ()
        {
            List<ParkingLot> model = _unitOfWork.ParkingLotRepository.GetAll ();

            return _parkingLotMapping.MapParkingLotList (model);
        }

        public List<ParkingLotViewModel> GetByUser (int userId)
        {
            if (_unitOfWork.UserRepository.UserExists (userId))
            {
                List<ParkingLot> model = _unitOfWork.ParkingLotRepository.GetByUser (userId);

                return _parkingLotMapping.MapParkingLotList (model);
            }

            return null;
        }

        public bool Remove (int id)
        {
            bool parkingLotExists = _unitOfWork.ParkingLotRepository.ParkingLotExists (id);

            if (parkingLotExists)
            {
                ParkingLot model = _unitOfWork.ParkingLotRepository.Get (id);

                _unitOfWork.ParkingLotRepository.Remove (model);
                _unitOfWork.SaveChanges ();

                return true;
            }

            return false;
        }

        public ParkingLotViewModel Update (ParkingLotViewModel model)
        {
            if (_unitOfWork.ParkingLotRepository.ParkingLotExists (model.Id))
            {
                ParkingLot modelMapping = _mapper.Map<ParkingLot> (model);

                _unitOfWork.ParkingLotRepository.Update (modelMapping);
                _unitOfWork.SaveChanges ();

                return model;
            }

            return null;
        }
    }
}
