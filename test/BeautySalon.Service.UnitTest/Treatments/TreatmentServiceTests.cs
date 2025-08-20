using BeautySalon.Entities.Treatments;
using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Test.Tool.Entities.Treatments;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BeautySalon.Service.UnitTest.Treatments;
public class TreatmentServiceTests:BusinessIntegrationTest
{

    private readonly ITreatmentService _sut;
    public TreatmentServiceTests()
    {
        _sut = TreatmentServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task Add_should_add_treatment_properly()
    {
        var dto=new AddTreatmentDtoBuilder ()
            .WithURL("url")
            .WithTitle("title")
            .WithDescription("description")
            .WithImageName("imageName")
            .WithImageUniqueName("unique")
            .Build ();

        await _sut.Add(dto);

        var expected = ReadContext.Set<Treatment>().Include(_ => _.Images).FirstOrDefault();
        var expectedImage = expected!.Images.FirstOrDefault();
        expected!.Title.Should().Be(dto.Title);
        expected.Description.Should().Be(dto.Description);
        expectedImage!.ImageName.Should().Be(dto.ImageName);
        expectedImage.ImageUniqueName.Should().Be(dto.ImageUniqueName);
        expectedImage.URL.Should().Be(dto.URL);
    }
}
