using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatikaDev.NetCore.BookStore.Application.BookOperations.Commands.DeleteBook;
using PatikaDev.NetCore.BookStore.Application.BookOperations.Queries.GetBookById;
using PatikaDev.NetCore.BookStore.Application.BookOperations.Queries.GetBooks;
using PatikaDev.NetCore.BookStore.Application.BookOperations.Commands.CreateBook;
using PatikaDev.NetCore.BookStore.Application.BookOperations.Commands.UpdateBook;
using PatikaDev.NetCore.BookStore.DBOperations;
using PatikaDev.NetCore.BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PatikaDev.NetCore.BookStore.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static PatikaDev.NetCore.BookStore.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace PatikaDev.NetCore.BookStore.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context; // readonly sadece constrctor içerisinden set edilebilir
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context,_mapper);
            query.bookId = id;
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);  
        }
        /*[HttpGet("list")]
        public IActionResult GetList(string listName, string order)
        {
            var _Book = _context.Books.(x => x.Id == id).Tolist();
            if (order == "asc")
                return Ok(_context.Books.OrderBy(x=> x.));
            return Ok(_Book);
        }*/
        [HttpPost]
        public IActionResult Create([FromBody]CreateBookModel book)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = book;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody]UpdateBookModel book)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.bookId = id;
            command.Model = book;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return StatusCode(201);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.bookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command); 
            command.Handle();
            return Ok();
        }

    }
}
