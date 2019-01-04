using System;
using System.Collections.Generic;

namespace Trivia.Business.Question
{
    public class QuestionDetailsModel
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public string Text { get; set; }
        public ICollection<Domain.Entities.Answer> Answers { get; set; }
    }
}
