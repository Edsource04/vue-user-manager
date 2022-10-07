<template>
  <h2>Permissions</h2>
  <hr />
  <ed-spinner v-show="loading"/>
  <button type="button" class="btn btn-default" @click="goToCreatePage">Create</button>
  <ed-permission-list :permissionTypes="permissionTypes" @delete="deletePermissionType" @edit="edit" v-bind:permissions="permissions"/>
  </template>

<script setup>
import EdPermissionList from '../components/EdPermissionList.vue'
import { ref, onMounted, onBeforeMount } from 'vue'
import { useRoute, useRouter } from 'vue-router'

import EdSpinner from '../components/EdSpinner.vue'
import swal from 'sweetalert2'

const route = useRoute()
const router = useRouter()

    const permissions = ref([])
    const loading = ref(true)
    const editPermission = ref(false)
    const permissionTypes = ref([])


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
            path: `/permissions/${id}`,
            query: {
                isEdit: true
            }
        })
    }

    const getPermissions = async () => {

        loading.value = true
        const baseUrl = 'https://localhost:7031/Permissions'
        const res = await fetch(baseUrl)
        const data = await res.json()
        permissions.value = data
        loading.value = false

        return data
 
    }
    onBeforeMount(() => {
        getPermissions()
    })

    const getPermissionTypes = async () => {
        loading.value = true
        const baseUrl = 'https://localhost:7031/PermissionType'
        const res = await fetch(baseUrl)
        const data = await res.json()
        permissionTypes.value = data
    }

    const goToCreatePage = () => {
            router.push({
            path: `/permissions/0`,
            query: {
                isEdit: false
            }
        })
    }
    
    const deletePermissionType = id => {

        swal.fire({
            title: 'Do you want to delete the permission?',
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: 'Yes',
            denyButtonText: `Dont't delete`,
        }).then((result) => {
            if(result.isConfirmed) {
                const baseUrl = `https://localhost:7031/Permissions/${id}`
                fetch(baseUrl, {
                    method: 'DELETE',
                    headers: {
                "Content-type": "application/json; charset=UTF-8"
                }
           
                }).then(res => console.log(res.json()))
                .then(data => {
                    swal.fire({
                        title: `Permission Deleted`,
                        icon: "success",
                        showConfirmButton: false,
                        showCloseButton: true,
                        timer: 3000,
                        timerProgressBar: true
                        
                    })
                    permissions.value = []
                    getPermissions()
                    delete route.query
                })
                 swal.fire('Deleted!', '', 'success')
                    } else if(result.isDenied){
                        swal.fire('Record not deleted', '', 'info')
                    }
                })

        
            }
</script>