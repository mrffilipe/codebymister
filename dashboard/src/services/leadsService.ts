import api from '../config/axios';
import type { LeadDto, CreateLeadRequest, UpdateLeadRequest } from '../types/lead';

export const leadsService = {
  getAll: () => api.get<LeadDto[]>('/leads'),
  
  getById: (id: string) => api.get<LeadDto>(`/leads/${id}`),
  
  create: (data: CreateLeadRequest) => api.post<LeadDto>('/leads', data),
  
  update: (id: string, data: UpdateLeadRequest) => 
    api.put<LeadDto>(`/leads/${id}`, data),
  
  delete: (id: string) => api.delete(`/leads/${id}`),
};
