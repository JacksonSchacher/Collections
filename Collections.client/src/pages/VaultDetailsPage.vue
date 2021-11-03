<template>
<h1>{{vault.name}}</h1>
<p>{{vault.description}}</p>
<h5>Posts: {{keeps.length}}</h5>
  <h1 v-for="k in keeps" :key="k.id">{{k.name}}</h1>
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
    })
    return {
      keeps: computed(() => AppState.vaultKeeps.filter(v => v.vaultId == route.params.vaultId)),
      vault: computed(() => AppState.currentVault)
    }
  }
}
</script>

<style>

</style>