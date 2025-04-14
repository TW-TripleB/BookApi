var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // ✅ 加入 Controller 支援
builder.Services.AddSingleton<MongoDbService>(); 
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers(); // ✅ 註冊 Controllers（讓 BooksController 可用）

app.Run();
