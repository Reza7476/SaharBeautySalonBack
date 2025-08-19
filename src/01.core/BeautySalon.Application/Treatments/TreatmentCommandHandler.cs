using BeautySalon.Application.Treatments.Contracts;
using BeautySalon.Application.Treatments.Contracts.Dto;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Services.Treatments.Contracts.Dto;
using System.Net.Http.Headers;

namespace BeautySalon.Application.Treatments;
public class TreatmentCommandHandler : TreatmentHandler
{
    private readonly ITreatmentService _service;
    private readonly IImageService _mediaService;

    public TreatmentCommandHandler(
        ITreatmentService service,
        IImageService mediaService)
    {
        _service = service;
        _mediaService = mediaService;
    }

    public async Task<long> Add(AddTreatmentHandlerDto dto)
    {
        var media = await _mediaService.SaveMedia(new AddMediaDto()
        {
            Media = dto.Media,
        });

        var treatmentId = await _service.Add(new AddTreatmentDto()
        {
            Description=dto.Description,
            Title=dto.Title,
            ImageName=media.ImageName,
            ImageUniqueName=media.UniqueName,
            URL =media.FilePath,
            Extension=media.Extension
        });

        return treatmentId;
    }
}
