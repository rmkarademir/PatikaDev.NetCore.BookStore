using Microsoft.EntityFrameworkCore;
using PatikaDev.NetCore.BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.DBOperations
{
    public class LinqDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName:"BookStoreDB");
        }
    }
}
