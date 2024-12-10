namespace LibraryManager.Domain.Entities.Shared
{
    public abstract class Entity<TId>
    {        
        public TId Id { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public Entity(TId id)
        {
            Id = id;
        }        
    }
}
