using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Treatments.Contracts.Dto;

namespace BeautySalon.Services.Treatments.Contracts;
public interface ITreatmentService : IService
{
    Task<long> Add(AddTreatmentDto dto);

    Task<IPageResult<GetAllTreatmentsDto>> GetAll(IPagination? pagination=null);
}
