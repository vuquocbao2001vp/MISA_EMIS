<template>
  <div class="container">
    <TheMenu />
    <div id="containerRight" class="container-right">
      <TheHeader />
      <TheContent />
    </div>
  </div>
</template>
<script>
import TheHeader from "./components/layout/TheHeader.vue";
import TheMenu from "./components/layout/TheMenu.vue";
import TheContent from "./components/layout/TheContent.vue";
export default {
  name: "App",
  components: {
    TheHeader,
    TheMenu,
    TheContent,
  },
};
</script>

<style>
@import url(./style/main.css);
</style>
