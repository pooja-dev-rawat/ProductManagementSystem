using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.EntityModels
{
    public class TeamMember
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Name { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        public ICollection<TeamMemberProject>? TeamMemberProjects { get; set; }
    }

    public class TeamMemberProject
    {
        public int TeamMemberId { get; set; }
        public TeamMember? TeamMember { get; set; }

        public int ProjectId { get; set; }
        public Projects? Project { get; set; }
    }

}
