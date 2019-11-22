using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediaCalendar.Areas.Identity;
using MediaCalendar.Data;
using MediaCalendar.Users;
using Microsoft.Extensions.Logging;

namespace MediaCalendar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<SeriesLibary>();
            services.AddScoped<MovieLibary>();
            services.AddSingleton<GetImdbSeries>();
            services.AddDbContext<Database>(options =>
            {
                //options.UseSqlite(Configuration.GetConnectionString("DatabaseFile"));
                options.UseSqlite("Data Source=DatabaseFile.sqlite");
            });

            #region Login
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=LoginDatabase.sqlite"));

            services.AddDefaultIdentity<ApplicationUser>()
                    .AddRoles<IdentityRole<int>>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultUI()
                    .AddClaimsPrincipalFactory<MyUserClaimsPrincipalFactory>();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings.
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequiredUniqueChars = 0;
            //});

            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        ctx.Response.Redirect("/Login");
                        return Task.CompletedTask;
                    }
                };
            });
            #endregion Login
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
        IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            // Create users with various roles if they do not exist in the database
            CreateUsers(serviceProvider);
        }

        private static void CreateUsers(IServiceProvider serviceProvider)
        {
            // Management
            AddUser(serviceProvider, "bestyrelse1@live.dk", "Qweqwe1", "Mark Ruffalo", "11111111111111", "Bestyrelse");
            AddUser(serviceProvider, "bestyrelse2@live.dk", "Qweqwe1", "Chris Hemsworth", "11111111111111", "Bestyrelse");
            AddUser(serviceProvider, "bestyrelse3@live.dk", "Qweqwe1", "Chris Evans", "11111111111111", "Formand");
            AddUser(serviceProvider, "bogholder@live.dk", "Qweqwe1", "Scarlett Johansson", "11111111111111", "Bogholder");

            // Members
            AddUser(serviceProvider, "andreas@live.dk", "Qweqwe1", "Andreas Biegel", "11111111111111", "Medlem");
            AddUser(serviceProvider, "kasper@live.dk", "Qweqwe1", "Kasper Nielsen", "11111111111111", "Medlem");
            AddUser(serviceProvider, "alex@live.dk", "Qweqwe1", "Alex Kristensen", "11111111111111", "Medlem");
            AddUser(serviceProvider, "yann@live.dk", "Qweqwe1", "Yann Perruchon", "11111111111111", "Medlem");
            AddUser(serviceProvider, "esben@live.dk", "Qweqwe1", "Esben Bach", "11111111111111", "Medlem");
            AddUser(serviceProvider, "kristian@live.dk", "Qweqwe1", "Kristian Mortensen", "11111111111111", "Medlem");
            AddUser(serviceProvider, "thomas@live.dk", "Qweqwe1", "Thomas Stubkjær", "11111111111111", "Medlem");
        }

        private static void AddUser(IServiceProvider serviceProvider, string userEmail, string userPassowrd,
                                    string userFullName, string userBankAccount, string roleName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            Task<ApplicationUser> checkAppUser = userManager.FindByEmailAsync(userEmail);
            checkAppUser.Wait();

            ApplicationUser appUser = checkAppUser.Result;

            if (checkAppUser.Result == null)
            {
                ApplicationUser newAppUser = new ApplicationUser
                {
                    Email = userEmail,
                    UserName = userEmail,
                    FullName = userFullName,
                    BankAccount = userBankAccount,
                    Role = roleName
                };

                Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(newAppUser, userPassowrd);
                taskCreateAppUser.Wait();
            }
        }
    }
}
