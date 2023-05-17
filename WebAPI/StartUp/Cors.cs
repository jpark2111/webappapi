namespace WebAPI.StartUp
{
    public class Cors
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllCors", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .SetIsOriginAllowed(requestingOrigin => true)
                    .Build();
                });
            });
        }
    }
}
