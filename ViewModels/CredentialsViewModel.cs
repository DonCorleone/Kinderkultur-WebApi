
using KinderKulturServer.ViewModels.Validations;
using FluentValidation.Validators;

namespace KinderKulturServer.ViewModels
{
    // ToDo .NET Core 3.0 [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
