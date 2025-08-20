using BeautySalon.Common.Interfaces;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;

namespace BeautySalon.Services.WhyUsSections.Contracts;
public interface IWhyUsSectionService : IService
{
    Task<long> Add(AddWhyUsSectionDto dto);
    Task AddQuestions(AddWhyUsQuestionDto dto, long sectionId);
}
