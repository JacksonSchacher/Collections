<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-primary px-3">
    <div class="">
      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <h3>Collections</h3>
        </div>
      </router-link>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarText"
        aria-controls="navbarText"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon" />
      </button>
    </div>
    <div class="collapse navbar-collapse" id="navbarText">
    <div class="">
      <form @submit.prevent="search()">
        <label for="searh">Search Collections</label>
        <input type="search" id="search" placeholder="Search.." autofocus required />
        <button class="search-btn" type="submit"><i class="mdi mdi-magnify"></i></button>
      </form>
    </div>
    <div>
      <span class="navbar-text">
        <button
          class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0"
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>

        <div class="dropdown my-2 my-lg-0" v-else>
          <div
            class="dropdown-toggle selectable"
            data-bs-toggle="dropdown"
            aria-expanded="false"
            id="authDropdown"
          >
            <img
              :src="user.picture"
              alt="user photo"
              height="40"
              class="rounded"
            />
            <span class="mx-3 text-success lighten-30">{{ user.name }}</span>
          </div>
          <div
            class="dropdown-menu p-0 list-group w-100"
            aria-labelledby="authDropdown"
          >
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item list-group-item-action hoverable">
                Manage Account
              </div>
            </router-link>
            <div
              class="list-group-item list-group-item-action hoverable text-danger"
              @click="logout"
            >
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </span>
    </div>
    </div>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed } from 'vue'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active{
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
.search-btn {
  display: none;
  position: absolute;
  top: 0;
  right: 0;
  width: 3rem;
  font-weight: 600;
  background-color: #333333;
  color: #f2f2f2;
  border-radius: 0 15px 15px 0;
}
form {
  position: relative;
  width: 20rem;
  border-radius: 15px;
}
input, button {
  border: 0;
  font-size: 1.25rem;
}
input[type="search"] {
  outline: 0;
  width: 100%;
  padding: 0 1rem;
  border-radius: 15px;
  appearance: none;
  transition: all .3s ease-in-out;
  transition-property: width, border-radius;
  z-index: 1;
  position: relative;
}
input:not(:placeholder-shown) {
  border-radius: 15px 0 0 15px;
  width: calc(100% - 3rem);
}
input:not(:placeholder-shown) + button {
    display: block;
}
label {
  position: absolute;
  clip: rect(1px, 1px, 1px, 1px);
  padding: 0;
  border: 0;
  height: 1px;
  width: 1px;
  overflow: hidden;
}
</style>
