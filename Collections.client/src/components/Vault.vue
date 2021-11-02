<template>
  <div class="card keep-card text-white">
  <img src="https://images.unsplash.com/photo-1560105908-5d41e79c1043?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=735&q=80" loading="lazy" class="card-img selectable" alt="...">
  <div class="card-img-overlay">
    <h5 class="card-title">{{vault.name}}</h5>
    <p class="card-text">{{vault.description}}</p>
    <!-- <p class="card-text">{{keep.createdAt}}</p> -->
  </div>
</div>
</template>

<script>
import { Vault } from '../models/Vault'
export default {
  props: {
    vault: {
      type: Vault,
      default: () => { return new Vault()}
    }
  }
}
</script>

<style lang="scss" scoped>
img {
  border-radius: 15px;
}
.card {
  border-radius: 15px;
  filter: drop-shadow(1px 1px 15px #8d8d8d);
}
.card-img-overlay {
  text-align: initial;
}
</style>