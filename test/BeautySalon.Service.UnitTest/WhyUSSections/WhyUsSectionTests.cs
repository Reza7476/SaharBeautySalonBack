using BeautySalon.Entities.WhyUsSections;
using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Services.WhyUsSections.Exceptions;
using BeautySalon.Test.Tool.Entities.WhyUsSections;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.UnitTest.WhyUSSections;
public class WhyUsSectionTests : BusinessUnitTest
{
    private readonly IWhyUsSectionService _sut;

    public WhyUsSectionTests()
    {
        _sut = WhyUsSectionServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task Add_should_add_why_us_section_properly()
    {
        var dto = new AddWhyUsSectionDtoBuilder()
            .WithTitle("title")
            .WithMedia()
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<Why_Us_Section>().First();
        expected.Title.Should().Be(dto.Title);
        expected.Image.ImageName.Should().Be(dto.Media.ImageName);
        expected.Image.UniqueName.Should().Be(dto.Media.UniqueName);
        expected.Image.Extension.Should().Be(dto.Media.Extension);
        expected.Image.URL.Should().Be(dto.Media.URL);
    }

    [Fact]
    public async Task AddQuestions_should_add_why_us_question_properly()
    {
        var section = new WhyUsSectionBuilder()
            .Build();
        Save(section);
        var dto = new AddWhyUsQuestionDtoBuilder()
            .WithQuestions("question", "answer")
            .Build();

        await _sut.AddQuestions(dto, section.Id);

        var expected = ReadContext.Set<Why_Us_Question>().First();
        expected.Question.Should().Be(dto.Questions.First().Question);
        expected.Answer.Should().Be(dto.Questions.First().Answer);
    }

    [Theory]
    [InlineData(-1)]
    public async Task AddQuestions_should_throw_exception_when_why_us_section_not_found(long sectionId)
    {
        var dto = new AddWhyUsQuestionDtoBuilder()
            .Build();

        Func<Task> expected = async () => await _sut.AddQuestions(dto, sectionId);

        await expected.Should().ThrowExactlyAsync<WhyUsSectionNotFoundException>();
    }
}
