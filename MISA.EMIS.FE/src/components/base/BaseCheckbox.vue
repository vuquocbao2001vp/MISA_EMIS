<template>
  <div class="checkbox"></div>
</template>
<script>
export default {};
</script>
<style>
.checkbox {
  width: 20px;
  height: 20px;
  background-position: center;
  background-repeat: no-repeat;
  cursor: pointer;
}

.checkbox-inactive {
  background-image: url(../../assets/Icons/ic_Checkbox_Inactive.png) !important;
}

.checkbox-inactive:hover {
  background-image: url(../../assets/Icons/ic_Checkbox_Hover.png);
}

.checkbox-inactive-disable {
  background-image: url(../../assets/Icons/ic_Checkbox_Inactive_Disable.png);
}

.checkbox-active {
  background-image: url(../../assets/Icons/ic_Checkbox_Active.png) !important;
}

.checkbox-active-disable {
  background-image: url(../../assets/Icons/ic_Checkbox_Active_Disable.png);
}
</style>