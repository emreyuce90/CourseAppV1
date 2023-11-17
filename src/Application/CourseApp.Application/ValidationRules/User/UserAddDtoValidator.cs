using CourseApp.Application.Dtos.User;
using FluentValidation;

namespace CourseApp.Application.ValidationRules.User {
    public class UserAddDtoValidator : AbstractValidator<UserAddDto> {
        public UserAddDtoValidator() {
            RuleFor(u => u.Name)
                 .NotNull()
                 .NotEmpty()
                 .WithMessage("İsim alanı boş geçilemez")
                 .EmailAddress()
                 .WithMessage("E-Mail adresi geçerli değil");
            RuleFor(u => u.Surname)
                 .NotNull()
                 .NotEmpty()
                 .WithMessage("Soyisim alanı boş geçilemez")
                 .EmailAddress()
                 .WithMessage("E-Mail adresi geçerli değil");

            RuleFor(u => u.Email)
                 .NotNull()
                 .NotEmpty()
                 .WithMessage("E-mail adresi boş geçilemez")
                 .EmailAddress()
                 .WithMessage("E-Mail adresi geçerli değil");


            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Şifre alanı boş geçilemez");

            RuleFor(u => u.PasswordConfirm)
                .NotNull()
                .NotEmpty()
                .WithMessage("Şifre tekrar alanı boş geçilemez")
                .Equal(x => x.Password)
                .WithMessage("Şifre ve şifre tekrar alanı aynı olmalıdır");

        }
    }
}
