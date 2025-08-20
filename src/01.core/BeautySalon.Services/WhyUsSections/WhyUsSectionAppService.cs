using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Commons;
using BeautySalon.Entities.WhyUsSections;
using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;
using BeautySalon.Services.WhyUsSections.Exceptions;

namespace BeautySalon.Services.WhyUsSections;
public class WhyUsSectionAppService : IWhyUsSectionService
{
    private readonly IWhyUsSectionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WhyUsSectionAppService(
        IWhyUsSectionRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Add(AddWhyUsSectionDto dto)
    {
        var whyUsSection = new Why_Us_Section()
        {
            CreateDate = DateTime.UtcNow,
            Title = dto.Title,
            Image = new MediaDocument()
            {
                Extension = dto.Media.Extension,
                URL = dto.Media.URL,
                ImageName = dto.Media.ImageName,
                UniqueName = dto.Media.UniqueName
            }
        };

        await _repository.Add(whyUsSection);
        await _unitOfWork.Complete();

        return whyUsSection.Id;
    }

    public async Task AddQuestions(AddWhyUsQuestionDto dto, long sectionId)
    {
        var section = await _repository.FindById(sectionId);
        StopIfWhyUsSectionNotFound(section);

        foreach (var question in dto.Questions)
        {
            section!.Why_Us_Questions.Add(new Why_Us_Question()
            {
                Answer = question.Answer,
                Question = question.Question,
                CreateDate = DateTime.UtcNow
            });
        }

        await _unitOfWork.Complete();

    }

    private static void StopIfWhyUsSectionNotFound(Why_Us_Section? section)
    {
        if (section == null)
        {
            throw new WhyUsSectionNotFoundException();
        }
    }
}
