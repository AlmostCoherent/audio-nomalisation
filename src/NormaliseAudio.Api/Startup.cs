using Files;
using FFmpeg;
using FFMpeg;

namespace NormaliseAudio.Api
{
    /// <summary>
    /// The Web API startup class
    /// </summary>
    public class Startup
    {
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
                builder.AddConsole();
            });
            services.AddControllers();
            services.AddNormaliseAudioFileServices();
            services.AddFFMpegServices();
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app">An <see cref="IApplicationBuilder"/> instance</param>
        /// <param name="env">An <see cref="IWebHostEnvironment"/> instance</param>
        /// <param name="logger">An <see cref="ILogger{TCategoryName}"/>instance</param>
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpLogging();
            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audio API");
                })
            .UseEndpoints(b => b.MapControllers());
            app.Run();
        }
    }
}
