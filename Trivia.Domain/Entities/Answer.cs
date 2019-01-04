using System;

namespace Trivia.Domain.Entities
{
    public class Answer : Entity
    {
        public Guid StudentId { get; set; }
        public Guid QuestionId { get; set; }
        public string Text { get; set; }

        public static Answer Create(Guid studentId, Guid questionId, string text) => new Answer()
        {
            Id = new Guid(),
            StudentId = studentId,
            QuestionId = questionId,
            Text = text
        };

        public void Update(Guid studentId, Guid questionId, string text)
        {
            StudentId = studentId;
            QuestionId = questionId;
            Text = text;
        }
    }
}
