using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attendance.Business.Session.Models;
using Attendance.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Business.Session
{
    public class SessionService : ISessionService
    {
        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;

        public SessionService(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public Task<List<SessionDetailsModel>> GetAll() => GetAllSessionsDetails().ToListAsync();

        public Task<SessionDetailsModel> FindById(Guid id) => GetAllSessionsDetails().SingleOrDefaultAsync(s => s.Id == id);

        public async Task<Guid> CreateNew(SessionCreateModel newSession)
        {
            var session = Domain.Session.Create(newSession.LaboratoryId, GenerateConfirmationCode(), newSession.Duration);

            await _writeRepository.AddNewAsync(session);
            await _writeRepository.SaveAsync();

            return session.Id;
        }

        public async Task<Guid> Update(Guid id, SessionUpdateModel updatedSession)
        {
            var exist = await _readRepository.FindByIdAsync<Domain.Session>(id);
            if (exist != null)
            {
                exist.Update(exist.ConfirmationCode, updatedSession.ExtraDuration);
                await _writeRepository.UpdateAsync(id, exist);
                await _writeRepository.SaveAsync();
            }
            return exist.Id;
        }

        public async Task Delete(Guid id)
        {
            var session = await GetAllSessionsDetails().Include(p => p.Presences)
                .Where(s => s.Id == id && s.Presences.Any(p => p.Available == true))
                .FirstOrDefaultAsync();


            foreach (var presence in session.Presences)
            {
                await _writeRepository.DeleteByIdAsync<Domain.Presence>(presence.Id);
            }

            await _writeRepository.DeleteByIdAsync<Domain.Session>(id);
            await _writeRepository.SaveAsync();
        }

        private IQueryable<SessionDetailsModel> GetAllSessionsDetails() => _readRepository.GetAll<Domain.Session>()
            .Select(t => new SessionDetailsModel()
            {
                Id = t.Id,
                LaboratoryId = t.LaboratoryId,
                ConfirmationCode = t.ConfirmationCode,
                StartDate = t.StartTime,
                EndDate = t.EndTime,
                Duration = GetDuration(t.StartTime, t.EndTime),
                Presences = t.Presences
            });

        private int GetDuration(DateTime startTime, DateTime endTime)
        {
            TimeSpan span = endTime.Subtract(startTime);
            return (int)span.TotalMinutes;
        }

        private string GenerateConfirmationCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}

