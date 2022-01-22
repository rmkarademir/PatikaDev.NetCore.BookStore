using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Services
{
    public interface ILoggerService
    {
        public void Write(string message);
    }
}
