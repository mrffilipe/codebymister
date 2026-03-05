import api from '../config/axios';
import type { DashboardDto } from '../types/dashboard';

export const dashboardService = {
  getData: () => api.get<DashboardDto>('/dashboard'),
};
