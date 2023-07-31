namespace WebApi.Domain.Entities
{
    public class Book : BaseEnity
    {
        public string Title { get; set; } = default!;
        public List<Author> Authors { get; set; } = default!;

        public Genre Genre { get; set; }
        public int Inventory { get; set; }
    }

    public enum Genre
    {
        TextBooks,
        Novel,
        Fiction,
        ResearchPaper
    }
}