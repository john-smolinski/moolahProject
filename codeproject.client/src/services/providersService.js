import apiClient from '../axios';

export default {
  getAllProviders() {
    return apiClient.get('/providers');
  },
};
