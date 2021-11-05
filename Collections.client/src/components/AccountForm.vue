<template>
  <form @submit.prevent="editAccount()" class="keep-form">
    <div class="d-flex justify-content-between row">
      <div class="col-md-3">
        <h3>Edit Account</h3>
    <div class="form-group mt-4">
      <label for="name">Name</label>
      <input v-model="editable.name" type="text" class="form-control" id="name" aria-describedby="accountName" placeholder="Account Name.." required>
    </div>
      </div>
    </div>
    <div class="d-flex justify-content-between form-group align-items-end">    
      <div class="form-group">
      <label for="img">Image URL</label>
      <input v-model="editable.picture" type="url" class="form-control" id="img" aria-describedby="imgURL" placeholder="Image Url..">
    </div>
    <div>

      <button type="reset" class="btn btn-light me-2">Reset</button>
      <button type="submit" class="btn btn-primary">Edit</button>
    </div>
    </div>
  </form>
</template>

<script>
import { ref } from '@vue/reactivity'
import { watchEffect } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService'
import { Modal } from 'bootstrap'
export default {
  setup() {
    const editable = ref({})
    watchEffect(() => {
      editable.value = {...AppState.account}
    })
    return {
      editable,
      async editAccount() {
        try {
          accountService.editAccount(editable.value)
          const modal = Modal.getOrCreateInstance(document.getElementById('account-modal'))
          modal.hide()
          Pop.toast('Account Edited')
        } catch (error) {
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.form-control[id='title'] {
  width: 20vw;
}
.form-control[id='img'] {
  width: 40vw;
}
.keep-form {
  padding: 1rem 3rem;
}
.btn {
  height: 3rem;
}
</style>