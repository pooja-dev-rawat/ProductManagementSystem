using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityModels;
using WebApplication1.Models.DAL;

namespace WebApplication1.Controllers
{
    public class TeamMemberProjectsController : Controller
    {
        private readonly AppDB _context;

        public TeamMemberProjectsController(AppDB context)
        {
            _context = context;
        }

        // GET: TeamMemberProjects
        public async Task<IActionResult> Index()
        {
            var appDB = _context.TeamMembersProject.Include(t => t.Project).Include(t => t.TeamMember);
            return View(await appDB.ToListAsync());
        }

        // GET: TeamMemberProjects/Details/5
        public async Task<IActionResult> Details(int? teamMemberId, int? projectId)
        {
            if (teamMemberId == null || projectId==null)
            {
                return NotFound();
            }

            var teamMemberProject = await _context.TeamMembersProject
                .Include(t => t.Project)
                .Include(t => t.TeamMember)
                .FirstOrDefaultAsync(m => m.TeamMemberId == teamMemberId && m.ProjectId==projectId);
            if (teamMemberProject == null)
            {
                return NotFound();
            }

            return View(teamMemberProject);
        }

        // GET: TeamMemberProjects/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title");
            ViewData["TeamMemberId"] = new SelectList(_context.TeamMembers, "Id", "Name");
            ViewData["TeamMembers"] = new SelectList(_context.TeamMembers, "Id", "Name");
            return View();
        }

        // POST: TeamMemberProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamMemberId,ProjectId")] TeamMemberProject teamMemberProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamMemberProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title", teamMemberProject.ProjectId);
            ViewData["TeamMemberId"] = new SelectList(_context.TeamMembers, "Id", "Name", teamMemberProject.TeamMemberId);
            return View(teamMemberProject);
        }

        // GET: TeamMemberProjects/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int teamMemberId, int projectId)
        {
            var teamMemberProject = await _context.TeamMembersProject
         .FirstOrDefaultAsync(x => x.TeamMemberId == teamMemberId && x.ProjectId == projectId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title", teamMemberProject.ProjectId);
            ViewData["TeamMemberId"] = new SelectList(_context.TeamMembers, "Id", "Name", teamMemberProject.TeamMemberId);
            return View(teamMemberProject);
        }

        // POST: TeamMemberProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int teamMemberId, int projectId, [Bind("TeamMemberId,ProjectId")] TeamMemberProject teamMemberProject)
        {
            if (teamMemberId != teamMemberProject.TeamMemberId || projectId != teamMemberProject.ProjectId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = await _context.TeamMembersProject
    .FirstOrDefaultAsync(x => x.TeamMemberId == teamMemberProject.TeamMemberId && x.ProjectId == teamMemberProject.ProjectId);
                    if (existing != null)
                    {
                        _context.TeamMembersProject.Remove(existing);
                    }

                    var updated = new TeamMemberProject
                    {
                        TeamMemberId = teamMemberProject.TeamMemberId,
                        ProjectId = teamMemberProject.ProjectId
                    };

                    //existing.ProjectId= teamMemberProject.ProjectId;
                    //existing.TeamMemberId= teamMemberProject.TeamMemberId;
                    _context.TeamMembersProject.Add(updated);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = await _context.TeamMembersProject
                        .AnyAsync(x => x.TeamMemberId == teamMemberId && x.ProjectId == projectId);

                    if (!exists)
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title", teamMemberProject.ProjectId);
            ViewData["TeamMemberId"] = new SelectList(_context.TeamMembers, "Id", "Name", teamMemberProject.TeamMemberId);
            return View(teamMemberProject);
        }

        // GET: TeamMemberProjects/Delete/5
        public async Task<IActionResult> Delete(int teamMemberId, int projectId)
        {
            var teamMemberProject = await _context.TeamMembersProject
       .Include(tmp => tmp.TeamMember)
            .Include(tmp => tmp.Project)
       .FirstOrDefaultAsync(m => m.TeamMemberId == teamMemberId && m.ProjectId == projectId);

            if (teamMemberProject == null)
                return NotFound();

            return View(teamMemberProject);
        }

        // POST: TeamMemberProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int teamMemberId, int projectId)
        {
            var teamMemberProject = await _context.TeamMembersProject
         .FirstOrDefaultAsync(x => x.TeamMemberId == teamMemberId && x.ProjectId == projectId);

            if (teamMemberProject != null)
            {
                _context.TeamMembersProject.Remove(teamMemberProject);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TeamMemberProjectExists(int id)
        {
            return _context.TeamMembersProject.Any(e => e.TeamMemberId == id);
        }

        [HttpPost]
        public async Task<IActionResult> submiprojectmembers([FromBody] TeamMemberProjectCreateDto dto)
        {
            if (dto.SelectedTeamMemberIds == null || dto.ProjectId == 0)
                return BadRequest("Invalid data");

            foreach (var memberId in dto.SelectedTeamMemberIds)
            {
                var tmp = new TeamMemberProject
                {
                    TeamMemberId = memberId,
                    ProjectId = dto.ProjectId // or dto.ProjectId if it's Product
                };
                _context.TeamMembersProject.Add(tmp);
            }

            await _context.SaveChangesAsync();

            var appDB = _context.TeamMembersProject.Include(t => t.Project).Include(t => t.TeamMember);
            return View(await appDB.ToListAsync());
        }





    }
}
