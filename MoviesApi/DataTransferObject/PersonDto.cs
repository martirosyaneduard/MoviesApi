using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DataTransferObject
{
    public class PersonDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Nationality { get; set; } = string.Empty;

        [Range(typeof(DateTime), "01-01-1970", "01-01-2022")]
        public DateTime Birthday { get; set; }
    }
}
