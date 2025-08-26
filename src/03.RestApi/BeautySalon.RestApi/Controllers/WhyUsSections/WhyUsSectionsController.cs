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

    [HttpPost("add")]
    public async Task<long> Add([FromForm] AddWhyUsSectionHandlerDto dto)
    {
        return await _handler.Add(dto);
    }

    [HttpPatch("{sectionId}/questions")]
    public async Task AddQuestions(
        [FromRoute] long sectionId,
        [FromBody] AddWhyUsQuestionDto dto)
    {
        await _service.AddQuestions(dto, sectionId);
    }

    [HttpGet("{sectionId}/questions")]
    public async Task<List<GetWhyUsQuestionsDto>>
        GetQuestions([FromRoute] long sectionId)
    {
        return await _service.GetQuestionsBySectionId(sectionId);
    }

    [HttpGet]
    public async Task<List<GetAllWhyUsSectionDto>> GetAllWhyUsSection()
    {
        return await _service.GetAllWhyUsSection();
    }

    [HttpPut("{questionId}/question")]
    public async Task UpdateQuestion(
        [FromRoute] long questionId,
        [FromBody]UpdateWhyUsQuestionDto dto)
    {
        await _service.UpdateQuestion(questionId, dto);
    }


    [HttpPut("{id}")]
    public async Task UpdateWhyUsSection([FromRoute] long id, [FromForm]UpdateWhyUsSectionHandlerDto dto)
    {
        await _handler.UpdateWhyUsSection(id, dto);
    }
}
