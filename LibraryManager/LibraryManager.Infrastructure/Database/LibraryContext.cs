using LibraryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Database;

internal sealed class LibraryContext : DbContext
{
    public const string LibrarySchema = "Library";

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();

    public LibraryContext()      
    {

    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {

    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {        
        configurationBuilder.Properties<decimal>().HavePrecision(10, 2);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(LibrarySchema);        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryContext).Assembly);
    }

    public async ValueTask SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
    }
}