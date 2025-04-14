using BookApi.Models;
public class BookService
{
    private readonly MongoDbService _db;

    public BookService(MongoDbService db) => _db = db;

    public async Task<(bool Success, string Error)> ValidateNewBookAsync(Book book)
    {
        if (string.IsNullOrWhiteSpace(book.Title))
            return (false, "書名不能為空");

        if (book.Pages < 1)
            return (false, "頁數不能小於 1");

        var existing = await _db.GetBookByISBNAsync(book.ISBN);
        if (existing != null)
            return (false, "ISBN 已存在");

        return (false, "Unknown error");

    }
}
