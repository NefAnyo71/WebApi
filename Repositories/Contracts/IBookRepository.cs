using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Contracts
{
    public interface IBookRepository : IrepositoryBase<Book>
    {
        IQueryable<Book> GetAllBooks(bool trackChanges);
        Book GetOneBookById(int bookId, bool trackChanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
    }
}
