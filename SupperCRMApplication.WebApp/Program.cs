using Microsoft.EntityFrameworkCore;
using SupperCRMApplication.DataAccess;
using SupperCRMApplication.DataAccess.Context;
using SupperCRMApplication.Services;

namespace SupperCRMApplication.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                //options.UseLazyLoadingProxies();
            });
            builder.Services.AddControllersWithViews();
            //session ayaða kaldýrmak için eklenecek servisler
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(opts =>
            {
                opts.Cookie.Name = "suppercrm.session";
                opts.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            //Dependency Injection
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IClientService, ClientService>();

            //Dependency Injection
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            //Dependency Injection
            builder.Services.AddScoped<IIssueRepository, IssueRepository>();
            builder.Services.AddScoped<IIssueService, IssueService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //session u devreye almasý için pipeline a ekledik .
            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Dashboard}/{id?}");

            app.Run();
        }
    }
}