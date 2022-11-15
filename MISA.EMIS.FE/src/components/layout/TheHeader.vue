<template>
  <div id="headerLayout" class="header">
    <div class="header-left">
      <span class="header-title">{{Resource.HEADER.HeaderTitle}}</span>
      <div class="title-icon icon-sprite-help" title="Trợ giúp"></div>
    </div>
    <div class="header-right">
      <div class="header-notification icon-center icon-Notification-32" title="Thông báo"></div>
      <div class="user">
        <div class="user-name">{{Resource.HEADER.UserName}}</div>
        <div class="user-select-icon icon-center icon-Chevron"></div>
        <div class="user-avatar icon-center icon-avatar-36"></div>
      </div>
    </div>
  </div>
</template>
<script>
import MISAResource from '@/script/constants/MISAResource'
export default {
  created(){
    this.Resource = MISAResource;
  }
};

</script>
<style scoped>
.header {
    width: 100%;
    height: 48px;
    position: relative;
    box-shadow: 2px 0 4px 0 #A6A6A6;
}

.header-left {
    height: 48px;
    display: flex;
    align-items: center;
    position: absolute;
    left: 16px;
    top: 0;
}

.header-right {
    height: 48px;
    display: flex;
    align-items: center;
    position: absolute;
    right: 32px;
    top: 0;
}

.header-title {
    font-size: 18px;
    min-width: 143px;
}

.title-icon {
    margin-left: 8px;
    cursor: pointer;
}

.header-notification {
    width: 32px;
    height: 32px;
    margin-right: 16px;
    cursor: pointer;
}

.user {
    display: flex;
    align-items: center;
    height: 40px;
    border-left: 1px solid #cccccc;
}

.user-name {
    font-size: 14px;
    width: 48px;
    margin-left: 16px;
}

.user-select-icon {
    width: 24px;
    height: 24px;
    margin: 0 4px;
    cursor: pointer;
}

.user-avatar {
    width: 40px;
    height: 40px;
    border-radius: 50%;
    cursor: pointer;
}
</style>
