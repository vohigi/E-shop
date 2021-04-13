using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using EShop.Data;
using EShop.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EShop
{
    public class Startup
    {
        
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("IdentityDb"), b => b.MigrationsAssembly("EShop")));
            services.AddDbContext<ShopContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("EShopDb"), 
                    b => b.MigrationsAssembly("EShop")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            CultureInfo[] cultures = {
                new CultureInfo("ua"),
                new CultureInfo("ru"),
                new CultureInfo("en")
            };
            cultures[1].NumberFormat = CultureInfo.GetCultureInfo("en-US").NumberFormat;
            cultures[0].NumberFormat = CultureInfo.GetCultureInfo("en-US").NumberFormat;

            services.Configure<RequestLocalizationOptions>(opt =>
            {
                opt.DefaultRequestCulture = new RequestCulture(cultures[0]);
                opt.SupportedCultures = cultures;
                opt.SupportedUICultures = cultures;
            });
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IdentityContext identityContext, 
            ShopContext shopContext)
        {
            InitDb(identityContext, shopContext);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            { endpoints.MapRazorPages(); });
        }

        private static void InitDb(IdentityContext identityContext, ShopContext shopContext)
        {
            identityContext.Database.Migrate();
            shopContext.Database.Migrate();

            RoleInitializer.AdministratorInitialize(identityContext);
        }
    }
}