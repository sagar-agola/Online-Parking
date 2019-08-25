using PBS.Business.Core.BusinessModels;

namespace PBS.Business.Contracts.Services
{
    public interface IAddressService
    {
        AddressViewModel Add (AddressViewModel model);
        AddressViewModel Update (AddressViewModel model);
    }
}
