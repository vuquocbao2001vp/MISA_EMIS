<template>
  <router-link
    class="menu-item"
    :class="isItemSelected ? 'menu-item--active' : ''"
  >
    <div class="item-icon icon-center" :class="menuItemIcon"></div>
    <span class="item-text" >{{ menuItemText }}</span>
  </router-link>
</template>
<script>
export default {
  props: ["menuItemIcon", "menuItemText", "isItemSelected"],
};
</script>

<style scoped>
.text-selected {
  color: var(--active-sidebar) !important;
}
.menu-item {
  box-sizing: border-box;
  display: flex;
  align-items: center;
  margin-bottom: 6px;
  border-width: 0 0 0 4px;
  border-style: solid;
  border-color: transparent;
}

.menu-item:hover {
  background-color: var(--menu-item-active);
  color: var(--button-green-text);
}

.menu-item--active {
  color: var(--button-green-text);
  border-left: 4px solid;
  border-color: var(--active-sidebar);
  background-color: var(--menu-item-active);
}

.item-icon {
  height: 40px;
  width: 48px;
  padding-left: 8px;
  margin-right: 4px;
  box-sizing: border-box;
}

.item-text {
  color: var(--menu-color-text);
  height: 18px;
  width: 150px;
}

a {
  text-decoration: none;
}
.router-link-exact-active {
  border-left: 4px solid;
  border-color: var(--active-sidebar);
  background-color: var(--menu-item-active);
}
.router-link-exact-active .item-text{
  color: var(--active-sidebar);
}
</style>