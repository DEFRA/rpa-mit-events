using System;
using System.IO;
using EST.MIT.Events.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        // var serviceProvider = services.BuildServiceProvider();
        // var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        services.AddSingleton<IEventTableService, EventTableService>();

    })
    .Build();

Console.WriteLine("Startup.ConfigureServices() completed");

host.Run();
