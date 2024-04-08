using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MIT.Events.Function.Services;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        if(hostContext.HostingEnvironment.IsDevelopment())
        {
            Console.WriteLine("STARTING IN DEVELOPMENT MODE");
            config.AddUserSecrets<Program>();
        }
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddEnvironmentVariables();
    })
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        Console.WriteLine("Startup.ConfigureServices() called");
        services.AddScoped<IEventTableService, EventTableService>();
    })
    .Build();

Console.WriteLine("Startup.ConfigureServices() completed");

host.Run();
