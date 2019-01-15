using Attendance.Business.Session.Models;
using FluentValidation;

namespace Attendance.Business.Session.Validations
{
    public class SessionCreateModelValidator: AbstractValidator<SessionCreateModel>
    {
        public SessionCreateModelValidator()
        {
            RuleFor(s => s.Duration).GreaterThanOrEqualTo((short) 10);
            RuleFor(s => s.Duration).LessThanOrEqualTo((short) 120);
        }
    }
}
