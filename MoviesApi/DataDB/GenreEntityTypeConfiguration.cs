using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesApi.Models;

namespace MoviesApi.DataDB
{
    public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
           builder.HasKey(x => x.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasMany(g=>g.Movies)
                   .WithMany(m=>m.Genres);
        }
    }
}
