export enum InterestLevel {
  Low = 1,
  Medium = 2,
  High = 3,
  VeryHigh = 4
}

export enum Timing {
  Immediate = 1,
  ThisWeek = 2,
  ThisMonth = 3,
  NextMonth = 4,
  ThreeMonths = 5,
  Undefined = 6
}

export enum ConversationStatus {
  Active = 1,
  WaitingResponse = 2,
  ProposalSent = 3,
  Closed = 4,
  Lost = 5
}

export interface Conversation {
  id: string;
  leadId: string;
  leadName: string;
  interestLevel: InterestLevel;
  timing: Timing;
  notes: string;
  nextStep?: string | null;
  status: ConversationStatus;
  createdAt: string;
  updatedAt?: string | null;
}

export interface CreateConversationRequest {
  leadId: string;
  interestLevel: InterestLevel;
  timing: Timing;
  notes: string;
  nextStep?: string;
}

export interface UpdateConversationRequest {
  interestLevel: InterestLevel;
  timing: Timing;
  notes: string;
  nextStep?: string;
  status: ConversationStatus;
}
