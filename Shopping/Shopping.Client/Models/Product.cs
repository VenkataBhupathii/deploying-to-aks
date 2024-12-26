using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shopping.Client.Models
{
    public class Product
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Category { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string ImageFile { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
    }
}