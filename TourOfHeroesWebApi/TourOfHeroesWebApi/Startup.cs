// ReSharper disable StringLiteralTypo
namespace TourOfHeroesWebApi
{
    using System.IO;
    using System.Reflection;

    using TourOfHeroesCommon;
    using TourOfHeroesData.Seeder.Contracts;
    using TourOfHeroesMapping.Mapping;
    using TourOfHeroesDTOs.HeroDtos;
    using TourOfHeroesData;
    using TourOfHeroesServices.RealTimeHub;

    using NLog;
    using Infrastructure.Extensions;
    using GlobalErrorHandling.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore.Internal;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .SetupMvc()
                .AddDatabase(this.Configuration)
                .AddIdentity()
                .AddJwtAuthentication(services.GetApplicationSettings(this.Configuration))
                .AddApplicationServices()
                .AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ISeeder seeder)
        {
            AutoMapperConfig.RegisterMappings(typeof(PageResultDTO<>).GetTypeInfo().Assembly);

            //seed application roles

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<TourOfHeroesDbContext>())
                {
                    context.Database.EnsureCreated();

                    if (!context.Roles.Any())
                    {
                        context.Roles.Add(new IdentityRole
                        {
                            Name = GlobalConstants.AdminRole,
                            NormalizedName = GlobalConstants.AdminRole.ToUpper()
                        });

                        context.Roles.Add(new IdentityRole
                        {
                            Name = GlobalConstants.EditorRole,
                            NormalizedName = GlobalConstants.EditorRole.ToUpper()
                        });

                        context.Roles.Add(new IdentityRole
                        {
                            Name = GlobalConstants.UserRole,
                            NormalizedName = GlobalConstants.UserRole.ToUpper()
                        });

                        context.SaveChanges();
                    }
                }
            }

            seeder.SeedDatabase();

            app.UseDeveloperExceptionPage();

            app.UseCors(options =>
                options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(Configuration["ApplicationSettings:Client_URL"]));

            app.UseSignalR(routes =>
            {
                routes.MapHub<CommentHub>("/api/comments");
                routes.MapHub<CommentHub>("/api/blog");
                routes.MapHub<ProfileImageHub>("/api/profile");
            });

            app.ConfigureCustomExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}