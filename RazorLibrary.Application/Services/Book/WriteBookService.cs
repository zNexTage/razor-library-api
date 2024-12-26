using FluentValidation;
using RazorLibrary.Application.Validators.Book;
using RazorLibrary.Domain.Adapters.Repositories;
using RazorLibrary.Domain.Adapters.Repositories.Book;
using RazorLibrary.Domain.Adapters.Services.Book;
using RazorLibrary.Domain.DataTransferObject.Book;
using RazorLibrary.Domain.Entities;
using RazorLibrary.Domain.Exception;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RazorLibrary.Application.Services.Book
{
    public class WriteBookService : IWriteBookService
    {
        private readonly IWriteBookRepository _wRepository;
        private readonly IReadBookRepository _rRepository;

        private readonly IUnitOfWork _unitOfWork;

        public WriteBookService(IWriteBookRepository wRepository,
            IReadBookRepository rRepository,
            IUnitOfWork unitOfWork)
        {
            _wRepository = wRepository;
            _rRepository = rRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReadBookDto> Add(WriteBookDto bookDto)
        {
            Validate(bookDto);

            var authors = String.Join(", ", bookDto.Authors);

            var book = new Domain.Entities.Book()
            {
                Title = bookDto.Title,
                Photo = bookDto.Photo,
                Publisher = bookDto.Publisher,
                Authors = authors
            };

            await _wRepository.Add(book);
            await _unitOfWork.CommitAsync();

            return new ReadBookDto()
            {
                Id = book.Id.ToString(),
                Title = book.Title,
                Photo = book.Photo,
                Publisher = book.Publisher,
                Authors = book.Authors.Split(',').ToList()
            };
        }

        public async Task Delete(string id)
        {
            var bookExists = await _rRepository.Exists(id);

            if (!bookExists)
            {
                throw new NotFoundException("Não é possível concluir a ação, pois o livro buscado não existe."); 
            }

            await _wRepository.Delete(id);

            await _unitOfWork.CommitAsync();
        }

        public async Task<ReadBookDto> Edit(string id, WriteBookDto bookDto)
        {
            Validate(bookDto);

            var book = await _rRepository.GetById(id);

            if (book is null)
            {
                throw new NotFoundException("Não é possível concluir a ação, pois o livro buscado não existe.");
            }

            book.Title = bookDto.Title;
            book.Publisher = bookDto.Publisher;
            book.Authors = String.Join(", ", bookDto.Authors);
            book.Photo = bookDto.Photo;

            await _wRepository.Edit(book);

            await _unitOfWork.CommitAsync();

            return new ReadBookDto()
            {
                Authors = book.Authors.Split(", ").ToList(),
                Photo = bookDto.Photo,
                Id = book.Id.ToString(),
                Title = book.Title,
                Publisher = book.Publisher
            };
        }

        private void Validate(WriteBookDto bookDto)
        {
            var validator = new WriteBookValidator();

            var result = validator.Validate(bookDto);

            if (!result.IsValid)
            {
                var errors = result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage);

                throw new Domain.Exception.ValidationException(errors);
            }
        }
    }
}
