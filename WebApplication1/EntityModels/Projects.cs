using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.EntityModels
{
    public class Projects
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Title { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public ICollection<TeamMemberProject>? TeamMemberProjects { get; set; }
    }
}
