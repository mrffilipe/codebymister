namespace Codebymister.Application.UseCases.Leads.Commands.DeleteLead;

public interface IDeleteLead
{
    Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}


