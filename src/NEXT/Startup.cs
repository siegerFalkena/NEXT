﻿using System;
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
using NEXT.DB.Models;
using Microsoft.Data.Entity;
using NEXT.DB;
using NEXT.API.Repositories;

using NEXT.API.Query;
using AutoMapper;

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
            var connection = @"MultipleActiveResultSets=true;Server=localhost,1433;Database=NEXT;User ID=NEXT;Password=password31!;";
            services.AddEntityFramework().AddSqlServer().AddDbContext<NEXTContext>(options => options.UseSqlServer(connection));
            //services.AddIdentity<User, Role>().AddEntityFrameworkStores<NEXTContext>().AddDefaultTokenProviders();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IVendorRepository, VendorRepository>();
            services.AddTransient<IAttributeRepository, AttributeRepository>();
            services.AddTransient<IAttributeTypeRepository, AttributeTypeRepository>();
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddSingleton<IMappingConfigProvider, Mapping>();
            services.AddAuthentication();
            services.AddRouting();
            services.AddMvc();
            services.AddSession();
            services.AddCaching();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseIISPlatformHandler();

            DefaultFilesOptions fileOptions = new DefaultFilesOptions();

            //routes
            var routeBuilder = new RouteBuilder();
            routeBuilder.ServiceProvider = app.ApplicationServices;
            //routeBuilder.Routes.Add(new TemplateRoute(new AuthRouter(), "auth/", app.ApplicationServices.GetService<IInlineConstraintResolver>()));
            routeBuilder.Build();
            app.UseRouter(routeBuilder.Build());

            Console.WriteLine("The current directory is {0}", Directory.GetCurrentDirectory());
            FileServerOptions fileServerOptions = new FileServerOptions()
            {
                RequestPath = new PathString("/app"),
                EnableDirectoryBrowsing = true
            };

            app.UseFileServer(fileServerOptions);

            fileOptions.DefaultFileNames.Clear();
            fileOptions.DefaultFileNames.Add("htmlpage.html");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookieAuthentication(options =>
            {
                options.AuthenticationScheme = "MyCookieMiddlewareInstance";
                options.LoginPath = new PathString("/");
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
            });

            

            app.UseCookieAuthentication(options =>
            {
                options.AuthenticationScheme = "NEXTAuthenticationScheme";
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
            });


            if (env.IsDevelopment())
            {
                app.UseRuntimeInfoPage();
                app.UseDeveloperExceptionPage();
            }
            //app.UseIdentity();
            app.UseMvcWithDefaultRoute();

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
