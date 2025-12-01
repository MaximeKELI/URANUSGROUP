using System.ComponentModel.DataAnnotations;

namespace UranusGroup.Models
{
    public class Service
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? LongDescription { get; set; }
        
        [StringLength(50)]
        public string Icon { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
        
        public int SortOrder { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public List<ServiceFeature> Features { get; set; } = new();
    }
    
    public class ServiceFeature
    {
        public int Id { get; set; }
        
        public int ServiceId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Feature { get; set; } = string.Empty;
        
        public int SortOrder { get; set; } = 0;
        
        public Service? Service { get; set; }
    }
}
