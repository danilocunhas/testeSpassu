using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public int BookCode { get; set; }
        public string Title { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public int Edition { get; set; }
        public int PublishYear { get; set; }
        public decimal Price { get; set; }
        public List<int> AuthorIds { get; set; } = [];        
    }
}
