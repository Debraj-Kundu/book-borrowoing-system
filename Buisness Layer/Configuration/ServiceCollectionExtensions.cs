using BuisnessLayer.BookAppServices.Implementation;
using BuisnessLayer.BookAppServices.Interface;
using DataLayer.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString)
        {

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();

            //DbContext and repository configurations of Data Layer
            services.RegisterDataContext(connectionString);

            return services;
        }
    }
}
