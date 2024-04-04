using AutoMapper;
using BuisnessLayer.Domain;
using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() : base("MappingProfile")
        {
            CreateMap<Book, BookDomain>()
                .ForMember(dest => dest.LentByUser, opt => opt.MapFrom(src => src.LentByUser))
                .ForMember(dest => dest.CurrentBorrowedByUser, opt => opt.MapFrom(src => src.CurrentBorrowedByUser))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BookDomain, Book>()
                .ForMember(dest => dest.LentByUser, opt => opt.MapFrom(src => src.LentByUser))
                .ForMember(dest => dest.CurrentBorrowedByUser, opt => opt.MapFrom(src => src.CurrentBorrowedByUser))
                .ForMember(x => x.Name, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Lent_By_User_id, opt => opt.Ignore())
                .ForMember(x => x.Currently_Borrowed_By_User_Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<User, UserDomain>().ReverseMap()
                .ForMember(dest => dest.Books_Borrowed, opt => opt.MapFrom(src => src.Books_Borrowed))
                .ForMember(dest => dest.Books_Lent, opt => opt.MapFrom(src => src.Books_Lent));
        }
    }
}
