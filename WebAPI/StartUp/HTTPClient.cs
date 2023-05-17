namespace WebAPI.StartUp
{
    public class HTTPClient
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("Google", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://accounts.google.com/o/oauth2/v2/auth");
            });
            services.AddHttpClient("OAuth", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://oauth2.googleapis.com");
            });
        }
    }
}
