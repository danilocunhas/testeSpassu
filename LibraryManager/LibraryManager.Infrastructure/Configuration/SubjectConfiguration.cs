using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManager.Infrastructure.Configuration
{
    internal sealed class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable(nameof(Subject), LibraryContext.LibrarySchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnOrder(0);
            builder.Property(e => e.CreatedAt).HasColumnOrder(1).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(100);

            builder.HasMany(s => s.Books)
           .WithMany(b => b.Subjects)
           .UsingEntity<BookSubject>(
               j => j.HasOne(bs => bs.Book).WithMany().HasForeignKey(bs => bs.BookId),
               j => j.HasOne(bs => bs.Subject).WithMany().HasForeignKey(bs => bs.SubjectId),
               j => j.ToTable("BookSubject"));
        }
    }
}