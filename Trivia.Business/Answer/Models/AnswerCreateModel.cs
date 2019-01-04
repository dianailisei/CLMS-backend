using System;

namespace Trivia.Business.Answer
{
    public class AnswerCreateModel
    {
        public Guid StudentId { get; set; }
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
    }
}
