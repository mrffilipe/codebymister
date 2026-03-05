using Codebymister.Application.UseCases.Outreach.Dtos;

namespace Codebymister.Application.UseCases.Outreach.Commands.CreateOutreach;

public interface ICreateOutreach
{
    Task<OutreachDto> ExecuteAsync(CreateOutreachRequest request, CancellationToken cancellationToken = default);
}
