import api from '../config/axios';
import type { Outreach, CreateOutreachRequest, UpdateOutreachRequest } from '../types/outreach';

export const outreachService = {
  getAll: async (): Promise<Outreach[]> => {
    const response = await api.get('/outreaches');
    return response.data;
  },

  getById: async (id: string): Promise<Outreach> => {
    const response = await api.get(`/outreaches/${id}`);
    return response.data;
  },

  create: async (data: CreateOutreachRequest): Promise<Outreach> => {
    const response = await api.post('/outreaches', data);
    return response.data;
  },

  update: async (id: string, data: UpdateOutreachRequest): Promise<Outreach> => {
    const response = await api.put(`/outreaches/${id}`, data);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/outreaches/${id}`);
  }
};
