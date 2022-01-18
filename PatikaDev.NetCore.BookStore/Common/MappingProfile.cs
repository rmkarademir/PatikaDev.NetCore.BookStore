using AutoMapper;
using PatikaDev.NetCore.BookStore.BookOperations.GetBookById;
using PatikaDev.NetCore.BookStore.BookOperations.GetBooks;
using PatikaDev.NetCore.BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PatikaDev.NetCore.BookStore.BookOperations.CreateBook.CreateBookCommand;

namespace PatikaDev.NetCore.BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();//CreateBookModel objesi Book objesine map edilebilir
            CreateMap<Book, BookIdViewModel>().ForMember(dest=>dest.Genre, 
                opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));//her bir eleman için (ForMember) o elemandaki (dest.Genre) Genre alanına eşle (opt.MapForm),
                                                                            //hedef nesnedeki (src.GenreId) GenreId alanı ile
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
        
    }
}
