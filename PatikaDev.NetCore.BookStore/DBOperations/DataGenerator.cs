using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PatikaDev.NetCore.BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                    new Book
                    {
                        Title = "Kayıp Tanrılar Ülkesi",
                        GenreId = 1,
                        AuthorId = 1,
                        PageCount = 504,
                        PublishDate = new DateTime (2021, 11, 20)
                    },
                    new Book
                    {
                        Title = "Sis ve Gece",
                        GenreId = 1,
                        AuthorId = 1,
                        PageCount = 276,
                        PublishDate = new DateTime (1996, 10, 20)
                    },
                    new Book
                    {
                        Title = "Beni Ödülle Cezalandırma",
                        GenreId = 2,
                        AuthorId = 2,
                        PageCount = 248,
                        PublishDate = new DateTime(2016, 09, 20)
                    }
                );
                
                if (context.Genres.Any())
                {
                    return;
                }
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "PersonalGrowth"
                    },
                    new Genre
                    {
                        Name = "ScienceFiction"
                    },
                    new Genre
                    {
                        Name = "Noval"
                    }
                );
                if (context.Authors.Any())
                {
                    return;
                }
                context.Authors.AddRange(
                    new Author
                    {
                        FirstName = "Ahmet",
                        LastName = "Ümit",
                        BirthDate = new DateTime(1960,09,30)
                    },
                    new Author
                    {
                        FirstName = "Özgür",
                        LastName = "Bolat",
                        BirthDate = new DateTime(1979, 01, 01)
                    },
                    new Author
                    {
                        FirstName = "J. R. R.",
                        LastName = "Tolkien",
                        BirthDate = new DateTime(1892, 01, 03)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
