using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Projects.Commands.UpdateProject;

public record UpdateProjectRequest(
    bool UpdateBasicInfo = false,
    ProjectType? ProjectType = null,
    decimal? ClosedValue = null,
    DateTime? StartDate = null,
    DateTime? Deadline = null,
    string? ScopeSummary = null,
    bool UpdateStatus = false,
    ProjectStatus? Status = null,
    bool MarkEntryPayment = false,
    decimal? EntryPaymentValue = null,
    bool MarkFinalPayment = false,
    decimal? FinalPaymentValue = null
);


