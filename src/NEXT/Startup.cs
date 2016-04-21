using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Routing.Template;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.Logging;

namespace NEXT
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddMvc();
            services.AddSession();
            services.AddCaching();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseIISPlatformHandler();

            ////static files and filetypes
            //IFileProvider provider = new PhysicalFileProvider(Path.GetFullPath(""));
            //var filetypeprovider = new FileExtensionContentTypeProvider();
            //filetypeprovider.Mappings.Add(".myapp", "application/x-msdownload");
            DefaultFilesOptions fileOptions = new DefaultFilesOptions();
            
            //routes
            var routeBuilder = new RouteBuilder();
            routeBuilder.ServiceProvider = app.ApplicationServices;
            //routeBuilder.Routes.Add(new TemplateRoute(new AuthRouter(), "auth/", app.ApplicationServices.GetService<IInlineConstraintResolver>()));
            routeBuilder.Build();
            app.UseRouter(routeBuilder.Build());

            Console.WriteLine("The current directory is {0}", Directory.GetCurrentDirectory());
            FileServerOptions fileServerOptions = new FileServerOptions() {
                RequestPath = new PathString("/app"),
                EnableDirectoryBrowsing = true
            };

            app.UseFileServer(fileServerOptions);

            fileOptions.DefaultFileNames.Clear();
            fileOptions.DefaultFileNames.Add("htmlpage.html");
            app.UseDefaultFiles();                    
            app.UseStaticFiles();
            app.UseCookieAuthentication(options => {
                options.AuthenticationScheme = "MyCookieMiddlewareInstance";
                options.LoginPath = new PathString("/");
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
            });

            app.UseMvcWithDefaultRoute();
            app.UseSession();    


        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
