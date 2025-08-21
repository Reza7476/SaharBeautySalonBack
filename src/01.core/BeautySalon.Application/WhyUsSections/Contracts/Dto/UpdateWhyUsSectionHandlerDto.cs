using Microsoft.AspNetCore.Http;

namespace BeautySalon.Application.WhyUsSections.Contracts.Dto;
public class UpdateWhyUsSectionHandlerDto
{
    public required string Title { get; set; }
    public required IFormFile Image { get; set; }
}
