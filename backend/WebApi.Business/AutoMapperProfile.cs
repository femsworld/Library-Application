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
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            // CreateMap<UserDto, User>();
            CreateMap<User, UserAdminDto>();
            CreateMap<UserAdminDto, User>();
            CreateMap<User, UserChangePasswordDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, BookReadDto>().ReverseMap();
            // CreateMap<BookDto, Book>();
            CreateMap<Loan, LoanDto>().ReverseMap();
            CreateMap<LoanBook, LoanBookDto>().ReverseMap();
          
        }
    }
}