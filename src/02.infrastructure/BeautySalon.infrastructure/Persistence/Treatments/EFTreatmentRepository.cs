using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Treatments;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Services.Treatments.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Treatments;
public class EFTreatmentRepository : ITreatmentRepository
{
    private readonly DbSet<Treatment> _treatments;

    public EFTreatmentRepository(EFDataContext context)
    {
        _treatments = context.Set<Treatment>();
    }

    public async Task Add(Treatment treatment)
    {
        await _treatments.AddAsync(treatment);
    }

    public async Task<IPageResult<GetAllTreatmentsDto>> GetAll(IPagination? pagination)
    {
        var query = _treatments
            .Include(_ => _.Images)
            .Select(_ => new GetAllTreatmentsDto()
            {
                Description = _.Description,
                Title = _.Title,
                Id = _.Id,
                Media = _.Images.Select(media => new MediaDto()
                {
                    Extension = media.Extension,
                    URL = media.URL,
                    ImageName = media.ImageName,
                    UniqueName = media.ImageUniqueName
                }).First()
            }).AsQueryable();

        return await query.Paginate(pagination ?? new Pagination());
    }

    public async Task<GetTreatmentDetailsDto?> GetDetails(long id)
    {
        return await _treatments
            .Where(_ => _.Id == id)
            .Include(_ => _.Images)
            .Select(_ => new GetTreatmentDetailsDto()
            {
                Description = _.Description,
                Title = _.Title,
                Media = _.Images.Select(media => new MediaDto()
                {
                    Extension = media.Extension,
                    URL = media.URL,
                    UniqueName = media.ImageUniqueName,
                    ImageName = media.ImageName,
                }).ToList()
            }).FirstOrDefaultAsync();
    }
}
