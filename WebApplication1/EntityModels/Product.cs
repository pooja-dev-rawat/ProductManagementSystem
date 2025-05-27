using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.EntityModels
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public ICollection<Projects>? Projects { get; set; }
    }
}
