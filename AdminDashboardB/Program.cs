using DbContextB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ModelsB.Authentication_and_Authorization_B;
using System.Globalization;
using WebApplication1.Data;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
            //    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<BTechDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<ApplicationUserB>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<BTechDbContext>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUserB, IdentityRole>().AddEntityFrameworkStores<BTechDbContext>()
                    .AddDefaultTokenProviders().AddDefaultUI();

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-EG")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });


            builder.Services.AddControllersWithViews();
           
            var app = builder.Build();

            //using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var services = serviceScope.ServiceProvider;
            //    var context = services.GetRequiredService<BTechDbContext>();
            //    var seeder = new DataSeeder(context);
            //    try
            //    {
            //        RoleInitializer.Initialize(services).Wait();
            //        seeder.Seed();
            //    }
            //    catch (Exception ex)
            //    {
            //        // Log the error or handle accordingly
            //    }
            //}

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}