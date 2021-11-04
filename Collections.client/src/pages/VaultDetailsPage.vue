<template>
<div class="container pt-3">

<div class=" col-12 text-end f-18"><i class="mdi mdi-delete selectable" @click="deleteVault(vault.id)"></i></div>
<h1>{{vault.name}}</h1>
<p>{{vault.description}}</p>
<h5>Posts: {{vaultKeeps.length}}</h5>
<div class="masonry-columns text-center">
  <div v-for="k in vaultKeeps" :key="k.id">
    <i class="mdi mdi-delete selectable" @click="deleteVaultKeep(k.id, vault.id)"></i>
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
import Pop from '../utils/Pop'
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
          if (await Pop.confirm("Delete Collection?")) {
            await vaultsService.deleteVault(vaultId)
            Pop.toast("Collection Deleted")
            router.push({name: 'Profile', params: {profileId: vault.creator.id}})
          }
        } catch (error) {
          Pop.toast(error.Message, 'error')
        }
      },
      async deleteVaultKeep(vaultKeepId, vaultId) {
        try {
          if (await Pop.confirm("Remove Post from Collection?")) {
            await vaultsService.deleteVaultKeep(vaultKeepId, vaultId)
            Pop.toast("Post Removed")
          }
        } catch (error) {
          Pop.toast(error.Message, 'error')
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