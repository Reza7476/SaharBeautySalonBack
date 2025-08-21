using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Test.Tool.Entities.WhyUsSections;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.IntegrationTest.WhyUsSections;
public class WhyUsSectionServiceTests : BusinessIntegrationTest
{
    private readonly IWhyUsSectionService _sut;

    public WhyUsSectionServiceTests()
    {
        _sut = WhyUsSectionServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task GetQuestionsBySectionId_should_return_section_question_properly()
    {
        var section = new WhyUsSectionBuilder()
            .Build();
        Save(section);
        var question = new WhyUsQuestionBuilder()
            .WithSectionId(section.Id)
            .WithAuestion("question")
            .WithAnswer("answer")
            .Build();
        Save(question);

        var expected = await _sut.GetQuestionsBySectionId(section.Id);

        expected.First().Id.Should().Be(question.Id);
        expected.First().Answer.Should().Be(question.Answer);
    }

    [Fact]
    public async Task GetAllWhyUsSection_should_return_all_why_us_sections_properly()
    {
        var section = new WhyUsSectionBuilder()
            .WithTitle("title")
            .WithMedia()
            .Build();
        Save(section);

        var expected = await _sut.GetAllWhyUsSection();

        expected.First().Title.Should().Be(section.Title);
        expected.First().Image.Extension.Should().Be(section.Image.Extension);
        expected.First().Image.ImageName.Should().Be(section.Image.ImageName);
        expected.First().Image.UniqueName.Should().Be(section.Image.UniqueName);
        expected.First().Image.URL.Should().Be(section.Image.URL);
    }
}
