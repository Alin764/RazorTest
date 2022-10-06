using CommonRazorEngine;
using CommonRazorEngine.Views;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.PlatformAbstractions;
using System.Diagnostics;
using System.Reflection;


namespace WebJobConsoleApp
{
    internal class Program
    {
        private static IConfigurationRoot Configuration;
        private static IServiceProvider ServiceProvider;

        static async Task Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                            .Build();

            ServiceCollection services = ConfigureServices();
            ServiceProvider = services.BuildServiceProvider();

            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                //b.AddAzureStorageCoreServices();
            });

            var host = builder.Build();
            using (host)
            {
              //await host.RunAsync();
            }

            // Test razor view engine
            var viewToStringEngine = ServiceProvider.GetService<IRazorViewToStringRenderer>();
            string htmlContent = await viewToStringEngine.RenderToStringAsync<MyView>("~/Views/MyView.cshtml", new MyView());

            Console.WriteLine(htmlContent);
        }

        private static ServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(Configuration);

            var applicationEnvironment = PlatformServices.Default.Application;
            services.AddSingleton(applicationEnvironment);

            services.AddSingleton<Microsoft.AspNetCore.Hosting.IWebHostEnvironment>(new WebJobHostEnvironment
            {
                ApplicationName = Assembly.GetEntryAssembly().GetName().Name,
                //ApplicationName = typeof(IRazorViewToStringRenderer).Assembly.GetName().Name,
            });

            var listener = new DiagnosticListener("Microsoft.AspNetCore");
            services.AddSingleton<DiagnosticListener>(listener);
            services.AddSingleton<DiagnosticSource>(listener);
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>(sp => new LoggerFactory());

            services.AddMvcCore().AddRazorViewEngine();

            services.AddCommonRazorEngine(Configuration);

            return services;
        }
    }

    public class WebJobHostEnvironment : IWebHostEnvironment
    {
        public string WebRootPath { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string ApplicationName { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public string EnvironmentName { get; set; }
    }
}