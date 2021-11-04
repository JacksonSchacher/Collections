<template>
<div class="container pt-3">

<div class=" col-12 text-end f-18"><i class="mdi mdi-delete selectable" @click="deleteVault(vault.id)"></i></div>
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
import { router } from '../router'
export default {
  setup() {
    const route = useRoute();
    onMounted(async() => {
      await vaultsService.getVaultById(route.params.vaultId)
      await vaultsService.getVaultKeeps(route.params.vaultId)
    })
    return {
      vaultKeeps: computed(() => AppState.vaultKeeps.filter(v => v.vaultId == route.params.vaultId)),
      vault: computed(() => AppState.currentVault),
      async deleteVault(vaultId) {
        try {
          if (await Pop.confirm("Delete Vault?")) {
            await vaultsService.deleteVault(vaultId)
            Pop.toast("Vault Deleted")
            router.push({name: 'Profile', params: {profileId: vault.creator.id}})
          }
        } catch (error) {
          
        }
      }
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