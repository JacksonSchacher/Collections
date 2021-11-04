<template>
  <form @submit.prevent="submitKeep()" class="keep-form">
    <div class="d-flex justify-content-between row">
      <div class="col-3">
        <h3>Create Post</h3>
    <div class="form-group mt-4">
      <label for="title">Title</label>
      <input type="text" v-model="editable.name" class="form-control" id="title" aria-describedby="postTitle" placeholder="Post Title.." required>
    </div>
      </div>
      <div class="col-8">

    <div class="form-group text-center">
      <label for="description"></label>
      <textarea name="description" v-model="editable.description" id="description" cols="40" rows="8" placeholder="Post Description.."></textarea>
    </div>
      </div>
    </div>
    <div class="d-flex justify-content-between form-group align-items-end">    
      <div class="form-group">
      <label for="img">Image URL</label>
      <input type="url" class="form-control" v-model="editable.img" id="img" aria-describedby="imgURL" placeholder="Image Url.." required>
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
import { Keep } from '../models/Keep'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'
import { Modal } from 'bootstrap'
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'
export default {
  props: {
    keep: {
      type: Keep,
      default: () => new Keep()
    }
  },
  setup(props) {
    const route = useRoute()
    const editable = ref({})
    watchEffect(() => {
      editable.value = {...props.keep}
    })
    return {
      editable,
      async submitKeep() {
        try {
          if (editable.value.id) {
            await keepsService.editKeep(editable.value)
            Pop.toast('Edited Post')
            return
          }
          await keepsService.createKeep(editable.value)
          await profilesService.getKeeps(route.params.profileId)
          const modal = Modal.getInstance(document.getElementById('keep-modal'))
          modal.hide()
          editable.value = {}
          Pop.toast('Created Post')
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