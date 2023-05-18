namespace WebAPI.StartUp
{
    public class OAuth
    {
        private static IConfiguration? _configuration;
        public OAuth(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = _configuration.GetValue<string>("GoogleOAuth:Client");
                options.ClientSecret = _configuration.GetValue<string>("GoogleOAuth:Secret");
            });
        }
    }
}
