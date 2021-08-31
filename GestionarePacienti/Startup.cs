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
using Microsoft.EntityFrameworkCore;
using GestionarePacienti.Data;
using GestionarePacienti.Services;
using GestionarePacienti.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using GestionarePacienti.Areas.Identity.Data;
using GestionarePacienti.Hubs;
using Microsoft.AspNetCore.Http;
using GestionarePacienti.Data.Entities;

namespace GestionarePacienti
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
            services.AddControllersWithViews();

            services.AddRazorPages(
            //    options=>
            //{sets home page as Login page
            //    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");}
            );

            services.AddSignalR();

            services.AddDbContext<GestionarePacientiContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("GestionarePacientiContext")));

            //https://forums.asp.net/t/2160569.aspx?i+have+issue+with+register+page
            services.AddDefaultIdentity<GestionarePacientiUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            //services.AddIdentity<GestionarePacientiUser, IdentityRole>()
            //        .AddEntityFrameworkStores<GestionarePacientiContext>()
            //        .AddDefaultTokenProviders();


            services.AddScoped<IRepository<Patient>, Repository<Patient>>();
            services.AddScoped<IAppointmentDetailRepository, AppointmentDetailRepository>();
            services.AddScoped<IRepository<Doctor>, Repository<Doctor>>();

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //enabling Authentication
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
