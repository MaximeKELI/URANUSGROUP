using FluentValidation;
using UranusGroup.DTOs;

namespace UranusGroup.Validators
{
    public class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
    {
        public CreateContactDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom est requis")
                .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("L'email est requis")
                .EmailAddress().WithMessage("L'email doit être valide")
                .MaximumLength(100).WithMessage("L'email ne peut pas dépasser 100 caractères");

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Le téléphone ne peut pas dépasser 20 caractères")
                .Matches(@"^[\+]?[0-9\s\-\(\)]+$").WithMessage("Le format du téléphone n'est pas valide")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Le sujet est requis")
                .MaximumLength(200).WithMessage("Le sujet ne peut pas dépasser 200 caractères");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Le message est requis")
                .MaximumLength(2000).WithMessage("Le message ne peut pas dépasser 2000 caractères")
                .MinimumLength(10).WithMessage("Le message doit contenir au moins 10 caractères");
        }
    }

    public class SubscribeNewsletterDtoValidator : AbstractValidator<SubscribeNewsletterDto>
    {
        public SubscribeNewsletterDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("L'email est requis")
                .EmailAddress().WithMessage("L'email doit être valide")
                .MaximumLength(100).WithMessage("L'email ne peut pas dépasser 100 caractères");

            RuleFor(x => x.Source)
                .MaximumLength(50).WithMessage("La source ne peut pas dépasser 50 caractères");
        }
    }
}
