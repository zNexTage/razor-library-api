using AutoMapper;
using RazorLibrary.Application.Mapper.Book;

namespace RazorLibrary.Tests.Commom.Mock.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Build()
        {
            return new AutoMapper.MapperConfiguration(options =>
            {
                options.AddMaps(typeof(BookProfile).Assembly);
            }).CreateMapper();
        }
    }
}
