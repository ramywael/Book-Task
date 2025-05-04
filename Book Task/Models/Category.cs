namespace Book_Task.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public IEnumerable<Book> Books { get; set; } = null!;
    }
}
