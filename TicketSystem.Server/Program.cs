using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Server.Context;
using TicketSystem.Server.Models.Auth;
using TicketSystem.Server.Services;

namespace TicketSystem.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddScoped<TicketService>();
            builder.Services.AddScoped<ServiceAgreementService>();
            builder.Services.AddScoped<AssignedTicketService>();
            builder.Services.AddScoped<TicketDbContext>();
            builder.Services.AddScoped<TimelogSerivce>();
            builder.Services.AddScoped<RoleService>();

            builder.Services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")
            ));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

                options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
            })
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);

                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.MapStaticAssets();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<UserDbContext>();
                context.Database.Migrate();
                var roleService = services.GetRequiredService<RoleService>();
                roleService.SyncRoles().GetAwaiter().GetResult();

                // Not best practice but good enough for making a support user
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                ApplicationUser? user = userManager.FindByNameAsync("Emil1").GetAwaiter().GetResult();
                if (user != null && !userManager.IsInRoleAsync(user, "Support").GetAwaiter().GetResult())
                {
                    userManager.AddToRoleAsync(user, "Support").GetAwaiter().GetResult();
                }
            }

            app.Run();
        }
    }
}
