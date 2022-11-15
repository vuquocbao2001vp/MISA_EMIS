<template>
  <div class="content">
    <router-view></router-view>
  </div>
</template>
<script>
export default {};
</script>
<style scoped>
.content {
    position: relative;
    width: 100%;
    height: calc(100vh - 48px);
    box-sizing: border-box;
}
</style>