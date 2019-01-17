using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trivia.Business.Answer;

namespace Trivia.Business.Question
{
    public interface IQuestionService
    {
        Task<List<QuestionDetailsModel>> GetAll();

        Task<QuestionDetailsModel> FindById(Guid id);

        Task<QuestionDetailsModel> FindByCourseId(Guid id);

        Task<Guid> CreateNew(QuestionCreateModel newQuestion);

        Task<Guid> CreateFromExisting(Guid questionId);

        Task<Guid> Update(Guid id, QuestionCreateModel updatedQuestion);

        //Task<bool> AddAnswer(Guid questionId, AnswerCreateModel answer);

        Task Delete(Guid id);
    }
}
