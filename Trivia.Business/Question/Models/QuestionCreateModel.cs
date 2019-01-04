using System;

namespace Trivia.Business.Question
{
    public class QuestionCreateModel
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public string Text { get; set; }
    }
}
