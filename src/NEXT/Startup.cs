using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Routing.Template;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.StaticFiles;

namespace NEXT
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            ////static files and filetypes
            //IFileProvider provider = new PhysicalFileProvider(Path.GetFullPath(""));
            //var filetypeprovider = new FileExtensionContentTypeProvider();
            //filetypeprovider.Mappings.Add(".myapp", "application/x-msdownload");
            DefaultFilesOptions fileOptions = new DefaultFilesOptions();
            
            //routes
            var RouteBuilder = new RouteBuilder();
            RouteBuilder.ServiceProvider = app.ApplicationServices;
            RouteBuilder.Routes.Add(new TemplateRoute(new RESTRouter(), "REST/", app.ApplicationServices.GetService<IInlineConstraintResolver>()));
            RouteBuilder.Routes.Add(new TemplateRoute(new langRouter(), "lang/{l10n:alpha}", app.ApplicationServices.GetService<IInlineConstraintResolver>()));
            RouteBuilder.Build();
            app.UseRouter(RouteBuilder.Build());

            Console.WriteLine("The current directory is {0}", Directory.GetCurrentDirectory());
            FileServerOptions fileServerOptions = new FileServerOptions() {
                //FileProvider = new PhysicalFileProvider(@"D:\Source\WebApplication1\src\WebApplication1\MyStaticFiles"),
                RequestPath = new PathString("/app"),
                EnableDirectoryBrowsing = true
            };

            app.UseFileServer(fileServerOptions);

            fileOptions.DefaultFileNames.Clear();
            fileOptions.DefaultFileNames.Add("htmlpage.html");
            app.UseDefaultFiles();                    
            app.UseStaticFiles();


        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
