namespace Codebymister.Application.UseCases.Proposals.Commands.DeleteProposal;

public interface IDeleteProposal
{
    Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
