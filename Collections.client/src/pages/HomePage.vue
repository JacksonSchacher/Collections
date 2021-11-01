<template>
  <div class="container-fluid">
    <div class="row">
      <div v-for="k in keeps" :key="k.id" class="col-3">
        <Keep :keep='k' />
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { watchEffect } from '@vue/runtime-core'
import { keepsService} from '../services/KeepsService'
import Pop from '../utils/Pop'
export default {
  name: 'Home',
  setup() {
    watchEffect(() => {
      try {
        keepsService.getKeeps()
      } catch (error) {
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card{
    width: 50vw;
    > img{
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
