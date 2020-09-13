using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json.Serialization;

using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

using LazZiya.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Identity;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add detection services container and device resolver service.
            services.AddDetection();

            // Add framework services.
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));



            services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(Configuration["Data:SportStoreidentity:ConnectionString"]));
            
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();



            //services.AddTransient<IProductRepository, FakeProductRepository>();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //298 ��������� ������� �������������� ��� ������ ������ ������ ConfigureServices () ������ Startup.
            services.AddTransient<IOrderRepository, EFOrderRepository>();

            services.AddMvc(option => option.EnableEndpointRouting = false);

            //�� ���������� ��������� ������ ������� ������������ � ��������������
            //��������� ������, ��� ������������ ����� ������, ������� �������� ��
            //������� � ������������� � ������������������� ��������, ��������� �������������.
            //�������������� ASP.NET ���������� ����� ��� ������ �������� �������� ��������� ������,
            //� ��� ����� �������� ��� � ������, ��� �� � ����� ���������.������������� ������ �������
            //�������� ��������, �� ������ ������ ����� �������, ����� ���������� ��������������� ���
            //���������������.��������� ��������� ������� ������� ���������� � ����� Startup ����� �
            //�������������� ������������ �����������
            services.AddMemoryCache();
            services.AddSession();

            services.AddMvc()
                .AddNewtonsoftJson();


            //Json
            //var builder = services
            //    .AddControllersWithViews()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            //    .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);


            services.AddControllers(setupAction =>
                setupAction.ReturnHttpNotAcceptable = true
            ).AddXmlDataContractSerializerFormatters().AddNewtonsoftJson(setupAction =>
                setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            //services
            //    .AddControllers()
            //    .AddNewtonsoftJson();

            //services.AddControllers(options =>
            //{
            //    options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            //});

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddTransient<ITagHelperComponent, LocalizationValidationScriptsTagHelperComponent>();

        }

            //Json

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRequestLocalization("uk-UA");
            //app.UseRequestLocalization("ru-RU");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();

            //����� ������ AddMemoryCache ( ) ����������� ��������� ������ � ������.
            //����� AddSession () ������������ ������, ������������  ��� ������� �
            //������ ������, � ����� UseSession () ��������� ������� ������� �������������
            //������������� ������� � ��������, ����� ��� ��������� �� �������.
            app.UseSession();
            app.UseAuthentication();
            //��������� ����� ������������� � ����� Startup. cs.
            //��������� ����� ������������� URL ����� /?category=Soccer
            //� ������ Configure () ������Startup, ����� ������� ����� ������� ����� URL
            //app.UseMvc(routes =>
            //{

            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Product}/{action=List}/{id?}");
            //});

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults:new
                    {
                        controller="Product",
                        action="List"
                    });

                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    });

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    });

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    });

                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}");




            });

            app.UseDetection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapDefaultControllerRoute();
            });
            //SeedData.EnsurePopulated(app);
            ////����� ���������� ��������� ���������� ���� ������ Identit� �� ����� ������� ����������, ������� � ����� Configure() ������ Startup ��������.
            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}
