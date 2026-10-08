using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IBookRepository Book { get;}
        void Save();
    }
}
