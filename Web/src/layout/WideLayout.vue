<template>
    <div>
        {{title}}
        <slot></slot>
    </div>
</template>


<script>
export default {
    data: () => ({
        title: 'Wide page'
    })
}
</script>
