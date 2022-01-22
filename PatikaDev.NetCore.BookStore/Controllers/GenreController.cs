using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatikaDev.NetCore.BookStore.Application.GenreOperations.Commands.CreateGenre;
using PatikaDev.NetCore.BookStore.Application.GenreOperations.Commands.DeleteGenre;
using PatikaDev.NetCore.BookStore.Application.GenreOperations.Commands.UpdateGenre;
using PatikaDev.NetCore.BookStore.Application.GenreOperations.Queries.GetGenreById;
using PatikaDev.NetCore.BookStore.Application.GenreOperations.Queries.GetGenres;
using PatikaDev.NetCore.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PatikaDev.NetCore.BookStore.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static PatikaDev.NetCore.BookStore.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace PatikaDev.NetCore.BookStore.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context; // readonly sadece constrctor içerisinden set edilebilir
        private readonly IMapper _mapper;
        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetGenreByIdQuery query = new GetGenreByIdQuery(_context, _mapper);
            query.genreId = id;
            GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateGenreModel genre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model = genre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] UpdateGenreModel genre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.genreId = id;
            command.Model = genre;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return StatusCode(201);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.genreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
