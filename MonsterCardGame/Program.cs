using SWE.Models;
using Newtonsoft.Json;
using System.Net.Security;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Runtime.InteropServices.Marshalling;
using System.Reflection.Metadata;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // Configure services here
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(hostContext.Configuration.GetConnectionString("DefaultConnection")));
            });
}

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
        //logging
        services.AddLogging(builder => builder.AddConsole());
    }
}

//TcpServer tcpServer = new TcpServer();
 //       tcpServer.Start("localhost", 10001);

