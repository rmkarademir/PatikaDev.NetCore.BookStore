using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatikaDev.NetCore.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Application.AuthorOperations.Queries.GetAuthorById
{
    public class GetAuthorByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int authorId { get; set; }
        public GetAuthorByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public AuthorIdViewModel Handle()
        {
            var author = _dbContext.Authors.Find(authorId);
            if (author is null)
                throw new InvalidOperationException("Yazar sistemde kayıtlı değil!");
            AuthorIdViewModel authorVM = _mapper.Map<AuthorIdViewModel>(author);
            return authorVM;
        }
    }
    public class AuthorIdViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
    }
}
