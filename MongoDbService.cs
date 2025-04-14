using MongoDB.Driver;
using BookApi.Models;
using MongoDB.Bson;
using System.Collections.Generic;
public class MongoDbService
{
    private readonly IMongoCollection<Book> _booksCollection;

    public MongoDbService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("MongoDb"));
        var database = client.GetDatabase("LibraryDb");  // 替換為你的資料庫名稱
        _booksCollection = database.GetCollection<Book>("Books");  // 替換為你的資料表名稱
    }

    // 獲取所有書籍
    public async Task<List<Book>> GetBooksAsync()
    {
        return await _booksCollection.Find(book => true).ToListAsync(); // 使用 ToListAsync 來進行異步查詢
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await _booksCollection.Find(book => book.Id == id).FirstOrDefaultAsync(); // 使用 FirstOrDefaultAsync 來查詢
    }

    // 新增書籍，使用異步方法
    public async Task AddBookAsync(Book book)
    {
        await _booksCollection.InsertOneAsync(book); // 使用 InsertOneAsync 來進行異步插入
    }

    // 更新書籍，使用異步方法
    public async Task UpdateBookAsync(int id, Book updatedBook)
    {
        await _booksCollection.ReplaceOneAsync(book => book.Id == id, updatedBook); // 使用 ReplaceOneAsync 來進行異步更新
    }

    // 刪除書籍，使用異步方法
    public async Task DeleteBookAsync(int id)
    {
        await _booksCollection.DeleteOneAsync(book => book.Id == id); // 使用 DeleteOneAsync 來進行異步刪除
    }

    public async Task<Book?> GetBookByISBNAsync(string isbn) // 非同步方法，回傳 Book 或 null
    {
        return await _booksCollection
            .Find(book => book.ISBN == isbn) // 在 MongoDB 查找 ISBN 相符的文件
            .FirstOrDefaultAsync(); // 回傳第一筆，若找不到就回傳 null
    }
}
