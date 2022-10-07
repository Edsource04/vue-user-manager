import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import NotFoundView from '../views/NotFound.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/:pathMatch(.*)*',
      name: '404',
      component: NotFoundView
    },
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/permissionTypes',
      name: 'permission-types',
      component: () => import('../views/PermissionTypeView.vue')
    },
    {
      path: '/permissions',
      name: 'permissions',
      component: () => import('../views/PermissionView.vue')
    },
    {
      path: '/permissionTypes/:id',
      name: 'permission-type-id',
      component: () => import('../views/CreatePermissionTypeView.vue')
    },
    {
      path: '/permissions/:id',
      name: 'permission-id',
      component: () => import('../views/CreatePermissionView.vue')
    }
  ]
})

export default router
