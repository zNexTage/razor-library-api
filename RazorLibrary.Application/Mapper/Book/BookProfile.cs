using AutoMapper;
using RazorLibrary.Domain.DataTransferObject.Book;
using RazorLibrary.Domain.Entities;

namespace RazorLibrary.Application.Mapper.Book
{
    using RazorLibrary.Domain.Entities;
    using System.Linq;

    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<WriteBookDto, Book>()
                .ForMember(dto => dto.Authors, 
                opt => opt.MapFrom(dto => String.Join(", ", dto.Authors)));

            CreateMap<Book, ReadBookDto>()
                .ForMember(dto => dto.Id,
                opt => opt.MapFrom(book => book.Id.ToString()))
                .ForMember(dto => dto.Authors,
                opt => opt.MapFrom(book => SplitAuthors(book.Authors)));
        }

        public List<string> SplitAuthors(string authors) => authors.Split(", ").ToList();
    }
}
