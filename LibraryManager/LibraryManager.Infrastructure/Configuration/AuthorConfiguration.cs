using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManager.Infrastructure.Configuration
{
    internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable(nameof(Author), LibraryContext.LibrarySchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnOrder(0);
            builder.Property(e => e.CreatedAt).HasColumnOrder(1).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(100);            

            builder.HasMany(a => a.Books)
               .WithMany(b => b.Authors)
               .UsingEntity<BookAuthor>(
           j => j.HasOne(ba => ba.Book).WithMany().HasForeignKey(ba => ba.BookId),
           j => j.HasOne(ba => ba.Author).WithMany().HasForeignKey(ba => ba.AuthorId),
           j => j.ToTable("BookAuthor"));
        }
    }
}