<template>
  <div class="toast-message" :class="{'hide-toast': isHideToast}">
    <div class="toast-icon icon-center" v-if="toastType == 'success'" :class="{'icon-toast-success': toastType }"></div>
    <div class="toast-icon icon-center" v-if="toastType == 'fail'" ></div>
    <div class="toast-text">
      <span class="toast-text-header">{{title}}</span>
      <span class="toast-text-content">{{text}}</span>
    </div>
    <div class="toast-close"></div>
  </div>
</template>
<script>
export default {
    props: ['isHideToast','toastType','title', 'text']
};
</script>
<style scoped>
.hide-toast {
  display: none !important;
}

.toast-message {
  position: fixed;
  bottom: 40px;
  right: 40px;
  min-height: 80px;
  min-width: 300px;
  display: flex;
  align-items: center;
  border-radius: 4px;
  border-left: 4px solid;
  border-left-color: var(--active-sidebar);
  box-shadow: 1px 0.5px 1px 1px rgb(200, 200, 200);
  box-sizing: border-box;
  background-color: #fff;
}

.toast-icon {
  width: 40px;
  height: 40px;
  margin: 0 18px;
}

.toast-text {
  display: flex;
  flex-direction: column;
  height: 40px;
  width: 180px;
}

.toast-text-header {
  font-family: OpenSans-Bold;
  font-size: 18px;
  line-height: 16px;
  margin-bottom: 4px;
}

.toast-text-content {
  font-size: 14px;
}

.toast-close{
  width: 40px;
  height: 40px;
  margin-right: 18px;
  margin-left: 40px;
}
</style>