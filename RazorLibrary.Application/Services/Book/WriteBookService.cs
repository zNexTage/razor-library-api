using FluentValidation;
using RazorLibrary.Application.Validators.Book;
using RazorLibrary.Domain.Adapters.Repositories;
using RazorLibrary.Domain.Adapters.Repositories.Book;
using RazorLibrary.Domain.Adapters.Services.Book;
using RazorLibrary.Domain.DataTransferObject.Book;
using RazorLibrary.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RazorLibrary.Application.Services.Book
{
    public class WriteBookService : IWriteBookService
    {
        private readonly IWriteBookRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public WriteBookService(IWriteBookRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
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

            await _repository.Add(book);
            await _unitOfWork.CommitAsync();

            return new ReadBookDto()
            {
                Id = book.Id,
                Title = book.Title,
                Photo = book.Photo,
                Publisher = book.Publisher,
                Authors = book.Authors.Split(',').ToList()
            };
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ReadBookDto> Edit(WriteBookDto bookDto)
        {
            throw new NotImplementedException();
        }

        private void Validate(WriteBookDto bookDto)
        {
            var validator = new WriteBookValidator();

            validator.ValidateAndThrow(bookDto);
        }
    }
}
