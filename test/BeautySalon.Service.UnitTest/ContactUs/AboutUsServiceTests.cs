using BeautySalon.Entities.ContactUs;
using BeautySalon.Services.ContactUs.Contracts;
using BeautySalon.Services.ContactUs.Exceptions;
using BeautySalon.Test.Tool.Entities.ContactUs;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.UnitTest.ContactUs;
public class AboutUsServiceTests : BusinessUnitTest
{
    private readonly IAboutUsService _sut;

    public AboutUsServiceTests()
    {
        _sut = AboutUsServiceFactory.Generate(SetupContext);
    }

    [Fact]
    public async Task Add_should_add_about_us_correctly()
    {
        var dto = new AddAboutUsDtoBuilder()
            .WithAddress("address")
            .WithLatitude(0.12)
            .WithLongitude(0.25)
            .WithTelephone("telephone")
            .WithMobileNumber("mobileNumber")
            .WithDescription("description")
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<AboutUs>().First();
        expected.MobileNumber.Should().Be(dto.MobileNumber);
        expected.Telephone.Should().Be(dto.Telephone);
        expected.Latitude.Should().Be(dto.Latitude);
        expected.Longitude.Should().Be(dto.Longitude);
        expected.Description.Should().Be(dto.Description);
        expected.Address.Should().Be(dto.Address);
    }

    [Fact]
    public async Task Update_should_update_about_us_properly()
    {
        var aboutUs = new AboutUsBuilder()
            .WithTelephone("telephone")
            .WithMobileNumber("mobileNumber")
            .WithDescription("description")
            .WithAddress("address")
            .WithLatitude(.01)
            .WithLongitude(0.2)
            .Build();
        Save(aboutUs);
        var dto = new UpdateAboutUsDtoBuilder()
            .WithAddress("address")
            .WithLatitude(.01)
            .WithLongitude(.011)
            .WithDescription("description")
            .WithMobileNumber("mobile")
            .WithTelephone("tel")
            .Build();

        await _sut.Update(aboutUs.Id, dto);

        var expected = ReadContext.Set<AboutUs>().First();
        expected.MobileNumber.Should().Be(dto.MobileNumber);
        expected.Telephone.Should().Be(dto.Telephone);
        expected.Address.Should().Be(dto.Address);
        expected.Longitude.Should().Be(dto.Longitude);
        expected.Latitude.Should().Be(dto.Latitude);
        expected.Description.Should().Be(dto.Description);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Update_should_throw_exception_when_about_us_not_found(long id)
    {
        var dto = new UpdateAboutUsDtoBuilder()
            .Build();
        Func<Task> expected = async () => await _sut.Update(id, dto);

        await expected.Should().ThrowExactlyAsync<AboutUsNotFoundException>();
    }
}
