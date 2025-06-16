using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagementApp.Core.Interface;
using UserManagementApp.Infrastructure.Repositories;
using UserManagementApp.Infrastructure.Services;
using Polly.Extensions.Http;
using Polly;

namespace UserManagementApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,IConfiguration config)
        {
            services.AddScoped<IExternalVendorRespository, ExternalVendorRespository>();
            services.AddScoped<IReqresHttpClientService, ReqresHttpClientService>();

            // Configure ReqresApiOptions from configuration
            services.Configure<ReqresApiOptions>(config.GetSection("ReqresApi"));
            services.AddHttpClient<IReqresHttpClientService, ReqresHttpClientService>((serviceProvider, options) =>
            {
                var reqresApiOptions = serviceProvider.GetRequiredService<IConfiguration>()
                    .GetSection("ReqresApi")
                    .Get<ReqresApiOptions>();

                if (reqresApiOptions is null)
                    throw new InvalidOperationException("ReqresApiOptions configuration section is missing or invalid.");

                options.BaseAddress = new Uri(reqresApiOptions.BaseUrl);

                if (!string.IsNullOrWhiteSpace(reqresApiOptions.ApiKey) && !string.IsNullOrWhiteSpace(reqresApiOptions.ApiValue))
                {
                    options.DefaultRequestHeaders.Add(reqresApiOptions.ApiKey, reqresApiOptions.ApiValue);
                }

            }).AddTransientHttpErrorPolicy(policyBuilder =>
                policyBuilder.WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            );//Polly policy for retrying on transient errors


            return services;
        }
    }
}
