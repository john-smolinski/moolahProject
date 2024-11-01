import { createRouter, createWebHistory } from 'vue-router';
import Providers from '../components/Providers.vue';
import ToDos from '../components/ToDos.vue';

const routes = [
  { path: '/providers', component: Providers },
  { path: '/todos', component: ToDos },
];

const router = createRouter({
  history: createWebHistory(), // Use createWebHistory() instead of mode: 'history'
  routes,
});

export default router;
