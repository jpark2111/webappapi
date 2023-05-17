using WebAPI.Repositories;

namespace WebAPI.StartUp;

public class DependencyInjection
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IGoogleAuthRepository, GoogleAuthRepository>();
        services.AddSingleton<IOrderRepository, OrderRepository>();
    }
}
