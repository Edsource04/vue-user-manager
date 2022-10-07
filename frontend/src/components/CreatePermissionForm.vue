<template>
  <form>
  <div class="mb-3" v-show="isEdit">
    <label for="permissionId" class="form-label">Id</label>
    <input type="text" class="form-control" id="permissionId" v-model.trim="permission.id" readonly>
  </div>
  <div class="mb-3">
    <label for="permissionEmployeeName" class="form-label">Employee Name</label>
    <input type="text" class="form-control" id="permissionEmployeeName" v-model.trim="permission.employeeName">
  </div>
  <div class="mb-3">
    <label for="permissionEmployeeName" class="form-label">Employee Last Name</label>
    <input type="text" class="form-control" id="permissionEmployeeName" v-model.trim="permission.employeeLastName">
  </div>
  <div class="mb-3">
    <label for="permissionType" class="form-label">Permission Type</label>
    <select class="form-select" aria-label="Default select example" id="permissionType" v-model="permission.permissionTypeId">
    <option selected value="0" v-if="permission.permissionTypeId == 0">Select an Item</option>
    <option v-for="permissionType in permissionTypes" :key="permissionType.id" :value="permissionType.id">{{permissionType.description}}</option>
 </select>
  </div>
  <div class="mb-3">
    <label for="permissionDate" class="form-label">Date</label>
    <input type="date" class="form-control" id="permissionDate" v-model="permission.date"/>
  </div>
  <button type="button" class="btn btn-primary" v-show="!isEdit" @click="submit">Submit</button>
  <button type="button" class="btn btn-primary" v-show="isEdit" @click="UpdatePermission">Update</button>
</form>
</template>

<script setup>
import swal from 'sweetalert2'
import { reactive, ref, onBeforeMount, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()
const emit = defineEmits(['submit', 'update'])

const permission = reactive({
            id: 0,
            employeeName: "",
            employeeLastName: "",
            permissionTypeId: 0,
            date:  new Date().toISOString().slice(0, 10)
})

const isEdit = ref(false)
const permissionTypes = ref([])


onBeforeMount(() => {

})
const CreatePermission = () => {
          if(permission.employeeName === "")
          {
            swal.fire({
                title: 'Invalid Request Employee Name cannot be empty',
                icon: "error",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
            return
          }

          if(permission.employeeLastName === "")
          {
            swal.fire({
                title: 'Invalid Request Employee Last Name cannot be empty',
                icon: "error",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
            return
          }

          if(permission.employeeLastName === "")
          {
            swal.fire({
                title: 'Invalid Request Employee Last Name cannot be empty',
                icon: "error",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
            return
          }

          if(permission.permissionTypeId === 0)
          {
            swal.fire({
                title: 'Invalid Request Permission Type cannot be empty',
                icon: "error",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
            return
          }

          emit('submit', permission, isEdit.value)

        }

      const UpdatePermission = event => {
            event.preventDefault()

            if(permission.employeeName === "")
          {
            swal.fire({
                title: 'Invalid Request Employee Name cannot be empty',
                icon: "error",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
            return
          }

          if(permission.employeeLastName === "")
          {
            swal.fire({
                title: 'Invalid Request Employee Last Name cannot be empty',
                icon: "error",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
            return
          }

          if(permission.permissionTypeId === 0)
          {
            swal.fire({
                title: 'Invalid Request Permission Type cannot be empty',
                icon: "error",
                showConfirmButton: false,
                showCloseButton: true,
                timer: 3000,
                timerProgressBar: true
                
            })
            return
          }

          emit('update', permission, isEdit.value)

        }

      const submit = event => {
            event.preventDefault()
            CreatePermission()
        }

    const getPermissionById = async () => {
            if (route.params.id === 0){
              const perTypeURL = `https://localhost:7031/PermissionType`
              const perTypeRes = await fetch(perTypeURL)
              const perTypeData = await perTypeRes.json()

              permissionTypes.value = perTypeData
              permission.permissionTypeId = 0
              return
            }

            const baseUrl = `https://localhost:7031/Permissions/${route.params.id}`
            const res = await fetch(baseUrl)
            const data = await res.json()
            permission.id = data.id
            permission.employeeName = data.employeeName
            permission.employeeLastName = data.employeeLastName
            permission.date = data.date.split('T')[0]
            permission.permissionTypeId = data.permissionType.id
            isEdit.value = route.query.isEdit === 'true'

            console.log(data.date)

        }

      onMounted(() => {
        if(route.params.id > 0 && route.query.isEdit === 'true'){
         getPermissionById()  
        } 
        else{
          const baseUrl = `https://localhost:7031/PermissionType`
          fetch(baseUrl)
             .then(res => res.json())
             .then(data => permissionTypes.value = data)
        }
      })
</script>

<style>

</style>