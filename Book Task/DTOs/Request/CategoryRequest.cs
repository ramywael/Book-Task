using System.ComponentModel.DataAnnotations;

namespace Book_Task.DTOs.Request
{
    public class CategoryRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
