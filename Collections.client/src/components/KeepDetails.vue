<template>
<div v-if="keep.id" @click.stop class="container">
  <div class="row justify-content-around keep-deets">
    <div class="col-md-6">
      <img class="modal-img" :src="keep.img" alt="">
    </div>
    <div class="col-md-5 details bg-light">
      <div class="row mt-3 text-primary">
        <div class="col-4">
        <p>{{keep.views}} <i class="mdi mdi-eye"></i></p>
        </div>
        <div class="col-4">
        <p>{{keep.shares}} <i class="mdi mdi-share-variant-outline"></i></p>
        </div>
        <div class="col-4">
        <p>{{keep.keeps}} <i class="mdi mdi-bookmark-check"></i></p>
        </div>
      </div>
      <hr>
      <div class="row pt-2 pb-5">
      <h4>{{keep.name}}</h4>
      <p>{{keep.description}}</p>
      </div>
      <hr>
      <div class="row pb-3">
        <div class="col-6">
          <div class="dropdown">
            <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
              Add To Collection
            </button>
            <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
              <li v-for="v in myVaults" :key="v.id" @click="addKeepToVault(v.id, keep.id)" class="selectable">{{v.name}}</li>
            </ul> 
          </div>
        </div>
        <div v-if="keep.creator" class="col-6">
          <img class="creator-pic" :src="keep.creator.picture" alt="">
          <h6>{{keep.creator.name}}</h6>
        </div>
      </div>
  </div>
</div>
</div>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted, onRenderTriggered } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Pop'
export default {
  setup() {
    return {
      keep: computed(() => AppState.currentKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      async addKeepToVault(vaultId, keepId) {
        try {
          await vaultsService.addKeepToVault(vaultId, keepId)
          Pop.toast('Added to Collection')
        } catch (error) {
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>

<style>
.modal-img {
  width: 100%;
  height: 50vh;
  object-fit: cover;
  object-position: center;
  border-radius: 15px;
  filter: drop-shadow(1px 1px 10px #4e4e4e);
}
.details {
  border-radius: 15px;
  height: 50vh;
  filter: drop-shadow(1px 1px 10px #4e4e4e);
}
.container {
  z-index: 200;
}
.btn{
  z-index: 205;
}
.modal-dialog {
  max-width: 80vw;
}
.creator-pic {
  width: 3rem;
  height: 3rem;
  object-fit: cover;
  border-radius: 15px;
}
</style>