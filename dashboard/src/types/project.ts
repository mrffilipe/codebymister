import { ProjectType } from './proposal';

export enum ProjectStatus {
  NotStarted = 1,
  InProgress = 2,
  Testing = 3,
  Delivered = 4,
  Completed = 5,
  Cancelled = 6,
  OnHold = 7
}

export interface Project {
  id: string;
  leadId: string;
  leadName: string;
  projectType: ProjectType;
  closedValue: number;
  startDate: string;
  deadline?: string | null;
  status: ProjectStatus;
  entryPaymentReceived: boolean;
  entryPaymentValue?: number | null;
  entryPaymentDate?: string | null;
  finalPaymentReceived: boolean;
  finalPaymentValue?: number | null;
  finalPaymentDate?: string | null;
  scopeSummary: string;
  createdAt: string;
  updatedAt?: string | null;
}

export interface CreateProjectRequest {
  leadId: string;
  projectType: ProjectType;
  closedValue: number;
  startDate: string;
  scopeSummary: string;
  deadline?: string;
}

export interface UpdateProjectRequest {
  updateBasicInfo?: boolean;
  projectType?: ProjectType;
  closedValue?: number;
  startDate?: string;
  deadline?: string;
  scopeSummary?: string;
  updateStatus?: boolean;
  status?: ProjectStatus;
  markEntryPayment?: boolean;
  entryPaymentValue?: number;
  markFinalPayment?: boolean;
  finalPaymentValue?: number;
}
