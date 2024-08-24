using Application.Common.Interfaces;
using Application.Scraping.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Services.Scraping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddScoped<IWebScraper, WebScraper>();
            services.AddScoped<IHtmlParser, HtmlParser>();
            services.AddScoped<ISearchServiceFactory, SearchServiceFactory>();
            services.AddScoped<GoogleSearchService>();
            services.AddScoped<BingSearchService>();
            return services;
        }
    }
}
