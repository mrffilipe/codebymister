using Codebymister.Application.UseCases.Outreach.Dtos;

namespace Codebymister.Application.UseCases.Outreach.Commands.UpdateOutreach;

public interface IUpdateOutreach
{
    Task<OutreachDto?> ExecuteAsync(Guid id, UpdateOutreachRequest request, CancellationToken cancellationToken = default);
}
