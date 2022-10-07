<script setup>
  import { defineProps } from 'vue';

  defineProps(['permissions'])
</script>

<template>
  <div id="ed-table">
    <table class="table">
  <thead>
    <tr>
      <th scope="col">#</th>
      <th scope="col">Employee Name</th>
      <th scope="col">Permission Type</th>
      <th scope="col">Date</th>
      <th scope="col">Actions</th>
    </tr>
  </thead>
  <tbody>
    <tr v-for=" permission in permissions" :key="permission.id">
      <th scope="row">{{permission.id}}</th>
      <td>{{permission.employeeName}} - {{permission.employeeLastName}}</td>
      <td>{{ permission.permissionType.description}} </td>
      <td>{{permission.date}}</td>
      <td>
        <button type="button" class="btn btn-info me-3" @click="$emit('edit', permission.id)" >Edit</button>
        <button type="button" class="btn btn-danger" @click="$emit('delete', permission.id)" >Delete</button>
      </td>
    </tr>
  </tbody>
</table>
  </div>
</template>



<style>

</style>