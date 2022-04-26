using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DataTransferObject
{
    public class GenreDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
    }
}
