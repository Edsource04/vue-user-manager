<template>
  <h2>Create Permission Type</h2>
  <div class="container">
    <create-permission-type-form-vue @submit="postPermissionType" @update="putPermissionType"/>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router'

import CreatePermissionTypeFormVue
 from '../components/CreatePermissionTypeForm.vue';
 
 const route = useRoute()
 const router = useRouter()

 const isEdit = ref(false)

 onMounted(() => {
        isEdit.value = route.query.isEdit
    })

    const postPermissionType = async (permissionType, isEdit) => {
            const baseUrl = `https://localhost:7031/PermissionType`
            const res = await fetch(baseUrl, {    
           // Adding method type
            method: 'POST',
      
          // Adding body or contents to send
          body: JSON.stringify({
                 id: 0,
                 description: permissionType.description
           }),
      
          // Adding headers to the request
          headers: {
         "Content-type": "application/json; charset=UTF-8"
         }
          }) 
           router.push({
            path: "/permissionTypes",
            query: {
                isCreated: true
            }
          })
        }

        const putPermissionType = async (permissionType, isEdit) => {
            const baseUrl = `https://localhost:7031/PermissionType/${permissionType.id}`
            const res = await fetch(baseUrl, {    
           // Adding method type
            method: 'PUT',
      
          // Adding body or contents to send
          body: JSON.stringify({
                 id: permissionType.id,
                 description: permissionType.description
           }),
      
          // Adding headers to the request
          headers: {
         "Content-type": "application/json; charset=UTF-8"
         }
          }) 

          router.push({
            path: '/permissionTypes',
            query: {
                isUpdated: isEdit
            }
          })
          
        }
</script>

<style>

</style>