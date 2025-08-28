using BeautySalon.Services.ContactUs.Contracts;
using BeautySalon.Services.ContactUs.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.RestApi.Controllers.ContactUs;
[Route("api/contact-us")]
[ApiController]
public class ContactUsController : ControllerBase
{
    private readonly IAboutUsService _service;

    public ContactUsController(IAboutUsService service)
    {
        _service = service;
    }

    [HttpPost("add")]
    public async Task<long> Add([FromBody]AddAboutUsDto dto)
    {
        return await _service.Add(dto);
    }

    [HttpGet]
    public async Task<GetAboutUsDto?> Get()
    {
        return await _service.Get();
    }

    [HttpPut("{id}")]
    public async Task Update([FromRoute] long id, [FromBody] UpdateAboutUsDto dto )
    {
        await _service.Update(id ,dto);
    }

}
