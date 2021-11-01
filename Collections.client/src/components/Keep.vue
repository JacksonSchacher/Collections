<template>
  <div class="card keep-card text-white selectable" data-bs-toggle="modal" data-bs-target="#keep-details-modal">
  <img :src="keep.img" loading="lazy" class="card-img" alt="...">
  <div class="card-img-overlay">
    <h5 class="card-title">{{keep.name}}</h5>
    <p class="card-text">{{keep.description}}</p>
    <!-- <p class="card-text">{{keep.createdAt}}</p> -->
  </div>
</div>

  <!-- Keep Modal -->
<Modal id="keep-details-modal">
  <template #modal-body>
    <KeepDetails :keep="keep" />
  </template>
</Modal>

</template>

<script>
import { Keep } from '../models/Keep'
import { keepsService } from '../services/KeepsService'
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
      setKeepDetails(keep) {
        try {
          keepsService.setCurrentKeep(keep)
        } catch (error) {
          Pop.toast(error.Message, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.card-img-overlay {
  text-align: initial;
}
.modal-img {
  width: 100%;
  height: 50vh;
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
</style>

