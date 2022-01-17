using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatikaDev.NetCore.BookStore.BookOperations.CreateBook;
using PatikaDev.NetCore.BookStore.BookOperations.GetBookById;
using PatikaDev.NetCore.BookStore.BookOperations.GetBooks;
using PatikaDev.NetCore.BookStore.BookOperations.UpdateBook;
using PatikaDev.NetCore.BookStore.DBOperations;
using PatikaDev.NetCore.BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PatikaDev.NetCore.BookStore.BookOperations.CreateBook.CreateBookCommand;
using static PatikaDev.NetCore.BookStore.BookOperations.UpdateBook.UpdateBookCommand;

namespace PatikaDev.NetCore.BookStore.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context; // readonly sadece constrctor içerisinden set edilebilir
        public BookController(BookStoreDbContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public IActionResult Get()
        {
            GetBooksQuery getBooksQuery = new GetBooksQuery(_context);
            var result = getBooksQuery.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery getBookByIdQuery = new GetBookByIdQuery(_context);
            try
            {
                var result = getBookByIdQuery.Handle(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = book;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody]UpdateBookModel book)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.Model = book;
                command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return StatusCode(201);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book is null)
                return BadRequest(id + " Idli kitap sistemde kayıtlı değil!");
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }

    }
}
