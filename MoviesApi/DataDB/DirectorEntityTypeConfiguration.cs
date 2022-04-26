using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesApi.Models;

namespace MoviesApi.DataDB
{
    public class DirectorEntityTypeConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
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

            builder.Property(d=>d.Birthday)
                .HasColumnType("DATE");

            builder.HasMany(d => d.Movies)
                   .WithOne(m => m.Director);

        }
    }
}
