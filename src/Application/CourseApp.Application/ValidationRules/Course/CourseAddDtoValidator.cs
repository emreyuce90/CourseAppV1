using CourseApp.Application.Dtos.Course;
using FluentValidation;

namespace CourseApp.Application.ValidationRules.Course {
    public class CourseAddDtoValidator : AbstractValidator<CourseAddDto> {
        public CourseAddDtoValidator() {
            RuleFor(x => x.Description)
               .NotEmpty()
               .WithMessage("Açıklama alanı boş geçilemez");
            RuleFor(x => x.Title)
               .NotEmpty()
               .WithMessage("Başlık alanı boş geçilemez");
        }
    }
}
