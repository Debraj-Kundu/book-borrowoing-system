using DataLayer.DataContext;
using DataLayer.Repository.Implementation;
using DataLayer.Repository.Interface;
using DataLayer.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BookDomainDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Scoped);

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IBookUnitOfWork, BookUnitOfWork>();

            return services;
        }
    }
}
