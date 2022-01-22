using AutoMapper;
using PatikaDev.NetCore.BookStore.Application.AuthorOperations.Queries.GetAuthorById;
using PatikaDev.NetCore.BookStore.Application.AuthorOperations.Queries.GetAuthors;
using PatikaDev.NetCore.BookStore.Application.BookOperations.Queries.GetBookById;
using PatikaDev.NetCore.BookStore.Application.BookOperations.Queries.GetBooks;
using PatikaDev.NetCore.BookStore.Application.GenreOperations.Queries.GetGenreById;
using PatikaDev.NetCore.BookStore.Application.GenreOperations.Queries.GetGenres;
using PatikaDev.NetCore.BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PatikaDev.NetCore.BookStore.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static PatikaDev.NetCore.BookStore.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static PatikaDev.NetCore.BookStore.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace PatikaDev.NetCore.BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();//CreateBookModel objesi Book objesine map edilebilir
            CreateMap<Book, BookIdViewModel>().ForMember(dest=>dest.Genre, 
                opt => opt.MapFrom(src=>src.Genre.Name)).ForMember(dest => dest.Author,
                opt => opt.MapFrom(src => src.Author.FirstName +" "+ src.Author.LastName));//her bir eleman için (ForMember) o elemandaki (dest.Genre) Genre alanına eşle (opt.MapForm),
                                                                            //hedef nesnedeki (src.GenreId) GenreId alanı ile
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author,
                opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName));
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreViewModel>();
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorIdViewModel>();
        }
        
    }
}
