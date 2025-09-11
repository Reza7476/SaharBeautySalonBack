using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Treatments;
using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Services.Treatments.Contracts.Dto;
using BeautySalon.Services.Treatments.Exceptions;

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

    public async Task<long> AddImageReturnImageId(long id, AddImageDetailsDto dto)
    {
        await StopIfTreatmentNotFound(id);

        var treatmentImage = new TreatmentImage()
        {
            CreateDate = DateTime.UtcNow,
            Extension = dto.Extension,
            ImageName = dto.ImageName,
            ImageUniqueName = dto.UniqueName,
            URL = dto.URL,
            TreatmentId = id
        };
        await _repository.AddImage(treatmentImage);
        await _unitOfWork.Complete();
        return treatmentImage.Id;
    }

    private async Task StopIfTreatmentNotFound(long id)
    {
        if (!await _repository.ExistById(id))
        {
            throw new TreatmentNotFoundException();
        }
    }

    public async Task<IPageResult<GetAllTreatmentsDto>> GetAll(IPagination? pagination)
    {
        return await _repository.GetAll(pagination);
    }

    public async Task<GetTreatmentDetailsDto?> GetDetails(long id)
    {
        return await _repository.GetDetails(id);
    }
}
