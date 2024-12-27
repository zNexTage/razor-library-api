using AutoMapper;
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
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public WriteBookService(IWriteBookRepository wRepository,
            IReadBookRepository rRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _wRepository = wRepository;
            _rRepository = rRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReadBookDto> Add(WriteBookDto bookDto)
        {
            Validate(bookDto);

            var book = _mapper.Map<Domain.Entities.Book>(bookDto);

            await _wRepository.Add(book);
            await _unitOfWork.CommitAsync();

            var readDto = _mapper.Map<ReadBookDto>(book);

            return readDto;
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

            _mapper.Map(bookDto, book);

            await _wRepository.Edit(book);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReadBookDto>(book);
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
