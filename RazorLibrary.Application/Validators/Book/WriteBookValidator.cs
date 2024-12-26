using FluentValidation;
using RazorLibrary.Domain.DataTransferObject.Book;

namespace RazorLibrary.Application.Validators.Book
{
    public class WriteBookValidator : AbstractValidator<WriteBookDto>
    {
        public WriteBookValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Por favor, informe o título do livro")
                .MaximumLength(70).WithMessage("O título deve ter no máximo 70 caracteres");


            RuleFor(b => b.Publisher)
                .NotEmpty().WithMessage("Por favor, informe a editora")
                .MaximumLength(70).WithMessage("A editora deve ter no máximo 70 caracteres");

            RuleFor(b => b.Photo)
                .NotEmpty().WithMessage("Por favor, informe a foto do livro");

            RuleFor(b => b.Authors)
                .NotEmpty().WithMessage("Por favor, informe pelo menos 1 autor.");

            RuleForEach(b => b.Authors)
                .NotEmpty().WithMessage("Por favor, informe o nome do autor")
                .MaximumLength(70).WithMessage("O nome do autor deve ter no máximo 70 caracteres");
        }
    }
}
