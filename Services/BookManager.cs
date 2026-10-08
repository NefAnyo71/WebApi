using Entities.Models;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class BookManager : IBookServices
    {
        private readonly IRepositoryManager _manager;
        private readonly ILogger _logger;

        public BookManager(IRepositoryManager manager, ILogger logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Book CreateOneBook(Book book)
        {
            if(book == null)
            {
                _logger.LogError("Book object sent from client is null.");
                throw new ArgumentNullException(nameof(book));
            }
            _manager.Book.CreateOneBook(book);
            _manager.Save();
            _logger.LogInformation($"Book with id: {book.Id} created successfully.");
            return book;
        }

        public void DeleteOneBook(int id, Book book, bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(id, trackChanges);
            if (entity == null)
            {
                _logger.LogWarning($"Book with id: {id} could not be found.");
                throw new ArgumentNullException(nameof(entity));
            }
            _manager.Book.DeleteOneBook(entity);
            _manager.Save();
            _logger.LogInformation($"Book with id: {id} deleted successfully.");
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _manager.Book.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            return _manager.Book.GetOneBookById(id, trackChanges);
        }

        public void UpdateOneBook(int id, Book book)
        {
            var entity = _manager.Book.GetOneBookById(id, true);
            if (entity == null)
            {
                _logger.LogWarning($"Book with id: {id} could not be found.");
                throw new ArgumentNullException(nameof(entity));
            }
            entity.title = book.title;
            entity.price = book.price;
            _manager.Save();
            _logger.LogInformation($"Book with id: {id} updated successfully.");
            
        }
    }
}
