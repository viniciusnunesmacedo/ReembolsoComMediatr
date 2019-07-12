using Flunt.Notifications;
using Flunt.Validations;

namespace ReembolsoComMediatr.Web.Domain
{
    public abstract class Validatable : Notifiable, IValidatable
    {
        public abstract void Validate();
    }
}
