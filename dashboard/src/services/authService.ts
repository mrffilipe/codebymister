import api from '../config/axios';
import type { AuthSession } from '../types/auth';

export const authService = {
  exchangeToken: async (firebaseIdToken: string): Promise<AuthSession> => {
    const response = await api.post<AuthSession>('/auth/exchange-token', {
      firebaseIdToken,
    });

    return response.data;
  },

  refresh: async (): Promise<AuthSession> => {
    const response = await api.post<AuthSession>('/auth/refresh');
    return response.data;
  },
};
