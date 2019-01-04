using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trivia.Business.Answer
{
    public interface IAnswerService
    {
        Task<List<AnswerDetailsModel>> GetAll();

        Task<AnswerDetailsModel> FindById(Guid id);

        Task<Guid> CreateNew(AnswerCreateModel newAnswer);

        //Task<Guid> Update(Guid id, AnswerCreateModel updatedAnswer);

        Task Delete(Guid id);
    }
}
