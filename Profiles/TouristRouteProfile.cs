using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeXiecheng.API.Dtos;
using FakeXiecheng.API.Models;

namespace FakeXiecheng.API.Profiles
{
    public class TouristRouteProfile : Profile
    {
        public TouristRouteProfile()
        {
            //TouristRoute: 原始对象   TouristRouteDto:映射的目标对象
            //ForMember自定义映射 dest:投影的目标对象，opt：原始对象
            CreateMap<TouristRoute, TouristRouteDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.OriginalPrice * (decimal)(src.Discount ?? 1)))
                .ForMember(dest => dest.TravelDays, opt => opt.MapFrom(src => src.TravelDays.ToString()))
                .ForMember(dest => dest.TripType, opt => opt.MapFrom(src => src.TripType.ToString()))
                .ForMember(dest => dest.DepartureCity, opt => opt.MapFrom(src => src.DepartureCity.ToString()));

            CreateMap<TouristRouteCreationDto, TouristRoute>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<TouristRouteUpdataDto, TouristRoute>();
        }
    }
}
