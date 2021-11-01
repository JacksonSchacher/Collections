<template>
  <div class="row">
    <div class="col-6">
      <img class="modal-img" :src="k.img" alt="">
    </div>
    <div class="col-6">
      <h4>{{k.name}}</h4>
      <p>{{k.description}}</p>
      <br>
      <h5>{{k.creator.name}}</h5>
      <cite>{{k.updatedAt}}</cite>
    </div>
  </div>
</template>

<script>
import { Keep } from '../models/Keep'
export default {
 props: {
    k: {
      type: Keep,
      default: () => { return new Keep()}
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
}
</style>