using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.BookOperations.DeleteBook
{
    public class DeteleBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeteleBookCommandValidator()
        {
            RuleFor(command => command.bookId).GreaterThan(0);//bookId değeri 0 dan daha büyük olmalı.
        }
    }
}
