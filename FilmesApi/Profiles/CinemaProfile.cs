using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
  public class CinemaProfile : Profile
  {
    public CinemaProfile()
    {
      CreateMap<CreateCinemaDto, Cinema>();
      CreateMap<UpdateCinemaDto, Cinema>();
      CreateMap<Cinema, UpdateCinemaDto>();
      CreateMap<Cinema, ReadCinemaDto>()
        .ForMember(cinemaDto => cinemaDto.ReadEnderecoDto, opt => opt.MapFrom(cinema => cinema.Endereco))
        .ForMember(cinemaDto => cinemaDto.ReadEnderecoDto, opt => opt.MapFrom(cinema => cinema.Sessoes)); ;
    }
  }
}
