using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using API.Services;
using API.Data;

namespace API.Extensions;

public static class ApplicationSeviceExtensions //static class: allow us to use the method without need to instantiate the class
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();

        //Swagger configuring
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

}
