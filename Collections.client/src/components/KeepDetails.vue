<template>
  <div class="row">
    <div class="col-6">
      <img class="modal-img" :src="keep.img" alt="">
    </div>
    <div class="col-6">
      <h4>{{keep.name}}</h4>
      <p>{{keep.description}}</p>
      <br>
      <h5>{{keep.creator.name}}</h5>
      <cite>{{keep.updatedAt}}</cite>
    </div>
  </div>
</template>

<script>
import { Keep } from '../models/Keep'
export default {
 props: {
    keep: {
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