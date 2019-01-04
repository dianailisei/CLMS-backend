using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trivia.Business.Question
{
    public interface IQuestionService
    {
        Task<List<QuestionDetailsModel>> GetAll();

        Task<QuestionDetailsModel> FindById(Guid id);

        Task<Guid> CreateNew(QuestionCreateModel newQuestion);

        Task<Guid> Update(Guid id, QuestionCreateModel updatedQuestion);

        Task Delete(Guid id);
    }
}
