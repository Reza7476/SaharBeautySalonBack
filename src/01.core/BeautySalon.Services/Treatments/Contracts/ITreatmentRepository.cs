using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Treatments;
using BeautySalon.Services.Treatments.Contracts.Dto;

namespace BeautySalon.Services.Treatments.Contracts;
public interface ITreatmentRepository : IRepository
{
    Task Add(Treatment treatment);

    Task<IPageResult<GetAllTreatmentsDto>> GetAll(IPagination? pagination);
}
