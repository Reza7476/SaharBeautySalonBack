using BeautySalon.Application.WhyUsSections.Contracts;
using BeautySalon.Application.WhyUsSections.Contracts.Dto;
using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.WhyUsSections;
[Route("api/why-us-sections")]
[ApiController]
public class WhyUsSectionsController : ControllerBase
{
    private readonly IWhyUsSectionHandler _handler;
    private readonly IWhyUsSectionService _service;


    public WhyUsSectionsController(
        IWhyUsSectionHandler handler,
        IWhyUsSectionService service)
    {
        _handler = handler;
        _service = service;
    }


    [HttpPost]
    public async Task<long> Add([FromForm] AddWhyUsSectionHandlerDto dto)
    {
        return await _handler.Add(dto);
    }

    [HttpPatch("{sectionId}/questions")]
    public async Task AddQuestions([FromQuery]long sectionId, [FromBody]AddWhyUsQuestionDto dto)
    {
        await _service.AddQuestions(dto, sectionId);
    }
}
