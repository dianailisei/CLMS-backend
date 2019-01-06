using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attendance.Business.Presence.Models;
using Attendance.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Business.Presence
{
    public class PresenceService : IPresenceService
    {
        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;

        public PresenceService(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public Task<List<PresenceDetailsModel>> GetAll() => GetAllPresencesDetails().ToListAsync();

        public Task<PresenceDetailsModel> FindById(Guid id) => GetAllPresencesDetails().SingleOrDefaultAsync(s => s.Id == id);

        public async Task<Guid> Create(PresenceCreateModel newPresence)
        {
            var sessions = _readRepository.GetAll<Domain.Session>().Where(s => s.ConfirmationCode.Equals(newPresence.ConfirmationCode));
            
            if (sessions.Count() > 0)
            {
                
                var session = sessions.First();
                var presence = Domain.Presence.Create(newPresence.StudentId, session);
                //session.Presences.Add(presence);

                await _writeRepository.AttachObject(session);
                await _writeRepository.AddNewAsync(presence);
                //await _writeRepository.AddNewAsync<Domain.Session>(session);
                await _writeRepository.SaveAsync();

                return presence.Id;
            }
            return Guid.Empty;
        }

        public async Task Delete(Guid id)
        {
            await _writeRepository.DeleteByIdAsync<Domain.Presence>(id);
            await _writeRepository.SaveAsync();
        }

        private IQueryable<PresenceDetailsModel> GetAllPresencesDetails() => _readRepository.GetAll<Domain.Presence>()
            .Where(p => p.Available)
            .Select(p => new PresenceDetailsModel()
            {
                Id = p.Id,
                StudentId = p.StudentId,
                SessionEnrolled = p.SessionEnrolled,
                SubmitDate = p.SubmitDate
            });
    }
}
