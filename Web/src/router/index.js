import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home.vue'
import Login from '@/views/Login.vue'
import User from '@/views/User'
import ManagePriority from '@/views/ManagePriority'
import ManageProject from '@/views/ManageProject'
import Profile from '@/components/User/Profile'
import TaskList from '@/views/TaskList'

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
        path: '/login',
        name: 'login',
        component: Login
    },
    {
      path: '/ManagePriority',
      name: 'ManagePriority',
      component: ManagePriority
    },
    {
      path: '/ManageProject',
      name: 'ManageProject',
      component: ManageProject
    },
    {
        path: '/user/:mode',
        name: 'user',
        component: User
    },
    {
        path: '/tasklist',
        name: 'TaskList',
        component: TaskList
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
