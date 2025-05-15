using Application.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.FileStorage;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");        

        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IFileUploader, CloudinaryMediaUploader>();
        services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));

        return services;
    }
}