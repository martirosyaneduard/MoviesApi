using System.Text.Json.Serialization;

namespace MoviesApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Length { get; set; }

        public int Rating { get; set; }
        public DateTime RealesedYear { get; set; }

        [JsonIgnore]
        public Director? Director { get; set; }
        public int DirectorId { get; set; }

        public List<Actor> Actors { get; set; } = new();
        public List<Genre> Genres { get; set; } = new();

    }
}
