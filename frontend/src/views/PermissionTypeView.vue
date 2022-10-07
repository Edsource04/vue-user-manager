<template>
  <h2>Permission Types</h2>
  <hr />
  <ed-spinner v-show="loading"/>
  <button type="button" class="btn btn-default" @click="goToCreatePage">Create</button>
  <ed-permission-type-list :permissionTypes="permissionTypes" @delete="deletePermissionType" @edit="edit"/>
  </template>

<script setup>
import { ref, onBeforeMount, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'

import EdSpinner from '../components/EdSpinner.vue'
import EdPermissionTypeList from '../components/EdPermissionTypeList.vue'
import swal from 'sweetalert2'

const router = useRouter()
const route = useRoute()

    const permissionTypes = ref([])
    const loading = ref(true)
    const editPermissionType = ref(false)

 onBeforeMount(() => {
    getPermissionTypes()   
 })

 onMounted(() => {
    if(route.query.isCreated){
        swal.fire({
                title: `Permission type created`,
                icon: "success",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
    }

    if(route.query.isUpdated){
        swal.fire({
                title: `Permission type Updated`,
                icon: "success",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
    }
 })

    const edit = id => {
         router.push({
            path: `/permissionTypes/${id}`,
            query: {
                isEdit: true
            }
        })
    }

    const getPermissionTypes = async () => {
        loading.value = true
        const baseUrl = 'https://localhost:7031/permissiontype'
        const res = await fetch(baseUrl)
        const data = await res.json()
        permissionTypes.value = data
        loading.value = false
    }

    const goToCreatePage = () => {
        router.push({
            path: `/permissionTypes/0`,
            query: {
                isEdit: false
            }
        })
    }

    const deletePermissionType = id => {

        swal.fire({
        title: 'Do you want to delete the permission type?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Yes',
        denyButtonText: `Don't delete`,
        }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            const baseUrl = `https://localhost:7031/permissiontype/${id}`
        fetch(baseUrl, {
            method: 'DELETE',
            headers: {
         "Content-type": "application/json; charset=UTF-8"
        }
           
        }).then(res => console.log(res.json()))
          .then(data => {
            swal.fire({
                title: `Permission Type Deleted`,
                icon: "success",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
            permissionTypes.value = []
            getPermissionTypes()
            delete route.query
          })

            swal.fire('Deleted!', '', 'success')
        } else if (result.isDenied) {
            swal.fire('Record not deleted', '', 'info')
        }
        })
    }
</script>