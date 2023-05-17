namespace WebAPI.StartUp
{
    public class OAuth
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "254855841315-rmg1jjthsbhgqvl7kqcuopgb5d0n4kpf.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });
        }
    }
}
