import api from '../config/axios';
import type { Proposal, CreateProposalRequest, UpdateProposalRequest } from '../types/proposal';

export const proposalService = {
  getAll: async (): Promise<Proposal[]> => {
    const response = await api.get('/proposals');
    return response.data;
  },

  getById: async (id: string): Promise<Proposal> => {
    const response = await api.get(`/proposals/${id}`);
    return response.data;
  },

  create: async (data: CreateProposalRequest): Promise<Proposal> => {
    const response = await api.post('/proposals', data);
    return response.data;
  },

  update: async (id: string, data: UpdateProposalRequest): Promise<Proposal> => {
    const response = await api.put(`/proposals/${id}`, data);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/proposals/${id}`);
  }
};
