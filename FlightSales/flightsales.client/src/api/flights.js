import { apiClient } from './client';

const base = '/api/Flight';

export const flightsApi = {
    getAll: () => apiClient.get(base),
    getById: (id) => apiClient.get(`${base}/${id}`),
    add: (dto) => apiClient.post(base, dto),
    update: (id, dto) => apiClient.put(`${base}/${id}`, dto),
    remove: (id) => apiClient.delete(`${base}/${id}`),
};
