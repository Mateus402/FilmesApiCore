using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
  [ApiController]
  [Route("[Controller]")]
  public class EnderecoController : ControllerBase
  {
    private FilmeContext _context;
    private IMapper _mapper;

    public EnderecoController(
      FilmeContext context,
      IMapper mapper
      )
    {
      _context = context;
      _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um Endereco
    /// </summary>
    /// <param name="EnderecoDto">Objeto com os campos necessários para a criação de um Endereco</param>
    /// <returns></returns>
    public IActionResult AddEndereco([FromBody] CreateEnderecoDto enderecoDto)
    {
      Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
      _context.Enderecos.Add(endereco);
      _context.SaveChanges();
      return CreatedAtAction(nameof(GetEnderecoById), new { id = endereco.Id }, endereco);
    }

    /// <summary>
    /// Busca todos os Enderecos Paginado de 0 a 10
    /// </summary>
    /// <param name="skip">Objeto que parametriza o Inicio da páginação</param>
    /// <param name="take">Objeto que parametriza o Limite da páginação</param>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<ReadEnderecoDto> GetEnderecos([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
      return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos.Skip(skip).Take(take));
    }

    /// <summary>
    /// Busca um Endereco pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetEnderecoById(int id)
    {
      var endereco = _context.Enderecos
        .FirstOrDefault(endereco => endereco.Id == id);
      if (endereco == null) return NotFound();
      var enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
      return Ok(enderecoDto);
    }

    /// <summary>
    /// Alterar um Endereco pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="enderecoDto">Objeto com os campos necessários para a Alteração de um Endereco</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult UpdateCinema(int id, [FromBody] UpdateEnderecoDto enderecoDto)
    {
      var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
      if (endereco == null) return NotFound();
      _mapper.Map(enderecoDto, endereco);
      _context.SaveChanges();
      return NoContent();
    }

    /// <summary>
    /// Deleta um Endereco pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteEndereco(int id)
    {
      var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
      if (endereco == null) return NotFound();
      _context.Remove(endereco);
      _context.SaveChanges();
      return NoContent();
    }
  }
}

