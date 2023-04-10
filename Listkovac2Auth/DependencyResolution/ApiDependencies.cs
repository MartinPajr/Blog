using ListkovacBL.DAO;

namespace Listkovac2Auth.DependencyResolution
{
    public static class ApiDependencies
    {
        public static IServiceCollection AddApiDependencies(this IServiceCollection services)
        {
            services.AddScoped<IGeneralDAO, GeneralDAO>();

            return services;
        }
    }
}