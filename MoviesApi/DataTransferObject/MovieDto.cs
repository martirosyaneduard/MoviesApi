using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DataTransferObject
{
    public class MovieDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public double Length { get; set; }

        [Required]
        [Range(0,5)]
        public int Rating { get; set; }

        [Required]
        [Range(typeof(DateTime), "01-01-1970", "01-01-2022")]
        public DateTime RealesedYear { get; set; }

        public List<int> GenresIds { get; set; } = new();
        public List<int> ActorsIds { get; set; } = new();

        [Required]
        [Range(0,int.MaxValue)]
        public int DirectorId { get; set; }
    }
}
