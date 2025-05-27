namespace WebApplication1
{
    public class TeamMemberProjectCreateDto
    {
        public int ProjectId { get; set; }
        public List<int>? SelectedTeamMemberIds { get; set; }
    }

    public class EditTeamMemberProjectViewModel
    {
        public int OriginalTeamMemberId { get; set; }
        public int OriginalProjectId { get; set; }

        public int NewTeamMemberId { get; set; }
        public int NewProjectId { get; set; }
    }
}
