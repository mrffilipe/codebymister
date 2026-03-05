namespace Codebymister.Application.UseCases.Outreach.Commands.DeleteOutreach;

public interface IDeleteOutreach
{
    Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
