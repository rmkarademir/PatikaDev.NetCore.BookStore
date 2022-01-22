using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatikaDev.NetCore.BookStore.Common;
using PatikaDev.NetCore.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Application.BookOperations.Queries.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int bookId { get; set; }
        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BookIdViewModel Handle()
        {
            var book = _dbContext.Books.Include(x=>x.Genre).Include(x => x.Author).SingleOrDefault(x=>x.Id==bookId);
            if (book is null)
                throw new InvalidOperationException("Kitap sistemde kayıtlı değil!");
            BookIdViewModel bookVM = _mapper.Map<BookIdViewModel>(book);
            return bookVM;
        }
    }
    public class BookIdViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}

