using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        { 
      
            CreateMap<Post, GetAllPostsViewModel>().ReverseMap();
        }
    }
}
