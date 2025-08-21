using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.WhyUsSections.Contracts.Dto;
public class GetAllWhyUsSectionDto
{
    public long Id { get; set; }
    public MediaDto Image { get; set; } = default!;
    public required string Title { get; set; }
}
