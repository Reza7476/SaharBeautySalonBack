using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Treatments;
using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Services.Treatments.Contracts.Dto;

namespace BeautySalon.Services.Treatments;
public class TreatmentAppService : ITreatmentService
{

    private readonly ITreatmentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public TreatmentAppService(
        ITreatmentRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Add(AddTreatmentDto dto)
    {
        var treatment = new Treatment()
        {
            CreateDate = DateTime.UtcNow,
            Description = dto.Description,
            Title = dto.Title,
        };

        treatment.Images.Add(new TreatmentImage()
        {

            CreateDate = DateTime.UtcNow,
            ImageName = dto.ImageName,
            ImageUniqueName = dto.ImageUniqueName,
            URL = dto.URL,
            Extension = dto.Extension
        });

        await _repository.Add(treatment);
        await _unitOfWork.Complete();
        return treatment.Id;

    }

    public async Task<IPageResult<GetAllTreatmentsDto>> GetAll(IPagination? pagination)
    {
        return await _repository.GetAll(pagination);
    }
}
