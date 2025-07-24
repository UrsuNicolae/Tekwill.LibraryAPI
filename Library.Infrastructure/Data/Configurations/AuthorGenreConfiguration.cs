using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations
{
    public class AuthorGenreConfiguration : IEntityTypeConfiguration<AuthorGenres>
    {
        public void Configure(EntityTypeBuilder<AuthorGenres> builder)
        {
            builder.ToTable("author_genres");
            builder.HasKey(p => new {p.AuthorId, p.GenId});

            builder.HasOne(p => p.Author)
                .WithMany(b => b.AuthorGenres)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(p => p.Gen)
                .WithMany(b => b.AuthorGenres)
                .HasForeignKey(b => b.GenId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
