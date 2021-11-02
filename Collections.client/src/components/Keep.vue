<template>
  <div :id="'keep-' + keep.id" class="card keep-card text-white" @click="keepDetails(keep)">
  <div v-if="!isOpen">
    <img :src="keep.img" loading="lazy" class="card-img" alt="..." >
    <div class="card-img-overlay">
      <h5 class="card-title">{{keep.name}}</h5>
      <p class="card-text">{{keep.description}}</p>
      <!-- <p class="card-text">{{keep.createdAt}}</p> -->
    </div>
    <div v-if="keep.creator" class="align-self-end">
      <img class="creator-picture" :src="keep.creator.picture" :alt="keep.creator.name" @click="goToProfile(keep.creator.id)">
    </div>
  </div>
    <div v-else class="text-dark details">
      <KeepDetails />
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
import { ref } from '@vue/reactivity'
import { Keep } from '../models/Keep'
import { router } from '../router'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { watchEffect } from '@vue/runtime-core'
import { AppState } from '../AppState'
export default {
  props: {
    keep: {
      type: Keep,
      default: () => { return new Keep()}
    }
  },
  setup() {
    let isOpen = ref(false)
    let scrollPos = ref(0)
    watchEffect (() => {
      window.onscroll = function (e) {
        var vertical_position = 0;
        if (scrollY) {
          vertical_position = scrollY
        } else if (document.documentElement.clientHeight) {
          vertical_position = document.documentElement.scrollTop
        } else if (document.body) {
          vertical_position = document.body.scrollTop
        }
        var keepCard = document.getElementById(`keep-${AppState.currentKeep.id}`)
        keepCard.style.top = (vertical_position) + 'px';
      }
    })
    return {
      isOpen,
      scrollPos,
      async keepDetails(keep) {
        try {
          this.getScrollPos()
          isOpen.value = !isOpen.value
          logger.log('Open Status: ', isOpen)
          var keepCard = document.getElementById(`keep-${keep.id}`)
          keepCard.classList.toggle('focused-card')
          await keepsService.setCurrentKeep(keep)
        } catch (error) {
          Pop.toast(error.message, 'error')
        }
      },
      getScrollPos(){
        try {
          scrollPos.value = document.documentElement.scrollTop || document.body.scrollTop
          logger.log("Scroll Position", scrollPos)
          return
        } catch (error) {
          Pop.toast(error.message, 'error')
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
  transition: all .5s cubic-bezier(0.445, 0.05, 0.55, 0.95);
}
.keep-card {
  transform: rotateY(0deg);
}
.focused-card {
  margin: auto !important;
  justify-content: center;
  align-items: center;
  background-color: #97979767;
  transform: rotateY(180deg) translateZ(10px);
  height: 100vh;
  width: 100vw;
  max-width: 100vw !important;
  position: absolute;
  left: 0;
  right: 0;
  top: v-bind(scrollPos) + 'px';
  z-index: 100;
  backdrop-filter: blur(2px);
}
.details {
  transform: rotateY(180deg) translateZ(2px);
}
</style>

