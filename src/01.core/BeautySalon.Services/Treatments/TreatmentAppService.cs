﻿using BeautySalon.Common.Dtos;
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

    public async Task<long> AddImageReturnImageId(long id, ImageDetailsDto dto)
    {
        await StopIfTreatmentIsNotExist(id);

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

    private async Task StopIfTreatmentIsNotExist(long id)
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

    public async Task<string> GetUrl_Remove_Image(long imageId, long id)
    {
        await StopIfTreatmentIsNotExist(id);
        var images = await _repository.GetTreatmentImages(id);
        if (images.Count <= 1)
        {
            throw new NotAllowedDeleteImageException();
        }
        var image = await _repository.FindImageByImageId(imageId);
        var url = image!.URL.ToString();
        await _repository.RemoveImage(image!);
        await _unitOfWork.Complete();
        return url;
    }

    public async Task Update(UpdateTreatmentDto dto, long id)
    {
        var treatment = await _repository.FindById(id);
        StopIfTreatmentNotFound(treatment);
        treatment!.Title = dto.Title;
        treatment.Description = dto.Description;
        await _unitOfWork.Complete();

    }

    private static void StopIfTreatmentNotFound(Treatment? treatment)
    {
        if (treatment == null)
        {
            throw new TreatmentNotFoundException();
        }
    }

    public async Task<List<GetTreatmentForLandingDto>> GetForLanding()
    {
        return await _repository.GetForLanding();
    }
}