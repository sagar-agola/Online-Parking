using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.DAL;
using PBS.Business.Services;

namespace PBS.Api.ServiceInstallers
{
    public class DomainServicesInstaller : IServiceInstaller
    {
        public void InstallServices (IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork> ();
            services.AddScoped<IAuthService, AuthService> ();
            services.AddScoped<IUserService, UserService> ();
            services.AddScoped<IClaimService, ClaimService> ();
            services.AddScoped<IAddressService, AddressService> ();
            services.AddScoped<IParkingLotService, ParkingLotService> ();
            services.AddScoped<ISlotService, SlotService> ();
            services.AddScoped<ISlotTypeService, SlotTypeService> ();
            services.AddScoped<IRoleService, RoleService> ();
            services.AddScoped<IHomeService, HomeService> ();
            services.AddScoped<IBookingService, BookingService> ();
        }
    }
}
