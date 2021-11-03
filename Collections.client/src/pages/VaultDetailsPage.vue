<template>
<div class="container pt-3">

<h1>{{vault.name}}</h1>
<p>{{vault.description}}</p>
<h5>Posts: {{vaultKeeps.length}}</h5>
<div class="masonry-columns text-center">
  <div v-for="k in vaultKeeps" :key="k.id">
    <Keep :keep="k.keep" />
  </div>
</div>
</div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
export default {
  setup() {
    const route = useRoute();
    onMounted(async() => {
      await vaultsService.getVaultById(route.params.vaultId)
      await vaultsService.getVaultKeeps(route.params.vaultId)
    })
    return {
      vaultKeeps: computed(() => AppState.vaultKeeps.filter(v => v.vaultId == route.params.vaultId)),
      vault: computed(() => AppState.currentVault)
    }
  }
}
</script>

<style lang="scss" scoped>
.masonry-columns {
  columns: 4 200px;
  column-gap: 1.5rem;
  flex-flow: column;
  div {
    display: inline-block;
    max-width: 30rem;
    border-radius: 15px;
    margin: .5rem 0;
  } 
}
</style>