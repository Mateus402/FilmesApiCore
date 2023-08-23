using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
  public class Filme
  {
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O Título do Filme é Obrigatório")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O Gênero do Filme é Obrigatório")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "A Duração do Filme é Obrigatória!")]
    [Range(70, 600, ErrorMessage = "A Duração deve ter entre 70min a 600min")]
    public int Duracao { get; set; }
  }
}