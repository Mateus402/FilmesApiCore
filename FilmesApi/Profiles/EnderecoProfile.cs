using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
  public class EnderecoProfile : Profile
  {
    protected EnderecoProfile()
    {
      CreateMap<CreateEnderecoDto, Endereco>();
      CreateMap<UpdateEnderecoDto, Endereco>();
      CreateMap<Endereco, ReadEnderecoDto>();
    }
  }
}
