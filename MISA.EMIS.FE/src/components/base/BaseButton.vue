<template>
  <button :class="buttonType">{{ buttonName }}</button>
</template>
<script>
export default {
  props:[
      'buttonName',
      'buttonType',
  ],
}
</script>
<style>
button {
  margin: 0;
  padding: 0 12px;
  height: 32px;
  min-width: 96px;
  border-radius: 4px;
  border-width: 0;
}

button:focus {
  border-width: 1px solid #000;
  border-radius: 4px;
}

.button-green {
  background-color: var(--main);
  color: var(--button-white-background);
}

.button-green:hover {
  background-color: var(--main-hover);
  cursor: pointer;
}

.button-green-disable {
  background-color: var(--button-grey-background);
  color: var(--button-grey-text);
  cursor: not-allowed;
}

.button-white {
  background-color: var(--button-white-background);
  color: var(--button-black-text);
  border: 1px solid;
  border-color: var(--button-border);
}

.button-white:hover {
  background-color: var(--background);
  cursor: pointer;
}

.button-white--disable {
  background-color: var(--button-white-background);
  color: var(--button-grey-text);
  cursor: not-allowed;
}
</style>