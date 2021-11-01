<template>
  <div
    class="modal fade"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-body p-1">
          <slot name="modal-body"></slot>
          
        </div>
      </div>
      <div class="modal-footer">
        <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.modal {
  border-radius: 15px;
}
.modal-dialog {
  top: 15vh;
  max-width: 75vw;
}
.fade {
  backdrop-filter: blur(2px);
}
</style>