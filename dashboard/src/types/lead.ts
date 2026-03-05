export enum LeadPriority {
  Low = 1,
  Medium = 2,
  High = 3,
  VeryHigh = 4,
}

export enum LeadSource {
  GoogleSearch = 1,
  Instagram = 2,
  Facebook = 3,
  LinkedIn = 4,
  Referral = 5,
  ColdCall = 6,
  Event = 7,
  Other = 8,
}

export interface LeadDto {
  id: string;
  name: string;
  segment: string;
  city: string;
  website?: string | null;
  instagram?: string | null;
  phone?: string | null;
  problemDescription: string;
  priority: LeadPriority;
  source: LeadSource;
  createdAt: string;
  updatedAt?: string | null;
}

export interface CreateLeadRequest {
  name: string;
  segment: string;
  city: string;
  problemDescription: string;
  priority: LeadPriority;
  source: LeadSource;
  website?: string;
  instagram?: string;
  phone?: string;
}

export interface UpdateLeadRequest {
  name: string;
  segment: string;
  city: string;
  problemDescription: string;
  priority: LeadPriority;
  source: LeadSource;
  website?: string;
  instagram?: string;
  phone?: string;
}
