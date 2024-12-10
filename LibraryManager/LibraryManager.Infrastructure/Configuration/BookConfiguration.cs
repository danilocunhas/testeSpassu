using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManager.Infrastructure.Configuration
{
    internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book), LibraryContext.LibrarySchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnOrder(0);
            builder.Property(e => e.CreatedAt).HasColumnOrder(1).IsRequired();
            builder.Property(e => e.BookCode).IsRequired();
            builder.Property(e => e.Edition).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Publisher).HasMaxLength(100).IsRequired();
            builder.Property(e => e.PublishYear).HasMaxLength(4).IsRequired();
            builder.Property(e => e.Title).HasMaxLength(100).IsRequired();            

            builder.HasMany(b => b.Authors)
              .WithMany(a => a.Books)
              .UsingEntity<BookAuthor>(
                  j => j.HasOne(ba => ba.Author).WithMany().HasForeignKey(ba => ba.AuthorId),
                  j => j.HasOne(ba => ba.Book).WithMany().HasForeignKey(ba => ba.BookId),
                  j => j.ToTable("BookAuthor"));

            builder.HasMany(b => b.Subjects)
           .WithMany(s => s.Books)
           .UsingEntity<BookSubject>(
               j => j.HasOne(bs => bs.Subject).WithMany().HasForeignKey(bs => bs.SubjectId),
               j => j.HasOne(bs => bs.Book).WithMany().HasForeignKey(bs => bs.BookId),
               j => j.ToTable("BookSubject"));
        }
    }
}
