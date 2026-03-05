namespace Codebymister.Application.UseCases.Projects.Commands.DeleteProject;

public interface IDeleteProject
{
    Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
