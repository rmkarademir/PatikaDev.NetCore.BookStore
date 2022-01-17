using Microsoft.EntityFrameworkCore;
using PatikaDev.NetCore.BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.DBOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
