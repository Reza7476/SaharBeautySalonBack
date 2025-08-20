using Microsoft.AspNetCore.Http;

namespace BeautySalon.Application.WhyUsSections.Contracts.Dto;
public class AddWhyUsSectionHandlerDto
{
    public required string Title { get; set; }
    public required IFormFile Image { get; set; }
}
