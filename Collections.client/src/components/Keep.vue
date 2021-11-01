<template>
  <div class="card keep-card text-white selectable" data-bs-toggle="modal" data-bs-target="#keep-details-modal">
  <img :src="keep.img" class="card-img" alt="...">
  <div class="card-img-overlay">
    <h5 class="card-title">{{keep.name}}</h5>
    <p class="card-text">{{keep.description}}</p>
    <!-- <p class="card-text">{{keep.createdAt}}</p> -->
  </div>
</div>

  <!-- Keep Modal -->
<Modal id="keep-details-modal">
  <template #modal-body>
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
</Modal>

</template>

<script>
import { Keep } from '../models/Keep'
import Pop from '../utils/Pop'
export default {
  props: {
    keep: {
      type: Keep,
      default: () => { return new Keep()}
    }
  },
  setup() {
    return {

    }
  }
}
</script>

<style lang="scss" scoped>
.card-img-overlay {
  text-align: initial;
}
.modal-img {
  object-fit: cover;
  object-position: center;
}
img {
  border-radius: 15px;
}
.card {
  border-radius: 15px;
  filter: drop-shadow(1px 1px 15px #8d8d8d);
}
focused-keep {
  transition: all 1s ease-in-out;
  transform: scale(3);
}
</style>

