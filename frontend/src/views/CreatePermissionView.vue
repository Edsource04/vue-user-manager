<template>
  <h2>Create Permission Type</h2>
  <div class="container">
    <create-permission-form @submit="postPermission" @update="putPermission"/>
  </div>
</template>

<script setup>
import { ref, onBeforeMount, onMounted} from 'vue'
import { useRoute, useRouter } from 'vue-router'
import CreatePermissionForm from '../components/CreatePermissionForm.vue'

const isEdit = ref(false)
const route = useRoute()
const router = useRouter()

    onMounted(() => {
        isEdit.value = route.query.isEdit
    })

    const postPermission = async (permission, isEdit) => {
            const baseUrl = `https://localhost:7031/Permissions`
            const res = await fetch(baseUrl, {    
           // Adding method type
            method: 'POST',
      
          // Adding body or contents to send
          body: JSON.stringify({
                 id: 0,
                 employeeName: permission.employeeName,
                 employeeLastName: permission.employeeLastName,
                 permissionTypeId: permission.permissionTypeId,
                 date: permission.date
           }),
      
          // Adding headers to the request
          headers: {
         "Content-type": "application/json; charset=UTF-8"
         }
          }) 
          
          if (res.status === 200){
            router.push({
            path: "/permissions",
            query: {
                isCreated: true
            }
          })
          }

        }

      const putPermission = async (permission, isEdit) => {
            const baseUrl = `https://localhost:7031/Permissions/${permission.id}`
            const res = await fetch(baseUrl, {    
           // Adding method type
            method: 'PUT',
      
          // Adding body or contents to send
          body: JSON.stringify({
                 id: permission.id,
                 employeeName: permission.employeeName,
                 employeeLastName: permission.employeeLastName,
                 permissionType: {id: permission.permissionTypeId, description: ''},
                 date: permission.date
           }),
      
          // Adding headers to the request
          headers: {
         "Content-type": "application/json; charset=UTF-8"
         }
          }) 
          if (res.status === 204) {
            this.$router.push({
            path: '/permissions',
            query: {
                isUpdated: isEdit
            }
            })
          }
          
        }
</script>

<style>

</style>