namespace Codebymister.Application.UseCases.Conversations.Commands.DeleteConversation;

public interface IDeleteConversation
{
    Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
