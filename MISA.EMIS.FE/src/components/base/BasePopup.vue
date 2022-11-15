<template>
  <div class="popup" :class="{'hide-popup': isHidePopup}">
    <div class="popop-content">
      <div class="popup-close icon-center icon-X-2" @click="cancleDeleteStaff"></div>
      <span class="popup-title">{{title}}</span>
      <span class="popup-text">{{text}}</span>
      <div class="popup-button">
        <BaseButton buttonType="button-white" buttonName="Đóng" @click="cancleDeleteStaff"/>
        <BaseButton buttonType="button-green" buttonName="Đồng ý" @click="confirmDeleteStaff"/>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: ['isHidePopup','title', 'text', 'staffSelected', 'rowStaff'],
  methods: {
    /**
     * Khi bấm vào nút đóng thì hủy thực hiện xóa
     * Author: VQBao - 26/7/2022
     */
    cancleDeleteStaff() {
      this.$emit("hidePopup", true);
    },

    /**
     * Khi bấm vào nút đồng ý thì thực hiện xóa
     * Author: VQBao - 26/7/2022
     */
    confirmDeleteStaff() {
      if(this.rowStaff.length == 1 || this.rowStaff.length == 0){
        this.$emit("removeStaff", this.staffSelected);
        this.$emit("hidePopup", true);
      } else {
        this.$emit("removeStaffs", this.rowStaff);
        this.$emit("hidePopup", true);
      } 
    }
  },
}
</script>
<style scoped>
.hide-popup {
  display: none !important;
}
.popup {
  position: fixed;
  align-items: center;
  justify-content: center;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.24);
  display: flex;
  align-items: center;
}

.popop-content {
  width: 400px;
  background-color: #ffffff;
  position: relative;
  display: flex;
  flex-direction: column;
}

.popup-close {
  width: 40px;
  height: 40px;
  position: absolute;
  top: 0;
  right: 0;
  cursor: pointer;
}

.popup-title {
  font-family: Opensans-Bold;
  font-size: 24px;
  margin: 20px 24px 12px 24px;
}

.popup-text {
  margin: 0 24px 16px 24px;
  line-height: 24px;
}

.popup-button {
  margin-left: calc(100% - 24px - 96px - 96px - 8px);
  margin-bottom: 24px;
}

.popup-button button {
  margin-right: 8px 
}
</style>