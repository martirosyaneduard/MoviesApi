using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesApi.Models;

namespace MoviesApi.DataDB
{
    public class ActorEntityTypeConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.Nationality)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(d => d.Birthday)
                .HasColumnType("DATE");

            builder.HasMany(a => a.Movies)
                .WithMany(m => m.Actors);
        }
    }
}
