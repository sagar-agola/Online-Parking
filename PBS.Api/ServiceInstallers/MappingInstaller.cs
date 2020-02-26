using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PBS.Business.Utilities.Mappings;

namespace PBS.Api.ServiceInstallers
{
    public class MappingInstaller : IServiceInstaller
    {
        public void InstallServices (IServiceCollection services, IConfiguration configuration)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            services.AddAutoMapper ();
#pragma warning restore CS0618 // Type or member is obsolete

            services.AddSingleton<IUserMapping, UserMapping> ();
            services.AddSingleton<IParkingLotMapping, ParkingLotMapping> ();
            services.AddSingleton<ISlotMapping, SlotMapping> ();
            services.AddSingleton<IRoleMapping, RoleMapping> ();
            services.AddSingleton<IBookingMapping, BookingMapping> ();
        }
    }
}
