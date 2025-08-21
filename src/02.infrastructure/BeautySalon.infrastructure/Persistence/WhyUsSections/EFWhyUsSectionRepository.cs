using BeautySalon.Common.Dtos;
using BeautySalon.Entities.WhyUsSections;
using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.WhyUsSections;
public class EFWhyUsSectionRepository : IWhyUsSectionRepository
{

    private readonly DbSet<Why_Us_Section> _sections;
    private readonly DbSet<Why_Us_Question> _questions;

    public EFWhyUsSectionRepository(EFDataContext context)
    {
        _sections = context.Set<Why_Us_Section>();
        _questions = context.Set<Why_Us_Question>();
    }

    public async Task Add(Why_Us_Section whyUsSection)
    {
        await _sections.AddAsync(whyUsSection);
    }

    public async Task<Why_Us_Section?> FindById(long sectionId)
    {
        return await _sections.Where(_ => _.Id == sectionId).FirstOrDefaultAsync();
    }

    public async Task <Why_Us_Question?> FindWhyUsQuestionById(long questionId)
    {
        return await _questions.FirstOrDefaultAsync(_ => _.Id == questionId);
    }

    public async Task<List<GetAllWhyUsSectionDto>> GetAllWhyUsSection()
    {
        return await _sections.Select(_ => new GetAllWhyUsSectionDto()
        {
            Id = _.Id,
            Title = _.Title,
            Image = new MediaDto()
            {
                Extension = _.Image.Extension,
                ImageName = _.Image.ImageName,
                UniqueName = _.Image.UniqueName,
                URL = _.Image.URL
            }
        }).ToListAsync();
    }

    public async Task<List<GetWhyUsQuestionsDto>> GetQuestionsBySectionId(long sectionId)
    {
        return await _questions
            .Where(_ => _.SectionId == sectionId)
            .Select(_ => new GetWhyUsQuestionsDto()
            {
                Answer = _.Answer,
                Question = _.Question,
                Id = _.Id
            }).ToListAsync();
    }
}
