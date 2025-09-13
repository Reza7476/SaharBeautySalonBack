using BeautySalon.Application.WhyUsSections.Contracts;
using BeautySalon.Application.WhyUsSections.Contracts.Dto;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;
using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Common.Interfaces;
using BeautySalon.Common.Dtos;
using BeautySalon.Services.WhyUsSections.Exceptions;

namespace BeautySalon.Application.WhyUsSections;
public class WhyUsCommandHandler : IWhyUsSectionHandler
{
    private readonly IImageService _mediaService;
    private readonly IWhyUsSectionService _service;

    public WhyUsCommandHandler(
        IImageService mediaService,
        IWhyUsSectionService service)
    {
        _mediaService = mediaService;
        _service = service;
    }

    public async Task<long> Add(AddWhyUsSectionHandlerDto dto)
    {
        var media = await _mediaService.SaveMedia(new AddMediaDto()
        {
            Media = dto.Image
        });

        var whyUsSectionId = await _service.Add(new AddWhyUsSectionDto()
        {
            Title = dto.Title,
            Media = new MediaDto()
            {
                Extension = media.Extension,
                URL = media.URL,
                ImageName = media.ImageName,
                UniqueName = media.UniqueName
            },
            Description= dto.Description,
        });

        return whyUsSectionId;
    }

    public async Task UpdateWhyUsSection(long id, UpdateWhyUsSectionHandlerDto dto)
    {
        var section = await _service.GetById(id);

        if (section == null)
        {
            throw new WhyUsSectionNotFoundException();
        }

        try
        {
            await _mediaService.DeleteMediaByURL(section.Image.URL);
            MediaDto media = await _mediaService.SaveMedia(new AddMediaDto()
            {
                Media = dto.Image
            });
            await _service.UpdateWhyUsSection(id, new UpdateWhyUsSectionDto()
            {
                Media = new MediaDto()
                {
                    Extension = media.Extension,
                    ImageName = media.ImageName,
                    UniqueName = media.UniqueName,
                    URL = media.URL
                },
                Title=dto.Title
            });

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
