using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace api_gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

            .ConfigureServices((context, services) =>
            {
                services.Configure<KestrelServerOptions>(
                    context.Configuration.GetSection("Kestrel"));
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.Limits.MaxConcurrentConnections = 100;
                    serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                    serverOptions.Limits.MaxRequestBodySize = 10 * 1024;
                    serverOptions.Limits.MinRequestBodyDataRate =
                        new MinDataRate(bytesPerSecond: 100,
                            gracePeriod: TimeSpan.FromSeconds(10));
                    serverOptions.Limits.MinResponseDataRate =
                        new MinDataRate(bytesPerSecond: 100,
                            gracePeriod: TimeSpan.FromSeconds(10));
                    serverOptions.Limits.KeepAliveTimeout =
                        TimeSpan.FromDays(30);
                    serverOptions.Limits.RequestHeadersTimeout =
                        TimeSpan.FromDays(30);
                })
                .UseStartup<Startup>()
                .UseUrls("http://*:8123");
            });
    }
}