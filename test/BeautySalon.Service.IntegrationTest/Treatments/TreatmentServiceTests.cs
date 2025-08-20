using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Test.Tool.Entities.Treatments;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;

namespace BeautySalon.Service.IntegrationTest.Treatments;
public class TreatmentServiceTests:BusinessIntegrationTest
{
    private readonly ITreatmentService _sut;
    
    public TreatmentServiceTests()
    {
        _sut = TreatmentServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task GetAll_should_return_all_treatment_properly()
    {
        var treat1 = new TreatmentBuilder()
            .WithTitle("title1")
            .WithDescription("description1")
            .WithImage()
            .Build();
        Save(treat1);
        var treat2 = new TreatmentBuilder()
            .WithTitle("title2")
            .WithDescription("description2")
            .WithImage()
            .Build();
        Save(treat2);

        var expected = await _sut.GetAll();

        expected.TotalElements.Should().Be(2);
        expected.Elements.First().Title.Should().Be(treat1.Title);
        expected.Elements.First().Description.Should().Be(treat1.Description);
        expected.Elements.First().Media.ImageName.Should().Be(treat1.Images.First().ImageName);
        expected.Elements.First().Media.UniqueName.Should().Be(treat1.Images.First().ImageUniqueName);
        expected.Elements.First().Media.FilePath.Should().Be(treat1.Images.First().URL);
        expected.Elements.First().Media.Extension.Should().Be(treat1.Images.First().Extension);
        expected.Elements.Last().Title.Should().Be(treat2.Title);
        expected.Elements.Last().Description.Should().Be(treat2.Description);
        expected.Elements.Last().Media.ImageName.Should().Be(treat2.Images.First().ImageName);
        expected.Elements.Last().Media.UniqueName.Should().Be(treat2.Images.First().ImageUniqueName);
        expected.Elements.Last().Media.FilePath.Should().Be(treat2.Images.First().URL);
        expected.Elements.Last().Media.Extension.Should().Be(treat2.Images.First().Extension);
    }

    [Fact]
    public async Task GetDetails_should_return_treatment_properly()
    {
        var treat1 = new TreatmentBuilder()
            .WithTitle("title1")
            .WithDescription("description1")
            .WithImage()
            .Build();
        Save(treat1);

        var expected = await _sut.GetDetails(treat1.Id);

        expected!.Title.Should().Be(treat1.Title);
        expected.Description.Should().Be(treat1.Description);
        expected.Media!.UniqueName.Should().Be(treat1.Images.First().ImageUniqueName);
        expected.Media.ImageName.Should().Be(treat1.Images.First().ImageName);
        expected.Media.Extension.Should().Be(treat1.Images.First().Extension);
        expected.Media.FilePath.Should().Be(treat1.Images.First().URL);
    }

}
