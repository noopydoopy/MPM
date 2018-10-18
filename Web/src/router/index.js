import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/views/Login.vue'
import User from '@/views/User'
import ManagePriority from '@/views/ManagePriority'
import ManageProject from '@/views/ManageProject'
import TaskList from '@/views/TaskList'
import ManageUser from '@/views/ManageUser'
import ManageType from '@/views/ManageType'
import Dashboard from '@/views/Dashboard'
import AccessDeniedPage from '@/views/AccessDeniedPage'
import store from '@/store/index'

Vue.use(Router)

const authMiddleware = (to, from, next) => {
  let isAdmin = store.getters['authenticationModule/userIsAdmin'];
  if(isAdmin)
    return next()
  else
    return next('/forbidden');
}

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'dashboard',
      component: Dashboard
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/admin/managepriority',
      name: 'ManagePriority',
      component: ManagePriority,
      beforeEnter: authMiddleware
    },
    {
      path: '/manageproject/:projectId',
      name: 'ManageProject',
      component: ManageProject
    },
    {
      path: '/user/:mode',
      name: 'user',
      component: User
    },
    {
      path: '/tasklist/:projectId',
      name: 'TaskList',
      component: TaskList
    },
    {
      path: '/admin/manageuser/:id',
      name: 'manageuser',
      component: ManageUser,
      beforeEnter: authMiddleware
    },
    {
      path: '/admin/managetype',
      name: 'managetype',
      component: ManageType,
      beforeEnter: authMiddleware
    },
    {
      path:'/forbidden',
      name:'AccessDenied',
      component: AccessDeniedPage
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ '@/views/About.vue')
    }
  ]
})
