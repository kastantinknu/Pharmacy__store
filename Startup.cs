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

            //298 Хранилище заказов регистрируется как служба внутри метода ConfigureServices () класса Startup.
            services.AddTransient<IOrderRepository, EFOrderRepository>();

            services.AddMvc(option => option.EnableEndpointRouting = false);

            //Мы собираемся сохранять детали корзины пользователя с использованием
            //состояния сеанса, что представляет собой данные, которые хранятся на
            //сервере и ассоциируются с последовательностью запросов, сделанных пользователем.
            //Инфраструктура ASP.NET предлагает целый ряд разных способов хранения состояния сеанса,
            //в том числе хранение его в памяти, что мы и будем применять.Преимуществом такого подхода
            //является простота, но данные сеанса будут утеряны, когда приложение останавливается или
            //перезапускается.Включение поддержки сеансов требует добавления в класс Startup служб и
            //промежуточного программного обеспечения
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

            //Вызов метода AddMemoryCache ( ) настраивает хранилище данных в памяти.
            //Метод AddSession () регистрирует службы, используемые  для доступа к
            //данным сеанса, а метод UseSession () позволяет системе сеансов автоматически
            //ассоциировать запросы с сеансами, когда они поступают от клиента.
            app.UseSession();
            app.UseAuthentication();
            //Изменение схемы маршрутизации в файле Startup. cs.
            //Изменение схемы маршрутизации URL вроде /?category=Soccer
            //в методе Configure () классаStartup, чтобы создать более удобный набор URL
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
            ////Чтобы обеспечить начальное заполнение базы данных Identitу во время запуска приложения, добавим в метод Configure() класса Startup оператор.
            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}
