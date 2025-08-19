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
        var query= _treatments
            .Include(_ => _.Images)
            .Select(_ => new GetAllTreatmentsDto()
            {
                Description = _.Description,
                Title = _.Title,
                Id=_.Id,
                Media = _.Images.Select(media => new MediaDto()
                {
                    Extension = media.Extension,
                    FilePath = media.URL,
                    ImageName = media.ImageName,
                    UniqueName = media.ImageUniqueName
                }).First()
            }).AsQueryable();

        return await query.Paginate(pagination ?? new Pagination());
    }
}
