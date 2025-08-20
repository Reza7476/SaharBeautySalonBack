using BeautySalon.Entities.WhyUsSections;
using BeautySalon.Services.WhyUsSections.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.WhyUsSections;
public class EFWhyUsSectionRepository : IWhyUsSectionRepository
{

    private readonly DbSet<Why_Us_Section> _sections;

    public EFWhyUsSectionRepository(EFDataContext context)
    {
        _sections = context.Set<Why_Us_Section>();
    }

    public async Task Add(Why_Us_Section whyUsSection)
    {
        await _sections.AddAsync(whyUsSection);
    }

    public async Task<Why_Us_Section?> FindById(long sectionId)
    {
        return await _sections.Where(_ => _.Id == sectionId).FirstOrDefaultAsync();
    }
}
