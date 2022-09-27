using FFMpeg;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace FFMPEGProducer
{

    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddFFMpegServices();
            serviceCollection.AddSingleton(typeof(ILogger), typeof(Logger<ExecuteProcess>));
            var serviceProvider = serviceCollection
                                        .AddLogging(builder => {
                                            builder.ClearProviders();
                                            builder.AddConsole();
                                            builder.AddDebug();
                                        })
                                        .BuildServiceProvider();

            var gmlfs = new LUFSProvider((IExecuteProcess)serviceProvider.GetService(typeof(IExecuteProcess)));
            var inputFile = @"D:\\OneDrive\\Music\\SubCatTracks\\DevilairxSubCatEP\\SubCat-Hyenas[MST].wav";
            //Act
            gmlfs.GetMeLUFS(inputFile);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
               Host.CreateDefaultBuilder(args)
                   .ConfigureServices((hostContext, services) =>
                   {
                       services.AddFFMpegServices();
                   })
                    .ConfigureLogging((logging) => {
                        logging.AddConsole()
                                .AddDebug();
                    });
            }

}