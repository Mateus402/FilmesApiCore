﻿using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
  public class CinemaProfile : Profile
  {
    protected CinemaProfile()
    {
      CreateMap<CreateCinemaDto, Cinema>();
      CreateMap<UpdateCinemaDto, Cinema>();
      CreateMap<Cinema, ReadCinemaDto>();
    }
  }
}