using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Services;
using Elderson.Models;
using Serilog;
using Elderson.Hubs;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Http;

namespace Elderson
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
            
            services.AddRazorPages();
            services.AddSignalR();
            services.AddSession();
            services.AddTransient<UserService>();
            services.AddTransient<IncidentService>();
            services.AddTransient<ChatService>();
            services.AddDbContext<EldersonContext>();
            services.AddTransient<AdministratorService>();
            services.AddTransient<ScheduleService>();
            services.AddTransient<BookingService>();
            services.AddTransient<InventoryService>();
            services.AddTransient<FAQService>();
            services.AddTransient<FormService>();
            services.AddTransient<FormMedsService>();
            services.AddTransient<SupplierService>();
            services.AddTransient<MedicalHistoryService>();
            services.AddTransient<PrescriptionService>();
            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopCenter; });
            services.AddHttpContextAccessor();

            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriServices(uri);
            });

            services.AddControllers();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseNotyf();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute("default", "api/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapHub<ChatAppHub>("/chatAppHub");
            });
        }
    }
}
