using AutoMapper;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Utilities.Mappings;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IBookingMapping _bookingMapping;

        public BookingService (IUnitOfWork unitofWork, IMapper mapper, IBookingMapping bookingMapping)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            this._bookingMapping = bookingMapping;
        }

        public BookingViewModel Add (BookingViewModel model)
        {
            Booking modelMapping = _mapper.Map<Booking> (model);

            modelMapping = _unitofWork.BookingRepository.Add (modelMapping);
            _unitofWork.SaveChanges ();

            return _mapper.Map<BookingViewModel> (modelMapping);
        }

        public BookingViewModel Get (int id)
        {
            bool exists = _unitofWork.BookingRepository.BookingExists (id);

            if (exists)
            {
                Booking model = _unitofWork.BookingRepository.Get (id);

                return _bookingMapping.MapBooking (model);
            }

            return null;
        }

        public List<BookingViewModel> GetAll ()
        {
            List<Booking> model = _unitofWork.BookingRepository.GetAll ();

            return _bookingMapping.MapBookingList (model);
        }

        public List<BookingViewModel> GetByUser (int userId)
        {
            List<Booking> model = _unitofWork.BookingRepository.GetByUser (userId);

            return _bookingMapping.MapBookingList (model);
        }

        public bool Update (BookingViewModel model)
        {
            if (_unitofWork.BookingRepository.BookingExists (model.Id))
            {
                Booking modelMapping = _mapper.Map<Booking> (model);

                _unitofWork.BookingRepository.Update (modelMapping);
                _unitofWork.SaveChanges ();

                return true;
            }

            return false;
        }
    }
}
