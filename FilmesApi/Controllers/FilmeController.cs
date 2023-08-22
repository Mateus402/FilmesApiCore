using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesApi.Controllers
{

  [ApiController]
  [Route("[Controller]")]
  public class FilmeController: ControllerBase
  {
    private static List<Filme> filmes = new List<Filme>();
    private static int id = 0;

    [HttpPost]
    public IActionResult AddFilme([FromBody] Filme filme)
    {
      filme.Id = id++;
      filmes.Add(filme);
      return CreatedAtAction(nameof(GetFilmeById), 
        new { id = filme.Id }, 
        filme);
    }

    [HttpGet]
    public IEnumerable<Filme> GetFilmes([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
      return filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetFilmeById(int id) 
    {
      var filme = filmes.FirstOrDefault(filme => filme.Id == id);
      if(filme== null) return NotFound();
      return Ok(filme);
    }
  }
}
