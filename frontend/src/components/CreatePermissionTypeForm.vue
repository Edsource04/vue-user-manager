<template>
  <form>
  <div class="mb-3" v-show="isEdit">
    <label for="permissionTypeId" class="form-label">Id</label>
    <input type="text" class="form-control" id="permissionTypeId" v-model.trim="permissionType.id" readonly>
  </div>
  <div class="mb-3">
    <label for="permissionTypeDescription" class="form-label">Description</label>
    <input type="text" class="form-control" id="permissionTypeDescription" v-model.trim="permissionType.description">
  </div>
  <button type="button" class="btn btn-primary" v-show="!isEdit" @click="submit">Submit</button>
  <button type="button" class="btn btn-primary" v-show="isEdit" @click="UpdatePermissionType">Update</button>
</form>
</template>

<script setup>
import { reactive, ref, onBeforeMount, defineEmits} from 'vue'
import { useRoute } from 'vue-router';

import swal from 'sweetalert2'

const route = useRoute()
const emit = defineEmits(['submit', 'update'])
const permissionType = reactive({
             id: 0,
            description: ""
})

const isEdit = ref(false)

const CreatePermissionType = () => {
          if(permissionType.description === "")
          {
            swal.fire({
                title: 'Invalid Request Description cannot be empty',
                icon: "error",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
            return
          }

          emit('submit', permissionType, isEdit.value)

        }
       const UpdatePermissionType = event => {
            event.preventDefault()

          if(permissionType.description === "")
          {
            swal.fire({
                title: 'Invalid Request Description cannot be empty',
                icon: "error",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
            return
          }

          emit('update', permissionType, isEdit.value)

        }

       const submit = event => {
            event.preventDefault()
            CreatePermissionType()
        }

        const getPermissionTypeById = async () => {
            const baseUrl = `https://localhost:7031/permissiontype/${route.params.id}`
            const res = await fetch(baseUrl)
            const data = await res.json()
            permissionType.id = data.id
            permissionType.description = data.description
            isEdit.value = this.$route.query.isEdit === 'true'
        }
  onBeforeMount(() => {
        if(route.params.id > 0 && route.query.isEdit === 'true'){
         this.getPermissionTypeById()  
        } })

</script>

<style>

</style>