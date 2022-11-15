<template>
  <div class="paging">
    <div
      class="paging-nav paging-first-page icon-center icon-first-page"
      @click="toFirstPage"
    ></div>
    <div
      class="paging-nav paging-prev icon-center icon-prev"
      @click="toPrevPage"
    ></div>
    <input
      class="paging-nav paging-number"
      v-model="pageNumber"
      @keyup.enter="loadPaging"
    />
    <div
      class="paging-nav paging-next icon-center icon-next"
      @click="toNextPage"
    ></div>
    <div
      class="paging-nav paging-last-page icon-center icon-last-page"
      @click="toLastPage"
    ></div>
    <div class="paging-order">
      {{ currentPage }}/{{ totalPage }} trang ({{ totalResult }} kết quả)
    </div>
  </div>
</template>
<script>
export default {
  props: ["totalResult", "totalPage", "currentPage", "filterName"],
  data() {
    return {
      pageNumber: null,
    };
  },
  watch: {
    currentPage: function (value) {
      this.pageNumber = value.toString();
    },
  },
  methods: {
    /**
     * Khi nhập số thứ tự trang muốn hiển thị thì thực hiện phân trang theo số thứ tự đó
     * Author: VQBao - 26/7/2022
     */
    loadPaging() {
      if (this.pageNumber != "") {
        var pageNumber = parseInt(this.pageNumber);
        if (pageNumber <= 0 || pageNumber == null) {
          pageNumber = 1;
          this.pageNumber = "1";
          this.$emit("loadData", 25, pageNumber, this.filterName);
        } else if (pageNumber > this.totalPage) {
          pageNumber = this.totalPage;
          this.$emit("loadData", 25, pageNumber, this.filterName);
        } else {
          this.$emit("loadData", 25, pageNumber, this.filterName);
        }
      } else {
        this.pageNumber="1";
        this.$emit("loadData", 25, 1, this.filterName);
      }
    },
    /**
     * Khi click vào nút về trang đầu
     * Author: VQBao - 26/7/2022
     */
    toFirstPage() {
      if (this.currentPage != 1) {
        this.$emit("loadData", 25, 1, this.filterName);
      }
    },
    /**
     * Khi click vào nút về trang cuối
     * Author: VQBao - 26/7/2022
     */
    toLastPage() {
      if (this.currentPage != this.totalPage) {
        this.$emit("loadData", 25, this.totalPage, this.filterName);
      }
    },
    /**
     * Khi click vào nút chuyển trang liền trước
     * Author: VQBao - 26/7/2022
     */
    toPrevPage() {
      if (this.currentPage > 1) {
        this.$emit("loadData", 25, this.currentPage - 1, this.filterName);
      }
    },
    /**
     * Khi click vào nút chuyển trang tiếp theo
     * Author: VQBao - 26/7/2022
     */
    toNextPage() {
      if (this.currentPage < this.totalPage) {
        this.$emit("loadData", 25, this.currentPage + 1, this.filterName);
      }
    },
  },
};
</script>
<style scoped>
input {
  min-width: 64px;
  padding: 0 8px;
  text-align: center;
  outline: none;
  border: 1px solid;
  border-color: var(--button-border);
  border-radius: 4px;
  box-sizing: border-box;
}

input:focus {
  border-color: var(--input-focus-border-color);
}

.paging {
  height: 56px;
  width: 100%;
  position: absolute;
  bottom: 0;
  background-color: var(--white-background);
  display: flex;
  align-items: center;
  padding-left: 16px;
  box-sizing: border-box;
  border-top: 1px solid;
  border-color: var(--button-border);
}
.paging-nav.icon-center {
  width: 24px;
  height: 24px;
  background-size: cover;
  margin-right: 20px;
  cursor: pointer;
}
.paging-number {
  width: 64px;
  height: 32px;
  display: flex;
  align-items: center;
  justify-content: center;
  border: 1px solid;
  border-radius: 4px;
  border-color: var(--button-border);
  cursor: text;
  margin-right: 20px;
}
</style>