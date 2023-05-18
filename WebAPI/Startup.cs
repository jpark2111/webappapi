

using WebAPI.StartUp;

namespace WebAPI;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureAppSettings(services);
    }

    private void ConfigureAppSettings(IServiceCollection services)
    {
        services.AddOptions();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        Swagger.ConfigureServices(services);

        Cors.ConfigureServices(services);

        //OAuth.ConfigureServices(services);

        HTTPClient.ConfigureServices(services);

        DependencyInjection.ConfigureServices(services, Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors("AllowAllCors");
        app.UseEndpoints(endpoints => 
        {
            endpoints.MapControllers();       
        });

        if(!env.IsDevelopment())
        {
            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();
            app.UseHsts();
        }
        
    }
}
