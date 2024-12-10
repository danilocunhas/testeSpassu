using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManager.Infrastructure.Configuration
{
    internal sealed class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.ToTable(nameof(BookAuthor), LibraryContext.LibrarySchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnOrder(0);           
        }
    }
}
