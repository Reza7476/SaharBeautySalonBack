using BeautySalon.Common.Dtos;

namespace BeautySalon.Common.Interfaces;
public interface IImageService : IService
{
    Task<MediaDto> SaveMedia(AddMediaDto dto);
    Task DeleteMediaByURL(string url);
}
