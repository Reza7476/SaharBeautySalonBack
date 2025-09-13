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
            .WithDescription("description")
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<Why_Us_Section>().First();
        expected.Title.Should().Be(dto.Title);
        expected.Description.Should().Be(dto.Description);
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

    [Fact]
    public async Task UpdateQuestion_should_update_question_properly()
    {
        var section = new WhyUsSectionBuilder()
            .Build();
        Save(section);
        var question = new WhyUsQuestionBuilder()
            .WithAuestion("question")
            .WithAnswer("answer")
            .WithSectionId(section.Id)
            .Build();
        Save(question);
        var dto = new UpdateWhyUsQuestionDtoBuilder()
            .WithQuestion("dummy")
            .WithAnswer("dummy")
            .Build();

        await _sut.UpdateQuestion(question.Id, dto);

        var expected = ReadContext.Set<Why_Us_Question>().First();
        expected.Question.Should().Be(dto.Question);
        expected.Answer.Should().Be(dto.Answer);
    }


    [Theory]
    [InlineData(-1)]
    public async Task UpdateQuestion_should_throw_exception_when_why_us_question_not_found(long questionId)
    {
        var dto = new UpdateWhyUsQuestionDtoBuilder()
            .Build();

        Func<Task> expected = async () => await _sut.UpdateQuestion(questionId, dto);

        await expected.Should().ThrowExactlyAsync<WhyUsQuestionNotFoundException>();
    }

    [Fact]
    public async Task UpdateWgyUsSection_should_update_section_properly()
    {
        var section = new WhyUsSectionBuilder()
            .WithTitle("title")
            .WithMedia()
            .Build();
        Save(section);
        var dto = new UpdateWhyUsSectionDtoBuilder()
            .WithTitle("title")
            .WithMedia()
            .Build();

        await _sut.UpdateWhyUsSection(section.Id, dto);

        var expected=ReadContext.Set<Why_Us_Section>().First();
        expected.Title.Should().Be(dto.Title);
        expected.Image.Extension.Should().Be(dto.Media.Extension);
        expected.Image.ImageName.Should().Be(dto.Media.ImageName);
        expected.Image.UniqueName.Should().Be(dto.Media.UniqueName);
        expected.Image.URL.Should().Be(dto.Media.URL);
    }
}
