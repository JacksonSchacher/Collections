<template>
  <div class="card keep-card text-white">
  <img :src="keep.img" loading="lazy" class="card-img selectable" alt="...">
  <div class="card-img-overlay">
    <h5 class="card-title">{{keep.name}}</h5>
    <p class="card-text">{{keep.description}}</p>
    <!-- <p class="card-text">{{keep.createdAt}}</p> -->
  </div>
    <div v-if="keep.creator" class="align-self-end">
      <img class="creator-picture" :src="keep.creator.picture" :alt="keep.creator.name" @click="goToProfile(keep.creator.id)">
    </div>
</div>

  <!-- Keep Modal -->
<!-- <Modal id="keep-details-modal">
  <template #modal-body>
    <KeepDetails :keep="keep" />
  </template>
</Modal> -->

</template>

<script>
import { Keep } from '../models/Keep'
import { router } from '../router'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'
import Pop from '../utils/Pop'
export default {
  props: {
    keep: {
      type: Keep,
      default: () => { return new Keep()}
    }
  },
  setup() {
    return {
      setKeepDetails(keep) {
        try {
          keepsService.setCurrentKeep(keep)
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
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.creator-picture {
  width: 50px;
  object-fit: cover;
  object-position: center;
  position: absolute;
  bottom: 10px;
  right: 10px;
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
</style>

