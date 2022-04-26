using System.Text.Json.Serialization;

namespace MoviesApi.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }

        [JsonIgnore]
        public List<Movie> Movies { get; set; } = new();
    }
}
