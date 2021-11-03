<template>
  <div class="card keep-card text-white" data-bs-toggle="modal" data-bs-target="#keep-details-modal" @click="setCurrentKeep(keep.id)">
  <div>
    <img :src="keep.img" loading="lazy" class="card-img" alt="..." >
    <div class="card-img-overlay">
      <h5 class="card-title">{{keep.name}}</h5>
      <!-- <p class="card-text">{{keep.createdAt}}</p> -->
    </div>
    <div v-if="keep.creator" class="align-self-end">
      <img class="creator-picture" :src="keep.creator.picture" :alt="keep.creator.name" @click="goToProfile(keep.creator.id)">
    </div>
  </div>
</div>

  <!-- Keep Modal -->
<Modal id="keep-details-modal">
  <template #modal-body>
    <KeepDetails />
  </template>
  <template #modal-footer>
    <i v-if="keep.creator.id == account.id" class="mdi mdi-delete f-18" @click="deleteKeep(keep.id)"></i>
  </template>
</Modal>

</template>

<script>
import { computed } from '@vue/reactivity'
import { Keep } from '../models/Keep'
import { router } from '../router'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
export default {
  props: {
    keep: {
      type: Keep,
      default: () => { return new Keep()}
    }
  },
  setup() {
    return {
      account: computed(() => AppState.account),
      currentKeep: computed(() => AppState.currentKeep),
      async setCurrentKeep(keepId) {
        try {
          await keepsService.setCurrentKeep(keepId)
        } catch (error) {
          Pop.toast(error.Message, 'error')
        }
      },
      async goToProfile(creatorId) {
        try {
          console.log('Profile ID', creatorId)
          await profilesService.setCurrentProfile(creatorId)
          router.push({ name: 'Profile', params: { profileId: creatorId}})
        } catch (error) {
          Pop.toast(error.Message, 'error')
        }
      },
      async deleteKeep(keepId) {
        try {
          if (await Pop.confirm("Delete Post")) {
            const modal = Modal.getInstance(document.getElementById('keep-details-modal'))
            modal.hide()
            await keepsService.deleteKeep(keepId)
            Pop.toast("Post Deleted")
          }
        } catch (error) {
          
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.creator-picture {
  width: 50px;
  height: 50px;
  object-fit: cover;
  object-position: center;
  position: absolute;
  bottom: 10px;
  right: 10px;
}
.card-title {
  background-color: #8d8d8d7c;
  border-radius: 15px;
  padding: .5rem;
}
.card-img-overlay {
  text-align: initial;
}
.modal-img {
  width: 100%;
  height: 50vh;
  object-fit: cover;
  object-position: center;
}
img {
  border-radius: 15px;
}
.card {
  border-radius: 15px;
  filter: drop-shadow(1px 1px 15px #8d8d8d);
}
.details {
  transform: rotateY(180deg) translateZ(2px);
}
</style>

