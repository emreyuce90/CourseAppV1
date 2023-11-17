using CourseApp.Application.Dtos.User;
using FluentValidation;

namespace CourseApp.Application.ValidationRules.User {
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto> {
        public UserLoginDtoValidator() {
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("E-mail adresiniz boş olamaz")
                .EmailAddress()
                .WithMessage("Lütfen geçerli bir e-mail adresi giriniz");

            RuleFor(u => u.Password)
               .NotEmpty()
               .WithMessage("Şifreniz boş olamaz")

        }
    }
}
