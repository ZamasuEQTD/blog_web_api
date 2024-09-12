using Application.Medias.Abstractions;
using Domain.Media;
using Domain.Media.Abstractions;
using Domain.Media.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Medias.Services
{
    public class FileContextStrategy(IServiceProvider serviceProvider) : IFileContextStrategy
    {
        public async Task<FileMedia> Execute(FileType type, FileProcesorParams @params)
        {
            var strategy = serviceProvider.GetKeyedService<IFileProcesorsStrategy>(type);

            if (strategy is null) throw new ArgumentException("Strategy no conseguida");

            return await strategy!.Procesar(@params);
        }
    }
}