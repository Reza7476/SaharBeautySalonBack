using BeautySalon.Entities.Commons;
using BeautySalon.Entities.ContactUs;
using BeautySalon.Services.ContactUs.Contracts;
using BeautySalon.Services.ContactUs.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.ContactUs;
public class EFAboutUsRepository : IAboutUsRepository
{
    private readonly DbSet<AboutUs> _aboutUs;

    public EFAboutUsRepository(EFDataContext context)
    {
        _aboutUs = context.Set<AboutUs>();
    }

    public async Task Add(AboutUs contactUs)
    {
        await _aboutUs.AddAsync(contactUs);
    }

    public async Task<AboutUs?> FindById(long id)
    {
        return await _aboutUs.Where(_ => _.Id == id).FirstOrDefaultAsync();
    }

    public async Task<GetAboutUsDto?> Get()
    {
        return await _aboutUs.Select(_ => new GetAboutUsDto()
        {
            MobileNumber = _.MobileNumber,
            Description = _.Description,
            Address = _.Address,
            Id = _.Id,
            Latitude = _.Latitude,
            Longitude = _.Longitude,
            Telephone = _.Telephone,
            Email = _.Email,
            Instagram = _.Instagram,
            LogoImage = _.LogoImage != null ? new MediaDocument()
            {
                Extension = _.LogoImage.Extension,
                ImageName = _.LogoImage.ImageName,
                UniqueName = _.LogoImage.UniqueName,
                URL = _.LogoImage.URL,
            } : null,
        }).FirstOrDefaultAsync();
    }
}
