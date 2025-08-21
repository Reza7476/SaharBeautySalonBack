using BeautySalon.Common.Dtos;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.WhyUsSections;
public class UpdateWhyUsSectionDtoBuilder
{
    private readonly UpdateWhyUsSectionDto _dto;
    public UpdateWhyUsSectionDtoBuilder()
    {
        _dto = new UpdateWhyUsSectionDto()
        {
            Title = "title",
            Media = new MediaDto()
            {
                Extension = "extension",
                ImageName = "imageName",
                UniqueName = "uniqueName",
                URL = "URL"
            }
        };
    }

    public UpdateWhyUsSectionDtoBuilder WithTitle(string title)
    {
        _dto.Title=title;
        return this;    
    }

    public UpdateWhyUsSectionDtoBuilder WithMedia()
    {
        _dto.Media = new MediaDto()
        {
            Extension = "extension",
            ImageName = "imageName",
            UniqueName = "uniqueName",
            URL = "URL"
        };
        return this;
    }


    public UpdateWhyUsSectionDto Build()
    {
        return _dto;
    }

}
