using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesApi.Models;

namespace MoviesApi.DataDB
{
    public class MovieEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Length)
                .IsRequired()
                .HasColumnType("FLOAT");

            builder.Property(m => m.Rating)
                .IsRequired()
                .HasPrecision(1, 5);

            builder.Property(m => m.RealesedYear)
                .IsRequired()
                .HasColumnType("DATE");


        }
    }
}
