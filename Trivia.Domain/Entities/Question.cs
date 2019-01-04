using System;
using System.Collections.Generic;

namespace Trivia.Domain.Entities
{
    public class Question : Entity
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime AddTime { get; set; }
        public short Duration { get; set; }
        public short Points { get; set; }
        public string Text { get; set; }
        public ICollection<Answer> Answers { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }

        public static Question Create(Guid teacherId, Guid courseId, short duration, short points, string text) => new Question()
        {
            Id = new Guid(),
            TeacherId = teacherId,
            CourseId = courseId,
            AddTime = DateTime.Today,
            Duration = duration,
            Points = points,
            Text = text,
            Answers = new List<Answer>()
        };

        public void Update(Guid teacherId, Guid courseId, short duration, short points, string text)
        {
            TeacherId = teacherId;
            CourseId = courseId;
            Duration = duration;
            Points = points;
            Text = text;
        }

        public void AddAnswer(Answer answer)
        {
            Answers.Add(answer);
        }
    }
}
