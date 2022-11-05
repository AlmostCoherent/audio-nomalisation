using FFMpeg;
using Files;
using Microsoft.AspNetCore.Http.Features;

namespace NormaliseAudio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
                .SetMinimumLevel(LogLevel.Trace)
                .AddConsole());

            ILogger<Program> programLogger = loggerFactory.CreateLogger<Program>();
            var builder = WebApplication.CreateBuilder(args);


            ILogger<Startup> startupLogger = loggerFactory.CreateLogger<Startup>();
            var startup = new Startup(builder.Configuration, startupLogger);

            // Add services to the container.
            startup.ConfigureServices(builder.Services); 

            var app = builder.Build();
            startup.Configure(app, builder.Environment);
        }
    }
}