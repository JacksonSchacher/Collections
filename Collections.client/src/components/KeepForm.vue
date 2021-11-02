<template>
  <form>
    <div class="form-group">
      <label for="title">Title</label>
      <input type="text" class="form-control" id="title" aria-describedby="postTitle" placeholder="Post Title..">
    </div>
    <div class="form-group">
      <label for="img">Image URL</label>
      <input type="url" class="form-control" id="img" aria-describedby="imgURL" placeholder="Image Url..">
    </div>
    <div class="form-group">
      <label for="description">Description</label>
      <textarea name="description" id="description" cols="30" rows="10" placeholder="Post Description.."></textarea>
    </div>
    <div>
      <button type="reset" class="btn btn-light">Reset</button>
      <button type="submit" class="btn btn-primary">Add</button>
    </div>
  </form>
</template>

<script>
export default {

}
</script>

<style>

</style>