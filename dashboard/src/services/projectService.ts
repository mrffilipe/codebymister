import api from '../config/axios';
import type { Project, CreateProjectRequest, UpdateProjectRequest } from '../types/project';

export const projectService = {
  getAll: async (): Promise<Project[]> => {
    const response = await api.get('/projects');
    return response.data;
  },

  getById: async (id: string): Promise<Project> => {
    const response = await api.get(`/projects/${id}`);
    return response.data;
  },

  create: async (data: CreateProjectRequest): Promise<Project> => {
    const response = await api.post('/projects', data);
    return response.data;
  },

  update: async (id: string, data: UpdateProjectRequest): Promise<Project> => {
    const response = await api.put(`/projects/${id}`, data);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/projects/${id}`);
  }
};
