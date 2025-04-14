using Microsoft.AspNetCore.Mvc; // 引入 ASP.NET Core Web API 的控制器功能
using BookApi.Models;

namespace BookApi.Controllers // 命名空間，確保這個 Controller 屬於 BooksApi
{
    [Route("api/[controller]")] // 設定 API 路徑，例如 /api/books
    [ApiController] // 標記這個類別為 Web API 控制器
    public class BooksController : ControllerBase // 繼承 ControllerBase，代表這是一個 API 控制器
    {
        // 建立一個模擬書籍資料的 List
        // private static readonly List<Book> Books = new()
        // {
        //     new Book { Id = 1, Title = "C# 入門", Author = "作者 A" },
        //     new Book { Id = 2, Title = "ASP.NET Core 高手之路", Author = "作者 B" }
        // };
        
        private readonly MongoDbService _mongoDbService;

        // 通過建構函數注入 MongoDbService
        public BooksController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        // 取得所有書籍
        [HttpGet] // 這個方法對應 HTTP GET 請求，例如 GET /api/books
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _mongoDbService.GetBooksAsync();  // 使用 await 呼叫異步方法
            return Ok(books); // 回傳 200 OK 並帶回書籍清單
        }


        // 取得單一本書籍（根據 ID）
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _mongoDbService.GetBookByIdAsync(id); // 使用 await 呼叫異步方法
            if (book == null)
            {
                return NotFound(); // 如果找不到，回傳 404 Not Found
            }
            return Ok(book); // 回傳 200 OK 並帶回書籍
        }

        // 新增一本書籍
        [HttpPost] // 這個方法對應 HTTP POST /api/books
        public async Task<ActionResult<Book>> AddBook(Book newBook)
        {
            await _mongoDbService.AddBookAsync(newBook);  // 使用 await 呼叫異步方法
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook); // 回傳 201 Created
        }

        // 更新書籍資訊
        [HttpPut("{id}")] // 這個方法對應 HTTP PUT /api/books/{id}
        public async Task<IActionResult> UpdateBookAsync(int id, Book updatedBook)
        {
            var book = await _mongoDbService.GetBookByIdAsync(id); // 使用 await 呼叫異步方法
            if (book == null)
            {
                return NotFound(); // 如果找不到，回傳 404 Not Found
            }
            await _mongoDbService.UpdateBookAsync(id, updatedBook);  // 使用 await 呼叫異步方法
            return NoContent(); // 回傳 204 No Content
        }

        // 刪除書籍
        [HttpDelete("{id}")] // 這個方法對應 HTTP DELETE /api/books/{id}
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _mongoDbService.GetBookByIdAsync(id);  // 使用 await 呼叫異步方法
            if (book == null)
            {
                return NotFound(); // 如果找不到，回傳 404 Not Found
            }
            await _mongoDbService.DeleteBookAsync(id); // 使用 await 呼叫異步方法
            return NoContent(); // 回傳 204 No Content
        }
    }
}
