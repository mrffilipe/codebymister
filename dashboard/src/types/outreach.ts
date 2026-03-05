export enum OutreachChannel {
  WhatsApp = 1,
  Instagram = 2,
  Email = 3,
  Phone = 4,
  LinkedIn = 5,
  Facebook = 6,
  InPerson = 7
}

export enum ResponseStatus {
  NoResponse = 1,
  Positive = 2,
  Neutral = 3,
  Negative = 4,
  NotInterested = 5
}

export interface Outreach {
  id: string;
  leadId: string;
  leadName: string;
  channel: OutreachChannel;
  message: string;
  sentAt: string;
  responded: boolean;
  responseAt?: string | null;
  responseStatus: ResponseStatus;
  followUpSent: boolean;
  notes?: string | null;
}

export interface CreateOutreachRequest {
  leadId: string;
  channel: OutreachChannel;
  message: string;
  notes?: string;
}

export interface UpdateOutreachRequest {
  responseAt?: string;
  responseStatus: ResponseStatus;
  followUpSent: boolean;
  notes?: string;
}
