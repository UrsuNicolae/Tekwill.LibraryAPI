using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations
{
    public class GenConfiguration : IEntityTypeConfiguration<Gen>
    {
        public void Configure(EntityTypeBuilder<Gen> builder)
        {
            builder.ToTable("genres");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(p => p.AuthorGenres)
                .WithOne(b => b.Gen)
                .HasForeignKey(b => b.GenId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
