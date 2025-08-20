using BeautySalon.Common.Dtos;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.WhyUsSections;
public class AddWhyUsQuestionDtoBuilder
{
    private readonly AddWhyUsQuestionDto _dto;

    public AddWhyUsQuestionDtoBuilder()
    {
        _dto = new AddWhyUsQuestionDto()
        {
            Questions = new List<QuestionAndAnswerDto>(),
        };
    }

    public AddWhyUsQuestionDtoBuilder WithQuestions(string question,string answer)
    {
        _dto.Questions.Add(new QuestionAndAnswerDto()
        {
            Answer = answer,
            Question = question
        });
        return this;
    }

    public AddWhyUsQuestionDto Build()
    {
        return _dto;
    }
}

