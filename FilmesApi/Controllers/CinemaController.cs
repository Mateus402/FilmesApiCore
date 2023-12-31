﻿using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
  [ApiController]
  [Route("[Controller]")]
  public class CinemaController : ControllerBase
  {
    private FilmeContext _context;
    private IMapper _mapper;

    public CinemaController(
      FilmeContext context, 
      IMapper mapper
      )
    {
      _context = context;
      _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um Cinema
    /// </summary>
    /// <param name="cinemaDto">Objeto com os campos necessários para a criação de um Cinema</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AddCinema([FromBody] CreateCinemaDto cinemaDto)
    {
      Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
      _context.Cinemas.Add(cinema);
      _context.SaveChanges();
      return CreatedAtAction(nameof(GetCinemaById), new { id = cinema.Id }, cinema);
    }

    /// <summary>
    /// Busca todos os cinemas 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<ReadCinemaDto> GetCinemas([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
      return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
    }

    /// <summary>
    /// Busca um Cinema pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetCinemaById(int id)
    {
      var cinema = _context.Cinemas
        .FirstOrDefault(cinema => cinema.Id == id);
      if (cinema == null) return NotFound();
      var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
      return Ok(cinemaDto);
    }

    /// <summary>
    /// Alterar um Cinema pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cinemaDto">Objeto com os campos necessários para a Alteração de um Cinema</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult UpdateCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
    {
      var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
      if (cinema == null) return NotFound();
      _mapper.Map(cinemaDto, cinema);
      _context.SaveChanges();
      return NoContent();
    }

    /// <summary>
    /// Deleta um Cinema pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteCinema(int id)
    {
      var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
      if (cinema == null) return NotFound();
      _context.Remove(cinema);
      _context.SaveChanges();
      return NoContent();
    }
  }
}
