<template>
    <div>
        <h1>{{message}}</h1>
        <second-component />
    </div>
</template>

<script>
import SecondComponent from './SecondComponent.vue'
export default {
    name: 'first-component',
    components: {
        SecondComponent,
    },
    data() {
        return {
            message: 'Our first component',
        }
    }
}
</script>

<style scoped>
    h1 {
        color: blue;
    }
</style>