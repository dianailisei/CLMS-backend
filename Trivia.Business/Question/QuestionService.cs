using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trivia.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Trivia.Business.Question
{
    public class QuestionService : IQuestionService
    {
        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;

        public QuestionService(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            this._readRepository = readRepository;
            this._writeRepository = writeRepository;
        }

        public Task<List<QuestionDetailsModel>> GetAll() => GetAllQuestionsDetails().ToListAsync();

        public Task<QuestionDetailsModel> FindById(Guid id) => GetAllQuestionsDetails().SingleOrDefaultAsync(c => c.Id == id);

        public async Task<Guid> CreateNew(QuestionCreateModel newQuestion)
        {
            var question = Domain.Entities.Question.Create(newQuestion.TeacherId, newQuestion.CourseId, newQuestion.Text);

            await _writeRepository.AddNewAsync(question);
            await _writeRepository.SaveAsync();

            return question.Id;
        }

        public async Task<Guid> Update(Guid id, QuestionCreateModel updatedQuestion)
        {
            var exist = await _readRepository.FindByIdAsync<Domain.Entities.Question>(id);
            if (exist != null)
            {
                exist.Update(updatedQuestion.TeacherId, updatedQuestion.CourseId, updatedQuestion.Text);
                await _writeRepository.UpdateAsync(id, exist);
                await _writeRepository.SaveAsync();
            }
            return exist.Id;
        }

        public async Task Delete(Guid id)
        {
            var question = await GetAllQuestionsDetails().Include(q => q.Answers).Where(q => q.Id == id).FirstOrDefaultAsync();

            foreach (var answer in question.Answers)
            {
                await _writeRepository.DeleteByIdAsync<Domain.Entities.Answer>(answer.Id);
            }

            await _writeRepository.DeleteByIdAsync<Domain.Entities.Question>(id);
            await _writeRepository.SaveAsync();
        }

        private IQueryable<QuestionDetailsModel> GetAllQuestionsDetails() => _readRepository.GetAll<Domain.Entities.Question>()
            .Select(q => new QuestionDetailsModel()
            {
                Id = q.Id,
                TeacherId = q.TeacherId,
                CourseId = q.CourseId,
                Text = q.Text,
                Answers = q.Answers
            });
    }
}
