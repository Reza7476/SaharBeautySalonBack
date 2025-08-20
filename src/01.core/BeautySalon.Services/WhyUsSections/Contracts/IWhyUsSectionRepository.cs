using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.WhyUsSections;

namespace BeautySalon.Services.WhyUsSections.Contracts;
public interface IWhyUsSectionRepository : IRepository
{
    Task Add(Why_Us_Section whyUsSection);
    Task<Why_Us_Section?> FindById(long sectionId);
}
