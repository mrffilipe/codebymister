using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Conversations.Commands.DeleteConversation;

public class DeleteConversation : IDeleteConversation
{
    private readonly IConversationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteConversation(IConversationRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var conversation = await _repository.GetByIdAsync(id, cancellationToken);
        if (conversation == null)
            return false;

        _repository.Delete(conversation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}



