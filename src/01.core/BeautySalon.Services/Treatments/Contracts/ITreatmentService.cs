using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Treatments.Contracts.Dto;

namespace BeautySalon.Services.Treatments.Contracts;
public interface ITreatmentService : IService
{
    Task<long> Add(AddTreatmentDto dto);
    Task<long> AddImageReturnImageId(long id, AddImageDetailsDto dto);
    Task<IPageResult<GetAllTreatmentsDto>> GetAll(IPagination? pagination=null);
    Task<GetTreatmentDetailsDto?> GetDetails(long id);
}
