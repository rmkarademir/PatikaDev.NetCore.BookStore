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
                        //Id = 1,
                        Title = "Kayıp Tanrılar Ülkesi",
                        GenreId = 1,
                        PageCount = 504,
                        PublishDate = new DateTime (2021, 11, 20)
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "Sis ve Gece",
                        GenreId = 1,
                        PageCount = 276,
                        PublishDate = new DateTime (1996, 10, 20)
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Beni Ödülle Cezalandırma",
                        GenreId = 2,
                        PageCount = 248,
                        PublishDate = new DateTime(2016, 09, 20)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
