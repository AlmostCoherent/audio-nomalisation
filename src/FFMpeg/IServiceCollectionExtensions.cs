using FFMpeg.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FFMpeg
{
    public static class IServiceCollectionExtensions
    {
		public static void AddFFMpegServices(this IServiceCollection services)
		{
			services.AddScoped<IResultBuilder, ResultBuilder>();
			services.AddScoped<IStartInfoProvider, StartInfoProvider>();
			services.AddScoped<IExecuteProcess, ExecuteProcess>();
		}
	}
}
