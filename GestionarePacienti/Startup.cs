using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using GestionarePacienti.Data;
using GestionarePacienti.Services;
using GestionarePacienti.Hubs;
using GestionarePacienti.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Authorization;

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

            services.AddScoped<IRepository<Patient>, Repository<Patient>>();
            services.AddScoped<IAppointmentDetailRepository, AppointmentDetailRepository>();
            services.AddScoped<IRepository<Doctor>, Repository<Doctor>>();

            services.AddHttpContextAccessor();

            //adding IDS4
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            //adds thw authentication service to the DI
            services.AddAuthentication(options =>
            {
                //using a cookie to sign in the user
                //we need the user to login, we will be using the OpenID Connect protocol
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                //adds handler that can process the cookie
                .AddCookie("Cookies")
                //configure the handler that performs the OpenID Connect
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";

                    //IDS address
                    options.Authority = "https://localhost:5001";
                    options.RequireHttpsMetadata = false;

                    //identify client
                    options.ClientId = "mvc";
                    options.ClientSecret = "mvcSecret";

                    //type of flow
                    //Grant types specify how a client can interact with the token service
                    options.ResponseType = "code";

                    //scopes represent something you want to protect and that clients want to access
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("offline_access");

                    //to persist the tokens from IdentityServer in the cookie (as they will be needed later)
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                });

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
                // to ensure the execution of the authentication services on each request
                //endpoints.MapDefaultControllerRoute()
                //         .RequireAuthorization();
                //but we are using the [Authorize] attribute on each controller

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
