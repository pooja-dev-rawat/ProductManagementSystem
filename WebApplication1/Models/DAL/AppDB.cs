using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityModels;

namespace WebApplication1.Models.DAL
{
    public class AppDB:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamMemberProject> TeamMembersProject { get; set; }    
        public AppDB(DbContextOptions<AppDB> options) : base(options) {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamMemberProject>()
                .HasKey(p=> new {p.TeamMemberId,p.ProjectId});

            modelBuilder.Entity<TeamMemberProject>()
                .HasOne(p => p.TeamMember)
                .WithMany(p => p.TeamMemberProjects)
                .HasForeignKey(p=> p.TeamMemberId);

            modelBuilder.Entity<TeamMemberProject>()
                .HasOne(p=>p.Project)
                .WithMany(p=>p.TeamMemberProjects)
                .HasForeignKey(p=>p.ProjectId);

            
        }

    }
}
