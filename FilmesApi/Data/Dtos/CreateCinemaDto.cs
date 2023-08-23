using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos
{
  public class CreateCinemaDto
  {
    [Required(ErrorMessage = "O Nome do Cinema é Obrigatório")]
    public string Nome { get; set; }
    public int EnderecoId { get; set; }
  }
}
