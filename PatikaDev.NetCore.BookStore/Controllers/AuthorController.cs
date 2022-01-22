using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatikaDev.NetCore.BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using PatikaDev.NetCore.BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using PatikaDev.NetCore.BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using PatikaDev.NetCore.BookStore.Application.AuthorOperations.Queries.GetAuthorById;
using PatikaDev.NetCore.BookStore.Application.AuthorOperations.Queries.GetAuthors;
using PatikaDev.NetCore.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PatikaDev.NetCore.BookStore.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static PatikaDev.NetCore.BookStore.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace PatikaDev.NetCore.BookStore.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _context; // readonly sadece constrctor içerisinden set edilebilir
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetAuthorByIdQuery query = new GetAuthorByIdQuery(_context, _mapper);
            query.authorId = id;
            GetAuthorByIdQueryValidator validator = new GetAuthorByIdQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateAuthorModel author)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = author;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateAuthorModel author)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.authorId = id;
            command.Model = author;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return StatusCode(201);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.authorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
