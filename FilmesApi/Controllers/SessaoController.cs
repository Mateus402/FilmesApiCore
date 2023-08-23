using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
  [ApiController]
  [Route("[Controller]")]
  public class SessaoController : ControllerBase
  {

    private FilmeContext _context;
    private IMapper _mapper;

    public SessaoController(
      FilmeContext context,
      IMapper mapper
      )
    {
      _context = context;
      _mapper = mapper;
    }

    /// <summary>
    /// Adiciona uma Sessao
    /// </summary>
    /// <param name="sessaoDto">Objeto com os campos necessários para a criação de uma Sessao</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AddSessao([FromBody] CreateSessaoDto sessaoDto)
    {
      Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
      _context.Sessoes.Add(sessao);
      _context.SaveChanges();
      return CreatedAtAction(nameof(GetSessaoById),
        new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId },
        sessao);
    }

    /// <summary>
    /// Busca todos os Sessões Paginado de 0 a 10
    /// </summary>
    /// <param name="skip">Objeto que parametriza o Inicio da páginação</param>
    /// <param name="take">Objeto que parametriza o Limite da páginação</param>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<ReadSessaoDto> GetSessoes([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
      return _mapper.Map<List<ReadSessaoDto>>(_context.Sessoes.Skip(skip).Take(take));
    }

    /// <summary>
    /// Busca uma Sessao pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{filmeId}/{cinemaId}")]
    public IActionResult GetSessaoById(int filmeId, int cinemaId)
    {
      var sessao = _context.Sessoes
        .FirstOrDefault(sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);
      if (sessao == null) return NotFound();
      var sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
      return Ok(sessaoDto);
    }

    ///// <summary>
    ///// Deleta uma Sessao pelo ID
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //[HttpDelete("{id}")]
    //public IActionResult DeleteSessao(int id)
    //{
    //  var sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
    //  if (sessao == null) return NotFound();
    //  _context.Remove(sessao);
    //  _context.SaveChanges();
    //  return NoContent();
    //}
  }
}
