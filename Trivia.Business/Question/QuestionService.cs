using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trivia.Domain.Interfaces;
using Trivia.Business.Answer;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Trivia.Business.Question
{
    public sealed class QuestionService : IQuestionService
    {
        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;

        public QuestionService(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            this._readRepository = readRepository;
            this._writeRepository = writeRepository;
        }

        public Task<List<QuestionDetailsModel>> GetAll() => GetAllQuestionsDetails().ToListAsync();

        public Task<QuestionDetailsModel> FindById(Guid id) => GetAllQuestionsDetails().SingleOrDefaultAsync(q => q.Id == id);

        public Task<QuestionDetailsModel> FindByCourseId(Guid id) => GetAllQuestionsDetails().SingleOrDefaultAsync(q => q.CourseId == id);

        public async Task<Guid> CreateNew(QuestionCreateModel newQuestion)
        {
            var question = Domain.Entities.Question.Create(newQuestion.TeacherId, newQuestion.CourseId, newQuestion.Duration,
                                                           newQuestion.Points, newQuestion.Text);

            await _writeRepository.AddNewAsync(question);
            await _writeRepository.SaveAsync();
            await _writeRepository.SaveAsync();

            return question.Id;
        }

        public async Task<Guid> CreateFromExisting(Guid questionId)
        {
            var existingQuestion = await FindById(questionId);
            if(existingQuestion != null)
            {
                var newQuestion = Domain.Entities.Question.Create(existingQuestion.TeacherId, existingQuestion.CourseId,
                                                                  existingQuestion.Duration, existingQuestion.Points,
                                                                  existingQuestion.Text);

                await _writeRepository.AddNewAsync(newQuestion);
                await _writeRepository.SaveAsync();
                await _writeRepository.SaveAsync();

                return newQuestion.Id;
            }
            return existingQuestion.Id;
        }

        //public async Task<bool> AddAnswer(Guid questionId, AnswerCreateModel answer)
        //{
        //    var existingQuestion = await _readRepository.FindByIdAsync<Domain.Entities.Question>(questionId);
        //    if (existingQuestion != null)
        //    {
        //        if (QuestionStillAvailable(existingQuestion.AddTime, existingQuestion.Duration))
        //        {
        //            var addAnswer = Domain.Entities.Answer.Create(answer.StudentId, questionId, answer.Text);
        //            existingQuestion.AddAnswer(addAnswer);
        //            existingQuestion.Answers.Add(addAnswer);

        //            await _writeRepository.UpdateAsync(existingQuestion.Id, existingQuestion);
        //            await _writeRepository.SaveAsync();
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public async Task<Guid> Update(Guid id, QuestionCreateModel updatedQuestion)
        {
            var exist = await _readRepository.FindByIdAsync<Domain.Entities.Question>(id);
            if (exist != null)
            {
                exist.Update(updatedQuestion.TeacherId, updatedQuestion.CourseId, updatedQuestion.Duration,
                             updatedQuestion.Points, updatedQuestion.Text);
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
                AddTime = q.AddTime,
                Duration = q.Duration,
                Points = q.Points,
                Text = q.Text,
                Answers = q.Answers
            });
    }
}
