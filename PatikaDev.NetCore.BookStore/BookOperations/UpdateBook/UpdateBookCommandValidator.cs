using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.bookId).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);//UpdateBookCommand türündeki Model nesnesinin GenreId alanı 0 dan daha büyük olmalı.
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(2);
            //RuleFor(command => command.Model.PageCount).GreaterThan(0);
            //RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}
