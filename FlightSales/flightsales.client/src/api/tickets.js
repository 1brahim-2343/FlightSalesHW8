import { apiClient } from './client';

const base = '/api/FlightTicket';

export const ticketsApi = {
    getById: (id) => apiClient.get(`${base}/${id}`),
    add: (dto) => apiClient.post(base, dto),
    remove: (id) => apiClient.delete(`${base}/${id}`),
};
