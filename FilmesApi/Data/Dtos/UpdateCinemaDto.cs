using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos
{
  public class UpdateCinemaDto
  {
    [Required(ErrorMessage = "O Nome do Cinema é Obrigatório")]
    public string Nome { get; set; }
  }
}
