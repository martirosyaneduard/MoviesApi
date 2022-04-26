using System.Text.Json.Serialization;

namespace MoviesApi.Models
{
    public class Genre
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Movie> Movies { get; set; } = new();
    }
}
