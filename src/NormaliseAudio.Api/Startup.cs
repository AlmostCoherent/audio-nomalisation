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
        private readonly ILogger<Startup> log;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log">A <see cref="ILogger{Startup}"/> instance.</param>
        public Startup(ILogger<Startup> log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Configures Services to process audio files and interact with file system.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            _ = app
                .UseRouting()
#if DEBUG && NET6_0_OR_GREATER
                .UseHttpLogging()
#endif
                .UseRequestLocalization()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Optimis Test API");
                })
                .UseEndpoints(b => b.MapControllers())
            ;
        }
    }
}
