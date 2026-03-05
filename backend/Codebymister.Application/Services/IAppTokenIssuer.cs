using Codebymister.Application.Common;

namespace Codebymister.Application.Services;

public interface IAppTokenIssuer
{
    string Issue(AppTokenClaims claims);
}
