using Files.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Files
{
    public static class IServiceCollectionExtensions
    {
        public static void AddNormaliseAudioFileServices(this IServiceCollection services)
        {
            services.AddSingleton<IBaseFileConfig, BaseFileConfig>();
            services.AddSingleton<IFileCreator, FileCreator>();
            services.AddSingleton<IFileRemover, FileRemover>();
        }
    }
}
