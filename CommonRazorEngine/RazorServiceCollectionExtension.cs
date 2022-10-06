using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonRazorEngine
{
    public static class RazorServiceCollectionExtension
    {
        /// <summary>
        /// Adds IRazorViewToStringRenderer.
        /// </summary>
        public static void AddCommonRazorEngine(this IServiceCollection services, IConfiguration configuration)
        {
            //var fileProvider = new EmbeddedFileProvider(typeof(RazorViewToStringRenderer).Assembly);

            // FileProviders property is not available anymore           
            services.Configure<RazorViewEngineOptions>(options =>
            {
                //options.FileProviders.Add(fileProvider);
            });

            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
        }
    }
}
