using System.Text.Json.Serialization;

namespace WebApi.Domain.Entities
{
    public class Book : BaseEnity
    {
        public string Title { get; set; } = default!;
        public List<Author> Authors { get; set; } = default!;

        public Genre Genre { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public int Inventory { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Genre
    {
        TextBooks,
        Novel,
        Fiction,
        ResearchPaper
    }
}