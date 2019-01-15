using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Attendance.Business.Session.Models;

namespace Attendance.Business.Session
{
    public interface ISessionService
    {
        Task<List<SessionDetailsModel>> GetAll();

        Task<SessionDetailsModel> FindById(Guid id);

        Task<List<SessionDetailsModel>> FindByLaboratory(Guid id);

        Task<SessionDetailsModel> CreateNew(SessionCreateModel newSession);

        Task<Guid> Update(Guid id, SessionUpdateModel updateSession);

        Task Delete(Guid id);
    }
}
