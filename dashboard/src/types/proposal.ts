export enum ProjectType {
  Website = 1,
  Ecommerce = 2,
  LandingPage = 3,
  System = 4,
  Mobile = 5,
  Integration = 6,
  Consulting = 7
}

export enum ProposalStatus {
  Sent = 1,
  UnderReview = 2,
  Accepted = 3,
  Refused = 4,
  Expired = 5
}

export interface Proposal {
  id: string;
  leadId: string;
  leadName: string;
  projectType: ProjectType;
  proposedValue: number;
  sentAt: string;
  status: ProposalStatus;
  refusalReason?: string | null;
  responseAt?: string | null;
  notes?: string | null;
}

export interface CreateProposalRequest {
  leadId: string;
  projectType: ProjectType;
  proposedValue: number;
  notes?: string;
}

export enum ProposalAction {
  MarkAsUnderReview = 0,
  Accept = 1,
  Refuse = 2,
  MarkAsExpired = 3,
  UpdateNotes = 4,
}

export interface UpdateProposalRequest {
  action: ProposalAction;
  refusalReason?: string;
  notes?: string;
}
