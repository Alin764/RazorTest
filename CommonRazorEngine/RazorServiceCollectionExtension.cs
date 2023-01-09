using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace CommonRazorEngine
{
    public static class RazorServiceCollectionExtension
    {
        /// <summary>
        /// Adds IRazorViewToStringRenderer.
        /// </summary>
        public static void AddCommonRazorEngine(this IServiceCollection services, IConfiguration configuration)
        {
            var fileProvider = new EmbeddedFileProvider(typeof(RazorViewToStringRenderer).Assembly);

            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {
                options.FileProviders.Clear();
                options.FileProviders.Add(fileProvider);
            });

            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
        }
    }
}
