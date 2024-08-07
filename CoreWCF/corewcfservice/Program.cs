using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoreWCF;
using CoreWCF.Description;
using corewcfservice.IServices;
using corewcfservice.dataaccess;

namespace corewcfservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Kestrel to allow synchronous IO operations
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // Add CoreWCF services and metadata support
            builder.Services.AddServiceModelServices();
            builder.Services.AddServiceModelMetadata();

            // Add CORS policies
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    corsBuilder => corsBuilder
                        .WithOrigins("http://localhost:5097", "http://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Enable CORS for the specific origin
            app.UseCors("AllowSpecificOrigin");

            // Configure CoreWCF services and endpoints
            app.UseServiceModel(serviceBuilder =>
            {
                // Configure LookupService
                serviceBuilder.AddService<LookupService>()
                    .AddServiceEndpoint<LookupService, ILookupService>(
                        new BasicHttpBinding(BasicHttpSecurityMode.None),
                        "/LookupService.svc"
                    );

                // Configure AccountService
                serviceBuilder.AddService<AccountService>()
                    .AddServiceEndpoint<AccountService, dataaccess.IAccountService>(
                        new BasicHttpBinding(BasicHttpSecurityMode.None),
                        "/AccountService.svc"
                    );

                // Configure AddressService
                serviceBuilder.AddService<AddressService>()
                    .AddServiceEndpoint<AddressService, IAddressService>(
                        new BasicHttpBinding(BasicHttpSecurityMode.None),
                        "/AddressService.svc"
                    );

                // Configure AccountHolderService
                serviceBuilder.AddService<AccountHolderService>()
                    .AddServiceEndpoint<AccountHolderService, IAccountHolderService>(
                        new BasicHttpBinding(BasicHttpSecurityMode.None),
                        "/AccountHolderService.svc"
                    );

                // Configure TransactionService
                serviceBuilder.AddService<TransactionService>()
                    .AddServiceEndpoint<TransactionService, ITransactionService>(
                        new BasicHttpBinding(BasicHttpSecurityMode.None),
                        "/TransactionService.svc"
                    );

                // Configure metadata behavior for all services
                ConfigureMetadataBehavior(serviceBuilder, "/LookupService.svc");
                ConfigureMetadataBehavior(serviceBuilder, "/AccountService.svc");
                ConfigureMetadataBehavior(serviceBuilder, "/AddressService.svc");
                ConfigureMetadataBehavior(serviceBuilder, "/AccountHolderService.svc");
                ConfigureMetadataBehavior(serviceBuilder, "/TransactionService.svc");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            app.Run();
        }

        private static void ConfigureMetadataBehavior(IServiceBuilder serviceBuilder, string serviceUrl)
        {
            serviceBuilder.ConfigureServiceHostBase<LookupService>(host =>
            {
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    host.Description.Behaviors.Add(new ServiceMetadataBehavior
                    {
                        HttpGetEnabled = true,
                        HttpGetUrl = new Uri($"http://localhost:5097/{serviceUrl}/metadata")
                    });
                }
            });
        }
    }
}
