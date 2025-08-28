using BeautySalon.Services.ContactUs.Contracts;
using BeautySalon.Test.Tool.Entities.ContactUs;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.IntegrationTest.ContactUs;
public class AboutUsServiceTest : BusinessIntegrationTest
{
    private readonly IAboutUsService _sut;
    public AboutUsServiceTest()
    {
        _sut = AboutUsServiceFactory.Generate(SetupContext);
    }


    [Fact]
    public async Task Get_should_return_about_us_properly()
    {
        var aboutUs = new AboutUsBuilder()
            .WithTelephone("telephone")
            .WithLatitude(0.12)
            .WithLongitude(.012)
            .WithDescription("description")
            .WithAddress("address")
            .WithMobileNumber("mobile")
            .Build();
        Save(aboutUs);

        var expected = await _sut.Get();

        expected.Id.Should().Be(aboutUs.Id);
        expected.Address.Should().Be(aboutUs.Address);
        expected.Latitude.Should().Be(aboutUs.Latitude);
        expected.Telephone.Should().Be(aboutUs.Telephone);
        expected.Longitude.Should().Be(aboutUs.Longitude);
        expected.Description.Should().Be(aboutUs.Description);
        expected.MobileNumber.Should().Be(aboutUs.MobileNumber);
    }
}
