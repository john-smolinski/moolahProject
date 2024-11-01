import apiClient from '../axios';

export default {
  searchToDos(searchParams) {
    const query = new URLSearchParams(searchParams).toString();
    return apiClient.get(`/todos/search?${query}`);
  },
  getAllToDos() {
    return apiClient.get('/todos');
  },
  getToDoById(id) {
    return apiClient.get(`/todos/${id}`);
  },
  createToDo(toDo) {
    return apiClient.post('/todos', toDo);
  },
  updateToDo(id, toDo) {
    return apiClient.put(`/todos/${id}`, toDo);
  },
  deleteToDo(id, provider) {
    return apiClient.delete(`/todos/${id}`, {
      params: { provider }
    });
  },
};
