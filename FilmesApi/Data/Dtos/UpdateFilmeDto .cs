using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos
{
  public class UpdateFilmeDto
  {
    [Required(ErrorMessage = "O Título do Filme é Obrigatório")]
    public string Titulo { get; set; } = null!;

    [Required(ErrorMessage = "O Gênero do Filme é Obrigatório")]
    [StringLength(50, ErrorMessage = "O tamanho não pode ultrapassar 50 caracteres")]
    public string Genero { get; set; } = null!;

    [Required(ErrorMessage = "A Duração do Filme é Obrigatória!")]
    [Range(70, 600, ErrorMessage = "A Duração deve ter entre 70min a 600min")]
    public int Duracao { get; set; }
  }
}
