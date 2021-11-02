<template>
  <div class="container-fluid py-1 px-4">
    <div v-if="keeps" class="masonry-columns text-center">
      <div v-for="k in keeps" :key="k.id" class="">
        <Keep :keep="k" />
      </div>
    </div>
    <div v-else class="row">
      <h1>Loading...</h1>
    </div>
  </div> 
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted, watchEffect } from '@vue/runtime-core'
import { keepsService} from '../services/KeepsService'
import Pop from '../utils/Pop'
import Modal from '../components/Modal.vue'
export default {
  components: { Modal },
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
.masonry-columns {
  columns: 4 200px;
  column-gap: 1.5rem;
  flex-flow: column;
  div {
    display: inline-block;
    max-width: 30rem;
    border-radius: 15px;
    margin: .5rem 0;
    z-index: 20;
  } 
}

// .masonry-columns {
//   columns: 6 200px;
//   column-gap: 1rem;
//   div {
//     width: 150px;
//     background: #EC985A;
//     color: white;
//     margin: 0 1rem 1rem 0;
//     display: inline-block;
//     width: 100%;
//     text-align: center;
//     font-family: system-ui;
//     font-weight: 900;
//     font-size: 2rem;
//   } 
//   @for $i from 1 through 36 { 
//     div:nth-child(#{$i}) {
//       $h: (random(400) + 100) + px;
//       height: $h;
//       line-height: $h;
//     }
//   }
// }



</style>
