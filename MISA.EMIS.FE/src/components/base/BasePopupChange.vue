<template>
  <div class="popup" :class="{ 'hide-popup': isHidePopup }">
    <div class="popop-content">
      <div
        class="popup-close icon-center icon-X-2"
        @click="continueType"
      ></div>
      <span class="popup-title">{{ title }}</span>
      <span class="popup-text">{{ text }}</span>
      <div class="popup-button">
        <BaseButton
          buttonType="button-white"
          buttonName="Đóng"
          @click="continueType"
        />
        <BaseButton
          buttonType="button-white"
          buttonName="Không"
          @click="closeDialog"
        />
        <BaseButton
          buttonType="button-green"
          buttonName="Lưu"
          @click="saveOnClick"
        />
      </div>
    </div>
  </div>
</template>
<script>
import MISAEnum from '@/script/constants/MISAEnum';
import BaseButton from "./BaseButton.vue";
export default {
  components: { BaseButton },
  props: ["isHidePopup", "title", "text", "btnSaveMode"],
  methods: {
    /**
     * Khi ấn đóng thì tiếp tục thực hiện nhập vào bảng thông tin chi tiết
     * Author: VQBao - 15/8/2022
     */
    continueType() {
      this.$emit("hidePopup", true);
    },
    /**
     * Khi ấn không thì đóng form thông tin chi tiết
     * Author: VQBao - 15/8/2022
     */
    closeDialog() {
      this.$emit("hidePopup", true);
      this.$emit("hideDialog");
      // nếu đang sửa thì rollback lại giá trị teacher cũ
      if(this.btnSaveMode == MISAEnum.EditMode.Edit){
        this.$emit("rollBackOldTeacher");
      }
    },
    /**
     * Khi ấn lưu thì lưu thông tin giáo viên đó
     * Author: VQBao - 15/8/2022
     */
    saveOnClick() {
      this.$emit("saveStaffOnClick");
      this.$emit("hidePopup", true);
    },
  },
};
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
  margin-left: calc(100% - 24px - 96px - 96px - 8px - 96px - 8px);
  margin-bottom: 24px;
}

.popup-button button {
  margin-right: 8px;
}
</style>