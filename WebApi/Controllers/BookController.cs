using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using Repositories.EFCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepositoryManager _manager;
        private readonly ILogger<BookController> _logger;

        public BookController(IRepositoryManager manager, ILogger<BookController> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.Book.GetAllBooks(false);
                _logger.LogInformation("Tüm kitaplar başarıyla getirildi.");
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Tüm kitaplar getirilirken bir hata oluştu.");
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _manager.Book.GetOneBookById(id, false);

                if (book is null)
                {
                    _logger.LogWarning($"{id} ID'li kitap bulunamadı.", id);
                    return NotFound();
                }

                _logger.LogInformation($"{id} ID'li kitap getirildi.", id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{id} ID'li kitap getirilirken bir hata oluştu.", id);
                throw new Exception(ex.Message);
            }
        }
        [HttpPost()]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                _manager.Book.CreateOneBook(book);
                _manager.Save();
                _logger.LogInformation($"{book.Id} ID'li kitap mysql e eklendi.", book.Id);
                return CreatedAtAction(nameof(GetOneBook), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{book.Id} ID'li kitap eklenirken bir hata oluştu.", book.Id);
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] Book book)
        {
            try
            {
                var entity = _manager.Book.GetOneBookById(id, true);
                if (entity is null)
                {
                    _logger.LogWarning($"{id} ID'li kitap bulunamadı.", id);
                    return NotFound();
                }
                if (id != book.Id)
                {
                    _logger.LogWarning($"{id} ID'li kitap güncellenirken bir hata oluştu.", id);
                    return BadRequest();
                }
                entity.title = book.title;
                entity.price = book.price;
                _manager.Save();
                _logger.LogInformation($"{id} ID'li kitap güncellendi.", id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{id} ID'li kitap güncellenirken bir hata oluştu.", id);
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _manager.Book.GetOneBookById(id, true);
                if (entity is null)
                {
                    _logger.LogWarning($"{id} ID'li kitap bulunamadı.", id);
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = $"Book with id: '{id}' could not found."
                    });
                }
                _manager.Book.DeleteOneBook(entity);
                _manager.Save();
                _logger.LogInformation($"{id} ID'li kitap mysql den silindi.",id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{id} Kitap silinmeye çalışırken bir sorun ile karşılaşıldı.",id);
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartialyUpdateOneBook([FromRoute(Name = "id")] int id
            , [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var entity = _manager.Book.GetOneBookById(id, true);
            if (entity is null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Book with id: '{id}' could not found."
                });
            }
            bookPatch.ApplyTo(entity);
            _manager.Save();
            return NoContent();
        }
    }
}
