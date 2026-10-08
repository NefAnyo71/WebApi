using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contract
{
    public interface IBookServices
    {
        IEnumerable<Book> GetAllBooks(bool trackChanges); 
        Book GetOneBookById(int id, bool trackChanges);
        Book CreateOneBook(Book book);
        void UpdateOneBook(int id, Book book);
        void DeleteOneBook(int id,Book book,bool trackChanges);

    }
}
