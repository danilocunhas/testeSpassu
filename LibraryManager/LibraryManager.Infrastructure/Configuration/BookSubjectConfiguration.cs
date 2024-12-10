using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManager.Infrastructure.Configuration
{
    internal sealed class BookSubjectConfiguration : IEntityTypeConfiguration<BookSubject>
    {
        public void Configure(EntityTypeBuilder<BookSubject> builder)
        {
            builder.ToTable(nameof(BookSubject), LibraryContext.LibrarySchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnOrder(0);           
        }
    }
}
