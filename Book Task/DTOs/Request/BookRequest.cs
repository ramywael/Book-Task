using System.ComponentModel.DataAnnotations;

namespace Book_Task.DTOs.Request
{
    public class BookRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal Price { get; set; }
        [Required]
        public string Auther { get; set; } = string.Empty;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be non-negative.")]
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }
}
