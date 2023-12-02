using DataAccess.Context;
using WorkerService;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        services.AddDbContext<DataContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<DataContext>();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();