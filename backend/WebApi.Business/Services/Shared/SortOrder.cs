using System.Text.Json.Serialization;

namespace WebApi.Business.Services.Shared
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortOrder
    {
        Ascending,
        Descending,
    }
}