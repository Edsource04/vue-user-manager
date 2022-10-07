<template>
  <div id="ed-table">
    <table class="table">
  <thead>
    <tr>
      <th scope="col">#</th>
      <th scope="col">Description</th>
      <th scope="col">Actions</th>
    </tr>
  </thead>
  <tbody>
    <tr v-for="(perType) in permissionTypes" :key="perType.id">
      <th scope="row">{{perType.id}}</th>
      <td>{{perType.description}}</td>
      <td>
        <button type="button" class="btn btn-info me-3" @click="$emit('edit', perType.id)" >Edit</button>
        <button type="button" class="btn btn-danger" @click="$emit('delete', perType.id)" >Delete</button>
      </td>
    </tr>
  </tbody>
</table>
  </div>
</template>

<script setup>
  import { defineProps } from 'vue';

const props = defineProps({
  permissionTypes: {
    type: Array,
    required: true
  }
})
</script>

<style>

</style>