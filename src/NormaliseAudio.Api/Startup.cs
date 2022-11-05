using Files;
using FFmpeg;
using FFMpeg;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace NormaliseAudio.Api
{
    /// <summary>
    /// The Web API startup class
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IConfiguration configuration;
        private readonly ILogger<Startup> logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log">A <see cref="ILogger{Startup}"/> instance.</param>
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Configures Services to process audio files and interact with file system.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => {
                builder.ClearProviders();
                builder.AddConsole();
                builder.AddApplicationInsights();
                builder.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>("", LogLevel.Trace);
            });

            this.logger.LogInformation("Configure services.");
            services.AddControllers();
            services.AddNormaliseAudioFileServices();
            services.AddFFMpegServices();

            this.logger.LogInformation("Configure app insights.");
            services.AddApplicationInsightsTelemetry(this.configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            });

        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app">An <see cref="IApplicationBuilder"/> instance</param>
        /// <param name="env">An <see cref="IWebHostEnvironment"/> instance</param>
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Logger.LogInformation("Configure app.");
            app.UseHttpLogging();
            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "Audio API");
                })
            .UseEndpoints(b => b.MapControllers());
            app.Run();
        }
    }
}
