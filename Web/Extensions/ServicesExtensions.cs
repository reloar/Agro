using Domain.Interface;
using Domain.ServiceInterface;
using Domain.Utilities;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Services.CyberPay;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            //Data
            services.AddScoped<DbContext, AgroContext>();

            //Repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<ICyberPay, CyberPayService>();
            services.AddScoped<ICyberPay, PayStackService>();

            services.AddScoped<INotificationUtility, NotificationRepository>();

        }
    }

}
