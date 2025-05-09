namespace BookApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Pages { get; set; } = 0;
        public string ISBN { get; set; } = string.Empty;
    }
}
