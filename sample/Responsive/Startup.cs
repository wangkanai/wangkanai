// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Responsive
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
            // Add responsive services.
            services.AddResponsive();
            // Or
            //services.AddResponsive(options =>
            //{
            //    options.View.DefaultTablet = DeviceType.Desktop;
            //    options.View.DefaultMobile = DeviceType.Mobile;
            //    options.View.DefaultDesktop = DeviceType.Desktop;
            //});

            // Add framework services.
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseResponsive();

            //app.UseResponsive(new ResponsiveOptions
            //{
            //    TabletDefault = DeviceType.Tablet
            //});

            //app.UseResponsive(options =>
            //{
            //    options.View.DefaultTablet = DeviceType.Desktop;
            //    options.View.DefaultMobile = DeviceType.Desktop;
            //    options.View.DefaultDesktop = DeviceType.Desktop;
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
