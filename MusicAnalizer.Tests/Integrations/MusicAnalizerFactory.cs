using System;
using backend.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MusicAnalizer.Tests.Integrations;

public class MusicAnalizerFactory : WebApplicationFactory<Program>
{
    private const string TestConnectionString = "Host=localhost;Port=5432;Database=musicdb_test;Username=postgres;Password=postgres";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IDbContextOptionsConfiguration<AppDbContext>>();
            services.RemoveAll<DbContextOptions<AppDbContext>>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(TestConnectionString);
            });
        });
    }
}
