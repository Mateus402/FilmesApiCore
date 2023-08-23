﻿using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
  public class Cinema
  {
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O Nome do Cinema é Obrigatório")]
    public string Nome { get; set; } = null!;
    public int EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; }
  }
}