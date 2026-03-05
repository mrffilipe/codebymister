namespace Codebymister.Application.UseCases.Maintenance.Commands.DeleteMaintenance;

public interface IDeleteMaintenance
{
    Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
