<template>
  <div class="menu" id="menuLayout">
    <div class="menu-header">
      <div id="menuIcon" class="menu-icon icon-center icon-Iconthietbi"></div>
      <span id="menuText" class="menu-text">{{Resource.MENU.MenuHeader}}</span>
      <div class="toggle-sidebar" @click="toggleSidebar"><div class="menu-sidebar icon-Sidebar"></div></div>
    </div>
    <div class="menu-selection">
      <BaseMenuItem
        to="/overview"
        menuItemIcon="icon-TongQuan"
        menuItemText="Tổng quan"
        :isItemSelected="isItemSelected[0]"
        @click="selectMenuItem(0)"
      />
      <BaseMenuItem
        to="/device"
        menuItemIcon="icon-ThietBi"
        menuItemText="Thiết bị"
        :isItemSelected="isItemSelected[1]"
        @click="selectMenuItem(1)"
      />
      <BaseMenuItem
        to="/borrow"
        menuItemIcon="icon-MuonTra"
        menuItemText="Mượn trả"
        :isItemSelected="isItemSelected[2]"
        @click="selectMenuItem(2)"
      />
      <BaseMenuItem
        to="/report"
        menuItemIcon="icon-BaoCao"
        menuItemText="Báo cáo"
        :isItemSelected="isItemSelected[3]"
        @click="selectMenuItem(3)"
      />
      <BaseMenuItem
        to="/system"
        menuItemIcon="icon-HeThong"
        menuItemText="Hệ thống"
        :isItemSelected="isItemSelected[4]"
        @click="selectMenuItem(4)"
      />
    </div>
  </div>
</template>
<script>
import BaseMenuItem from '../base/BaseMenuItem.vue'
import MISAResource from '../../script/constants/MISAResource'
export default {
  components: {
    BaseMenuItem,
  },
  created() {
    this.Resource = MISAResource
  },
  data() {
    return {
      isItemSelected: [],
      isToggle: false,
    }
  },
  methods: {
    /**
     * Khi chọn item ở menu thì đổi màu nền
     * Author: VQBao - 10/8/2022
     */
    selectMenuItem(index){
      for (let i = 0; i < this.isItemSelected.length; i++) {
        this.isItemSelected[i] = false;
      }
      this.isItemSelected[index] = true;
    },
    /**
     * Khi bấm vào icon sidebar thì thu gọn hoặc mở rộng menu
     * Author: VQbao - 27/7/2022
     */
    toggleSidebar(){
      var items = document.getElementsByClassName("item-text");
      var menuItems = document.getElementsByClassName("menu-item");
      if(!this.isToggle){
        document.getElementById("menuLayout").style.width = "56px";
        document.getElementById("menuLayout").style.transition = "0.1s";
        document.getElementById("menuIcon").style.display = "none";
        document.getElementById("menuText").style.display = "none";
        for(let i=0; i< items.length; i++){
          items[i].style.display = "none";
          menuItems[i].style.width = "56px";
          menuItems[i].style.transition = "0.1s"
        }
        document.getElementById("containerRight").style.width = "calc(100% - 56px)"
        this.isToggle = true;
      } else {
        document.getElementById("menuLayout").style.width = "220px";
        document.getElementById("menuLayout").style.transition = "0.1s";
        document.getElementById("menuIcon").style.display = "block";
        document.getElementById("menuText").style.display = "block";
        for(let i=0; i< items.length; i++){
          items[i].style.display = "block";
          menuItems[i].style.width = "220px";
          menuItems[i].style.transition = "0.1s";
        }
        document.getElementById("containerRight").style.width = "calc(100% - 220px)"
        this.isToggle = false;
      }
    }
  },
};
</script>
<style scoped>
.menu {
  width: 220px;
  height: 100%;
  background-color: var(--menu-sidebar-background);
  box-sizing: border-box;
}

.menu-header {
  width: 100%;
  height: 48px;
  display: flex;
  align-items: center;
  margin-bottom: 12px;
  box-sizing: border-box;
  position: relative;
}

.menu-icon {
  width: 34px;
  height: 34px;
  background-size: cover;
  margin: 8px 10px 8px 12px;
}

.menu-text {
  font-family: OpenSans-Bold;
  font-size: 16px;
  color: var(--menu-color-text);
  height: 22px;
  text-overflow: ellipsis;
  overflow: hidden;
}
.toggle-sidebar {
  width: 56px;
  height: 48px;
  position: absolute;
  right: 0;
  cursor: pointer;
}
.toggle-sidebar:hover{
  background-color: var(--menu-item-active);
}
.menu-sidebar {
  position: absolute;
  top: 16px;
  right: 20px;
}

.menu-selection {
  width: 220px;
  margin-top: 3px;
  height: calc(100vh - 50px - 16px);
}

</style>
