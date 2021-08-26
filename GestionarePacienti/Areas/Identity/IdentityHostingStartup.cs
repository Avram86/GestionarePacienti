using System;
using GestionarePacienti.Areas.Identity.Data;
using GestionarePacienti.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(GestionarePacienti.Areas.Identity.IdentityHostingStartup))]
namespace GestionarePacienti.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DbContextConnection")));

                //error if not commenting out: SCheme already exists
                //https://stackoverflow.com/questions/57253772/system-invalidoperationexception-scheme-already-exists-identity-application-a
                //services.AddDefaultIdentity<GestionarePacientiUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<ApplicationDbContext>();
            });
        }
    }
}