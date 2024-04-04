using AutoMapper;
using BuisnessLayer.Domain;
using Book_Borrowing_System.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Borrowing_System.Mapper
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile() : base("ApiMappingProfile")
        {
            CreateMap<BookDto, BookDomain>().ReverseMap()
                .ForMember(dest => dest.LentByUser, opt => opt.MapFrom(src => src.LentByUser))
                .ForMember(dest => dest.CurrentBorrowedByUser, opt => opt.MapFrom(src => src.CurrentBorrowedByUser));
            CreateMap<BookUpdateDto, BookDomain>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserDto, UserDomain>().ReverseMap()
                .ForMember(dest => dest.Books_Borrowed, opt => opt.MapFrom(src => src.Books_Borrowed))
                .ForMember(dest => dest.Books_Lent, opt => opt.MapFrom(src => src.Books_Lent));
        }
    }
}
