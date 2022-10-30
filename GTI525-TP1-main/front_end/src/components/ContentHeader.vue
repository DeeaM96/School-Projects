<template>
  <div class="mx-0 fill-width px-0 py-0" style="position: relative">
    <v-row class="mx-0 my-0" style="background: #fcaba7">
      <div class="banner-msg">Stations GTI525</div>
      <v-col cols="2">
        <img class="logo" :src="require('../assets/logo.png')" />
      </v-col>
      <v-col class="10 px-0 py-0">
        <span class="banner-container">
          <span class="banner-fade-in" />
          <img class="banner-img" :src="require('../assets/banner.jpg')" />
        </span>
      </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  name: "ContentHeader",
};
</script>

<style lang="scss">
.banner {
  &-img {
    width: 100%;
    height: 200px;
    object-fit: cover;
    object-position: 0% 33%;
  }

  &-container {
    position: relative;
    text-align: center;
    max-height: 200px;
    width: 100%;
  }

  &-msg {
    font-size: 40px;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    z-index: 1;
  }

  &-fade-in {
    position: absolute;
    background-image: linear-gradient(to right, #fcaba7, rgba(255, 0, 0, 0));
    width: 100%;
    height: 100%;
    left: 0;
  }
}

.logo {
  height: 120px;
  align-self: center;
}

.v-col {
  display: flex;
  justify-content: center;
}
</style>
