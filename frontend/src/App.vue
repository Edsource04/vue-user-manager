<script setup>
  import { RouterView } from 'vue-router';
  import EdNavBar from './components/EdNavBar.vue';
</script>
<template>
<ed-nav-bar />
<div class="container">
<router-view></router-view>
</div>
</template>
