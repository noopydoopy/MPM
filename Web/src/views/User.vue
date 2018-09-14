<template>
    <div>
        <nav-layout>
        
        <component :is="component"></component>
        </nav-layout>
    </div>
</template>

<script>

import Profile from '@/components/User/Profile'
import UserList from '@/components/User/UserList'
import UserDetailUpdate from '@/components/User/UserDetailUpdate'

export default {
    components: {
        Profile,
        UserList,
        UserDetailUpdate
    },
    data: () => ({
        component: 'profile'
    }),
    mounted() {
        console.log(this.$route.params.mode)
        if(this.$route.params.mode === 'profile'){
            this.component = 'profile'
        }
        else if(this.$route.params.mode === 'list'){
            this.component = 'user-list'
        }
        else if(this.$route.params.mode === 'update'){
     /*       this.$http.get('/api/Users/' + this.$route.query).then(response => {
                console.log(response)
            }, error => {
                console.log(error)
            })*/
            this.component = 'user-detail-update'
        }
    }
}
</script>
