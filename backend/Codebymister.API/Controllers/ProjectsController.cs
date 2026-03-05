using Codebymister.Application.UseCases.Projects.Commands.CreateProject;
using Codebymister.Application.UseCases.Projects.Commands.DeleteProject;
using Codebymister.Application.UseCases.Projects.Queries.GetAllProjects;
using Codebymister.Application.UseCases.Projects.Queries.GetProjectById;
using Codebymister.Application.UseCases.Projects.Commands.UpdateProject;
using Codebymister.API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebymister.API.Controllers;

[Authorize]
public class ProjectsController : V1BaseController
{
    private readonly ICreateProject _createProject;
    private readonly IGetAllProjects _getAllProjects;
    private readonly IGetProjectById _getProjectById;
    private readonly IUpdateProject _updateProject;
    private readonly IDeleteProject _deleteProject;

    public ProjectsController(
        ICreateProject createProject,
        IGetAllProjects getAllProjects,
        IGetProjectById getProjectById,
        IUpdateProject updateProject,
        IDeleteProject deleteProject)
    {
        _createProject = createProject;
        _getAllProjects = getAllProjects;
        _getProjectById = getProjectById;
        _updateProject = updateProject;
        _deleteProject = deleteProject;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await _createProject.ExecuteAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var projects = await _getAllProjects.ExecuteAsync(cancellationToken);
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var project = await _getProjectById.ExecuteAsync(id, cancellationToken);
        if (project == null)
            return NotFound();

        return Ok(project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await _updateProject.ExecuteAsync(id, request, cancellationToken);
        if (project == null)
            return NotFound();

        return Ok(project);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var success = await _deleteProject.ExecuteAsync(id, cancellationToken);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
