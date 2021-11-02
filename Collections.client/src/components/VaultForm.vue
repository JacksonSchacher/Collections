<template>
  <form class="keep-form">
    <div class="d-flex justify-content-between row">
      <div class="col-3">
        <h3>Create Vault</h3>
    <div class="form-group mt-4">
      <label for="title">Title</label>
      <input type="text" class="form-control" id="title" aria-describedby="vaultTitle" placeholder="Vault Title..">
    </div>
    <div class="form-group">
      <label for="isPrivate" class="form-check-label">Make Private?</label>
      <input type="checkbox" name="isPrivate" id="isPrivate" class="form-check-input">
       <small id="isPrivate" class="form-text f-10 text-muted">Private Collection Can Only Be Viewed By You</small>
     </div>
      </div>
      <div class="col-8">

    <div class="form-group text-center">
      <label for="description"></label>
      <textarea name="description" id="description" cols="40" rows="8" placeholder="Vault Description.."></textarea>
    </div>
      </div>
    </div>
    <div class="d-flex justify-content-between form-group align-items-end">    
      <div class="form-group">
      <label for="img">Image URL</label>
      <input type="url" class="form-control" id="img" aria-describedby="imgURL" placeholder="Image Url..">
    </div>
    <div>

      <button type="reset" class="btn btn-light me-2">Reset</button>
      <button type="submit" class="btn btn-primary">Create</button>
    </div>
    </div>
  </form>
</template>

<script>
export default {

}
</script>

<style lang="scss" scoped>
.form-control[id='title'] {
  width: 20vw;
}
.form-control[id='img'] {
  width: 40vw;
}
.keep-form {
  padding: 1rem 3rem;
}
.btn {
  height: 3rem;
}
</style>