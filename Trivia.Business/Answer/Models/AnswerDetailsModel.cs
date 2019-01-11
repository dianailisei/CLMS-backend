using System;

namespace Trivia.Business.Answer
{
    public class AnswerDetailsModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid QuestionId { get; set; }
        public DateTime AnswerTime { get; set; }
        public string Text { get; set; }
    }
}
