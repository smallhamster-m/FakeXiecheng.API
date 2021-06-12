﻿using AutoMapper;
using FakeXiecheng.API.Dtos;
using FakeXiecheng.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXiecheng.API.Profiles
{
    public class TouristRoutePicProfile : Profile
    {
        public TouristRoutePicProfile()
        {
            CreateMap<TouristRoutePic, TouristRoutePicDto>();
            CreateMap<TouristRoutePicCreationDto, TouristRoutePic>();
        }
    }
}
