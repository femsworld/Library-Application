using System.Text;
using AutoMapper;
using WebApi.Business.Dto;
using WebApi.Domain.Entities;

namespace WebApi.Business
{
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
          
        }
    }
}