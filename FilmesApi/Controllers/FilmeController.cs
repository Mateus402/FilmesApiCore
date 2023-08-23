using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{

  [ApiController]
  [Route("[Controller]")]
  public class FilmeController : ControllerBase
  {

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(
      FilmeContext context, 
      IMapper mapper
      )
    {
      _context = context;
      _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um Filme
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para a criação de um Filme</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AddFilme([FromBody] CreateFilmeDto filmeDto)
    {
      Filme filme = _mapper.Map<Filme>(filmeDto); 
      _context.Filmes.Add(filme);
      _context.SaveChanges();
      return CreatedAtAction(nameof(GetFilmeById),
        new { id = filme.Id },
        filme);
    }

    /// <summary>
    /// Busca todos os Filmes Paginado de 0 a 10
    /// </summary>
    /// <param name="skip">Objeto que parametriza o Inicio da páginação</param>
    /// <param name="take">Objeto que parametriza o Limite da páginação</param>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<ReadFilmeDto> GetFilmes([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
      return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take).ToList());
    }

    /// <summary>
    /// Busca um Filme pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetFilmeById(int id)
    {
      var filme = _context.Filmes
        .FirstOrDefault(filme => filme.Id == id);
      if (filme == null) return NotFound();
      var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
      return Ok(filmeDto);
    }

    /// <summary>
    /// Alterar um Filme pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="filmeDto">Objeto com os campos necessários para a criação de um Filme</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult UpdateFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
      var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
      if (filme == null) return NotFound();
      _mapper.Map(filmeDto, filme);
      _context.SaveChanges();
      return NoContent();
    }

    /// <summary>
    /// Altera um Filme pelo ID usando o PATCH
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patch">Particiona o JSON em partes para alterar somente o necessário</param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public IActionResult UpdateFilmePatch(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
      var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
      if (filme == null) return NotFound();

      var filmeUpdate = _mapper.Map<UpdateFilmeDto>(filme);

      patch.ApplyTo(filmeUpdate, ModelState);

      if (!TryValidateModel(filmeUpdate))
      {
        return ValidationProblem(ModelState);
      }

      _mapper.Map(filmeUpdate, filme);
      _context.SaveChanges();
      return NoContent();
    }

    /// <summary>
    /// Deleta um Filme pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteFilme(int id)
    {
      var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
      if (filme == null) return NotFound();
      _context.Remove(filme);
      _context.SaveChanges();
      return NoContent();
    }
  }
}
