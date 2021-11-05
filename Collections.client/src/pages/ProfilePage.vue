<template>
  <div class="container pt-5">
    <div class="row pb-3">
      <div class="col-md-3">
        <img class="profile-pic" :src="profile.picture" alt="">
      </div>
      <div class="d-flex flex-column profile-name col-md-6 bg-dark align-items-start justify-content-center">
        <h2>{{profile.name}}
          <i data-bs-toggle="modal" data-bs-target="#account-modal" v-show="profile.id == account.id" class="mdi mdi-pencil f-18 selectable"></i>
        </h2>
        <h6>Collections: {{vaults.length}}  </h6>
        <h6>Posts: {{keeps.length}}  </h6>
      </div>
    </div>
    <div class="row">
      <span><h3>Collections <i v-if="profile.id == account.id" class="mdi mdi-plus-box-multiple selectable" data-bs-toggle="modal" data-bs-target="#vault-modal"></i></h3></span>
      <div class="masonry-columns text-center">
      <div v-for="v in vaults" :key="v.id" class="selectable vaults-div" @click="goToVaultDetails(v)">
        <Vault :vault="v" />
      </div>
      </div>
    </div>
    <div class="row">
      <span><h3>Posts <i v-if="profile.id == account.id" class="mdi mdi-plus-box selectable" data-bs-toggle="modal" data-bs-target="#keep-modal"></i></h3></span>
      <div class="masonry-columns text-center">
      <div v-for="k in keeps" :key="k.id" class="d-inline-flex">
        <Keep :keep="k" />
      </div>
      </div>
    </div>
  </div>

  <Modal id="keep-modal">
  <template #modal-body>
    <KeepForm />
  </template>
</Modal> 

<Modal id="vault-modal"> 
  <template #modal-body>
    <VaultForm />
  </template>
</Modal>

<Modal id="account-modal"> 
  <template #modal-body>
    <AccountForm />
  </template>
</Modal>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted, watchEffect } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { router } from '../router'
import Pop from '../utils/Pop'
export default {
  setup() {
    const route = useRoute()
    onMounted(async() => {
      const profileId = route.params.profileId
      await profilesService.setCurrentProfile(profileId)
      await profilesService.getVaults(profileId)
      await profilesService.getKeeps(profileId)
    })
    return {
      profile: computed(() => AppState.currentProfile),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps),
      goToVaultDetails(vault) {
        try {
          if (vault.isPrivate) {
            if (AppState.account.id == AppState.currentProfile.id) {
              router.push({name: 'VaultDetails', params: {vaultId: vault.id}})
            }
            else{
              Pop.toast("This Vault is Private")
            }
            return
          }
          router.push({name: 'VaultDetails', params: {vaultId: vault.id}})
        } catch (error) {
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.profile-pic {
  border-radius: 15px;
  height: 10rem;
  width: 10rem;
  object-fit: cover;
  object-position: center;
}
.profile-name {
  border-radius: 15px;
}
.masonry-columns {
  columns: 4 200px;
  column-gap: 1.5rem;
  flex-flow: column;
  div {
    max-width: 30rem;
    border-radius: 15px;
    margin: .5rem 0;
  } 
}
.vaults-div {
  height: 200px;
  width: 200px;
  background-color: #cccccc;
  display: inline-flex;
  flex-flow: column;
}
</style>