namespace WebApi.Domain.Entities
{
    public class Book : BaseEnity
    {
        public string Title { get; set; } = default!;
        public List<Author> Authors { get; set; } = default!;
    }
}