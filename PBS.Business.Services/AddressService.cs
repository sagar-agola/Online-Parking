using AutoMapper;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;

namespace PBS.Business.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressService (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public AddressViewModel Add (AddressViewModel model)
        {
            Address modelMapping = _mapper.Map<Address> (model);

            modelMapping = _unitOfWork.AddressRepository.Add (modelMapping);
            _unitOfWork.SaveChanges ();

            return _mapper.Map<AddressViewModel> (modelMapping);
        }

        public AddressViewModel Update(AddressViewModel model)
        {
            Address modelMapping = _mapper.Map<Address> (model);

            bool success = _unitOfWork.AddressRepository.Update (modelMapping);

            if (success)
            {
                _unitOfWork.SaveChanges ();

                return _mapper.Map<AddressViewModel> (modelMapping);
            }

            return null;
        }
    }
}
