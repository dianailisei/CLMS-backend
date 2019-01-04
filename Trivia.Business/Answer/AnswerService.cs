using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trivia.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Trivia.Business.Answer
{
    public class AnswerService : IAnswerService
    {
        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;

        public AnswerService(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            this._readRepository = readRepository;
            this._writeRepository = writeRepository;
        }

        public Task<List<AnswerDetailsModel>> GetAll() => GetAllAnswersDetails().ToListAsync();

        public Task<AnswerDetailsModel> FindById(Guid id) => GetAllAnswersDetails().SingleOrDefaultAsync(c => c.Id == id);

        public async Task<Guid> CreateNew(AnswerCreateModel newAnswer)
        {
            var answer = Domain.Entities.Answer.Create(newAnswer.StudentId, newAnswer.QuestionId, newAnswer.Text);

            await _writeRepository.AddNewAsync(answer);
            await _writeRepository.SaveAsync();

            return answer.Id;
        }

        public async Task<Guid> Update(Guid id, AnswerCreateModel updatedAnswer)
        {
            var exist = await _readRepository.FindByIdAsync<Domain.Entities.Answer>(id);
            if (exist != null)
            {
                exist.Update(updatedAnswer.StudentId, updatedAnswer.QuestionId, updatedAnswer.Text);
                await _writeRepository.UpdateAsync(id, exist);
                await _writeRepository.SaveAsync();
            }
            return exist.Id;
        }

        public async Task Delete(Guid id)
        {
            await _writeRepository.DeleteByIdAsync<Domain.Entities.Answer>(id);
            await _writeRepository.SaveAsync();
        }

        private IQueryable<AnswerDetailsModel> GetAllAnswersDetails() => _readRepository.GetAll<Domain.Entities.Answer>()
            .Select(a => new AnswerDetailsModel()
            {
                Id = a.Id,
                StudentId = a.StudentId,
                QuestionId = a.QuestionId,
                Text = a.Text,
            });
    }
}
