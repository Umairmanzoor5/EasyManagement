using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Projects;
using EM.Services;
using Microsoft.EntityFrameworkCore;

namespace EM.Business;

public class ProjectService : IProjectService
{
    private readonly ApplicationDbContext _context;

    public ProjectService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<ListProjects>> ListProjectTask()
    {
        var listProject = await _context.ViewProjects.Select(project => new ListProjects
        {
            Reference = project.Reference,
            Description = project.Description,
            Value= project.Value,
            Client = project.Client,
            CreateDate = project.CreateDate,
            UpdateDate = project.UpdateDate,
            State = project.State,
        }).ToListAsync();

        return listProject;
    }

    public async Task CreateProjectTask(CreateProject project)
    {
        var newProject = new Project
        {
            Reference = project.Reference,
            Description = project.Description,
            CreateDate = DateTime.Now,
            IdClient= project.IdClient,
            IdState = 1,
            Value = 0
        };

        await _context.Projects.AddAsync(newProject);
        await _context.SaveChangesAsync();
    }

    public async Task<InfoProject> InfoProjectTask(string id)
    {
        var project = await _context.ViewProjects.FirstAsync(p => p.Reference.Equals(id));

        var proj = new InfoProject
        {
            Client = project.Client,
            CreateDate = project.CreateDate,
            Description = project.Description,
            Value = project.Value,
            Reference = project.Reference,
            UpdateDate = project.UpdateDate,
            State = project.State
        };

        return proj;
    }

    public async Task RecuseProjectTask(string id)
    {
        var project = await _context.Projects.FirstAsync(p => p.Reference.Equals(id));

        project.IdState = 2;

        project.UpdateDate = DateTime.Now;

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task ApproveProjectTask(string id)
    {
        var project = await _context.Projects.FirstAsync(p => p.Reference.Equals(id));

        project.IdState = 3;

        project.UpdateDate = DateTime.Now;

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDateTask(string id)
    {
        var project = await _context.Projects.FirstAsync(p => p.Reference.Equals(id));

        project.UpdateDate = DateTime.Now;

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeStateTask(string id, int state)
    {
        var project = await _context.Projects.FirstAsync(p => p.Reference.Equals(id));

        project.IdState = state;

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }
}