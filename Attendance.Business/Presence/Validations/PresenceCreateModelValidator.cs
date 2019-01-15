using Attendance.Business.Presence.Models;
using FluentValidation;

namespace Attendance.Business.Presence.Validations
{
    class PresenceCreateModelValidator : AbstractValidator<PresenceCreateModel>
    {
        public PresenceCreateModelValidator()
        {
            RuleFor(p => p.ConfirmationCode).Length(6, 6);
        }
    }
}
