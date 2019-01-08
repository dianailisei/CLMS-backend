using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Attendance.Business.Presence.Models;

namespace Attendance.Business.Presence
{
    public interface IPresenceService
    {
        Task<List<PresenceDetailsModel>> GetAll();

        Task<PresenceDetailsModel> FindById(Guid id);

        Task<Guid> Create(PresenceCreateModel newPresence);

       // Task<Guid> Update(Guid id, SessionUpdateModel updatedPresence);

        Task Delete(Guid id);
    }
}
