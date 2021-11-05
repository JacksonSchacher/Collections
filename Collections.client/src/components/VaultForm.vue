<template>
  <form @submit.prevent="createVault()" class="keep-form">
    <div class="d-flex justify-content-between row">
      <div class="col-md-3">
        <h3>Create Vault</h3>
    <div class="form-group mt-4">
      <label for="title">Title</label>
      <input type="text" class="form-control" id="title" aria-describedby="vaultTitle" v-model="editable.name" placeholder="Vault Title.." required>
    </div>
    <div class="form-group">
      <label for="isPrivate" class="form-check-label">Make Private?</label>
      <input v-model="editable.isPrivate" type="checkbox" name="isPrivate" id="isPrivate" class="form-check-input">
       <small id="isPrivate" class="form-text f-10 text-muted">Private Collection Can Only Be Viewed By You</small>
     </div>
      </div>
      <div class="col-md-8">

    <div class="form-group text-center">
      <label for="description"></label>
      <textarea v-model="editable.description" name="description" id="description" cols="40" rows="8" placeholder="Vault Description.."></textarea>
    </div>
      </div>
    </div>
    <div class="d-flex justify-content-between form-group align-items-end">    
      <div class="form-group">
      <label for="img">Image URL</label>
      <input v-model="editable.img" type="url" class="form-control" id="img" aria-describedby="imgURL" placeholder="Image Url..">
    </div>
    <div>

      <button type="reset" class="btn btn-light me-2">Reset</button>
      <button type="submit" class="btn btn-primary">Create</button>
    </div>
    </div>
  </form>
</template>

<script>
import { ref, watchEffect } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Pop'
import { Vault } from '../models/Vault'
import { Modal } from 'bootstrap'
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'
export default {
  props: {
    vault: {
      type: Vault,
      default: () => new Vault()
    }
  },
  setup(props) {
    const route = useRoute()
    const editable = ref({})
    watchEffect(() => {
      editable.value = {...props.vault}
    })
    return{
      editable,
      async createVault() {
        try {
          if (editable.value.id) {
            await vaultsService.editVault(editable.value)
            Pop.toast('Edited Collection')
            return
          }
          await vaultsService.createVault(editable.value)
          await profilesService.getVaults(route.params.profileId)
          const modal = Modal.getInstance(document.getElementById('vault-modal'))
          modal.hide()
          editable.value = {}
          Pop.toast("Created Collection")
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