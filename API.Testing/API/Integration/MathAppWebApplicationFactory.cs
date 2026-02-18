using DTO.DTOs;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.Testing.API.Integration
{
    internal class MathAppWebApplicationFactory: WebApplicationFactory<Program>
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<DataBase>));
                var connectionString = GetConnectionString();
                services.AddSqlServer<DataBase>(connectionString);

                var dbContext = CreateDbContext(services);
               
                dbContext.Database.EnsureDeleted();

                
            });

        }
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = base.CreateHost(builder);

            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataBase>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                if (!dbContext.educationLevels.Any())
                {
                    dbContext.educationLevels.Add(new EducationLevel
                    {
                        name = "Test Education Level"
                    });
                    dbContext.SaveChanges();

                }
            }

            return host;
        }
        private static string? GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<MathAppWebApplicationFactory>()
                .Build();
            var connectionString = configuration.GetConnectionString("MathDataBase");
            return connectionString;
        }
        private static DataBase CreateDbContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataBase>();
            return dbContext;
        }
    }
}
