import api from '../config/axios';
import type { Conversation, CreateConversationRequest, UpdateConversationRequest } from '../types/conversation';

export const conversationService = {
  getAll: async (): Promise<Conversation[]> => {
    const response = await api.get('/conversations');
    return response.data;
  },

  getById: async (id: string): Promise<Conversation> => {
    const response = await api.get(`/conversations/${id}`);
    return response.data;
  },

  create: async (data: CreateConversationRequest): Promise<Conversation> => {
    const response = await api.post('/conversations', data);
    return response.data;
  },

  update: async (id: string, data: UpdateConversationRequest): Promise<Conversation> => {
    const response = await api.put(`/conversations/${id}`, data);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/conversations/${id}`);
  }
};
