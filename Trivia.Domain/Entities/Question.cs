using System;
using System.Collections.Generic;

namespace Trivia.Domain.Entities
{
    public class Question : Entity
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public string Text { get; set; }
        public ICollection<Answer> Answers { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }

        public static Question Create(Guid teacherId, Guid courseId, string text) => new Question()
        {
            Id = new Guid(),
            TeacherId = teacherId,
            CourseId = courseId,
            Text = text
        };

        public void Update(Guid teacherId, Guid courseId, string text)
        {
            TeacherId = teacherId;
            CourseId = courseId;
            Text = text;
        }
    }
}
