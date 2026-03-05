import api from '../config/axios';
import type { Maintenance, CreateMaintenanceRequest, UpdateMaintenanceRequest } from '../types/maintenance';

export const maintenanceService = {
  getAll: async (): Promise<Maintenance[]> => {
    const response = await api.get('/maintenances');
    return response.data;
  },

  getById: async (id: string): Promise<Maintenance> => {
    const response = await api.get(`/maintenances/${id}`);
    return response.data;
  },

  create: async (data: CreateMaintenanceRequest): Promise<Maintenance> => {
    const response = await api.post('/maintenances', data);
    return response.data;
  },

  update: async (id: string, data: UpdateMaintenanceRequest): Promise<Maintenance> => {
    const response = await api.put(`/maintenances/${id}`, data);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/maintenances/${id}`);
  }
};
