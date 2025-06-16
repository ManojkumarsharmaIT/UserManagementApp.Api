using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UserManagementApp.Application.Interface;
using UserManagementApp.Core.Interface;

namespace UserManagementApp.Application
{
    public static class DependencyInjection
    {
         public static IServiceCollection AddApplicationDI(this IServiceCollection services)
         {
            services.AddScoped<IReqresApplication, ReqresApplication>();
            return services;
         }
    }
}
