export enum MaintenanceStatus {
  Active = 1,
  Suspended = 2,
  Cancelled = 3,
  Expired = 4
}

export interface Maintenance {
  id: string;
  projectId: string;
  projectName: string;
  monthlyValue: number;
  startDate: string;
  status: MaintenanceStatus;
  nextBillingDate: string;
  hostingIncluded: boolean;
  notes?: string | null;
  createdAt: string;
  updatedAt?: string | null;
}

export interface CreateMaintenanceRequest {
  projectId: string;
  monthlyValue: number;
  startDate: string;
  hostingIncluded: boolean;
  notes?: string;
}

export interface UpdateMaintenanceRequest {
  updateBasicInfo?: boolean;
  monthlyValue?: number;
  hostingIncluded?: boolean;
  notes?: string;
  updateStatus?: boolean;
  status?: MaintenanceStatus;
  updateNextBillingDate?: boolean;
  nextBillingDate?: string;
  processBilling?: boolean;
}
