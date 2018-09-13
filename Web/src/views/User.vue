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

export default {
    components: {
        Profile,
        UserList
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
    }
}
</script>
