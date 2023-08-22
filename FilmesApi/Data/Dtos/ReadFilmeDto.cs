using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos
{
  public class ReadFilmeDto
  {
    public string Titulo { get; set; } = null!;
    public string Genero { get; set; } = null!;
    public int Duracao { get; set; }
  }
}
