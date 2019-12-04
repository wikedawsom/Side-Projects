<template>
    <div>
        <h1>{{message}}</h1>
    </div>
</template>

<script>
export default {
    name: 'second-component',
    data() {
        return {
            message: 'Our second component',
        }
    }
}
</script>

<style scoped>
    h1 {
        color: red;
    }
</style>