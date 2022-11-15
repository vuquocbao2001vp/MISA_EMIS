<template>
  <BaseLoader :isHideLoader="isHideLoader" />
  <div class="content-toolbar">
    <input
      type="text"
      class="toolbar-input"
      placeholder="Nhập Số hiệu cán bộ hoặc Tên cán bộ để tìm kiếm"
      v-model="inputTextSearch"
      @keyup="searchByText"
    />
    <div class="input-search icon-center icon-Search" title="Tìm kiếm"></div>
    <div class="toolbar-button">
      <BaseButton
        id="btnCreate"
        buttonName="Thêm"
        buttonType="button-green"
        @click="addStaffOnClick"
      />
      <BaseButton
        id="btnExport"
        buttonName="Xuất khẩu"
        buttonType="button-white"
        @click="exportToExcel"
      />
      <div
        id="btnMore"
        class="toolbar-more icon-center icon-More"
        @click="btnToolbarMoreOnclick"
      >
      </div>
      <BaseButton
        class="icon-left icon-delete"
        id="btnDeleteMore"
        buttonName="Xóa"
        :buttonType="btnMoreType"
        :class="{ 'hide-delete-button': isHideDeleteButton }"
        @click="removeStaffsOnClick"
      />
    </div>
  </div>
  <div class="page-table">
    <div class="content-table">
      <table>
        <thead>
          <tr>
            <th class="col-checkbox" style="width: 28px">
              <BaseCheckbox
                @click="checkAllCheckbox"
                :class="isCheckAll ? 'checkbox-active' : 'checkbox-inactive'"
              />
            </th>
            <th class="col-staff-code">{{ Resource.STAFF_INFO.StaffCode }}</th>
            <th class="col-full-name">{{ Resource.STAFF_INFO.FullName }}</th>
            <th class="col-phone-number">
              {{ Resource.STAFF_INFO.PhoneNumber }}
            </th>
            <th class="col-department">{{ Resource.STAFF_INFO.Department }}</th>
            <th class="col-subject" :title="Resource.TITLE.SubjectManagement">
              {{ Resource.STAFF_INFO.Subject }}
            </th>
            <th class="col-room" :title="Resource.TITLE.RoomManagement">
              {{ Resource.STAFF_INFO.Room }}
            </th>
            <th class="col-is-trained" :title="Resource.TITLE.TrainingQualify">
              {{ Resource.STAFF_INFO.IsTrained }}
            </th>
            <th class="col-is-working">{{ Resource.STAFF_INFO.IsWorking }}</th>
            <th class="col-button"></th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(staff, index) in staffs"
            :key="staff.teacherId"
            :class="isCheckCheckbox[index] ? 'row-selected' : ''"
          >
            <td class="text-align-center col-checkbox">
              <BaseCheckbox
                @click="rowCheckboxSelected(staff, index)"
                :class="
                  isCheckCheckbox[index]
                    ? 'checkbox-active'
                    : 'checkbox-inactive'
                "
              />
            </td>
            <td class="text-align-center">{{ staff.teacherCode }}</td>
            <td
              class="text-align-left col-full-name"
              style="color: var(--button-green-text)"
              @click="rowOnClick(staff)"
              :title="staff.fullName"
            >
              {{ staff.fullName }}
            </td>
            <td class="text-align-center">{{ staff.phoneNumber }}</td>
            <td class="text-align-left">{{ staff.departmentName }}</td>
            <td class="text-align-left" :title="staff.subjectName">
              {{ staff.subjectName }}
            </td>
            <td class="text-align-left" :title="staff.roomName">
              {{ staff.roomName }}
            </td>
            <td class="text-align-center col-check-icon">
              <div
                class="cell-check icon-center icon-check"
                :class="{ 'hide-delete-button': !staff.isTrained }"
              ></div>
            </td>
            <td class="text-align-center col-check-icon">
              <div
                class="cell-check icon-center icon-check"
                :class="{ 'hide-delete-button': !staff.isWorking }"
              ></div>
            </td>
            <td class="text-align-center col-icon">
              <div
                class="cell-edit-icon icon-center icon-edit"
                @click="rowOnClick(staff)"
                title="Sửa"
              ></div>
              <div
                class="cell-remove-icon icon-center icon-remove"
                @click="removeStaffOnClick(staff)"
                title="Xóa"
              ></div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <BaseTablePaging
      :totalResult="totalResult"
      :totalPage="totalPage"
      :currentPage="currentPage"
      :filterName="filterName"
      @loadData="loadData"
    />
  </div>
  <DialogStaffDetail
    :staffs="staffs"
    :isShow="isShowDialog"
    :staffSelected="staffSelected"
    :staffSelectedId="staffSelectedId"
    :btnSaveMode="btnSaveMode"
    :dialogName="dialogName"
    @isShowStaffDialog="showStaffDialog"
    @loadData="loadData"
    @hideToastMessage="hideToastMessage"
  />
  <BaseToast
    toastType="success"
    :isHideToast="isHideToast"
    :title="toastTitle"
    :text="toastText"
    
  />
  <BasePopup
    :title="popupTitle"
    :text="popupText"
    :isHidePopup="isHidePopup"
    @hidePopup="hidePopup"
    @removeStaff="removeStaff"
    @removeStaffs="removeStaffs"
    :staffSelected="staffSelected"
    :rowStaff="rowStaff"
  />
</template> 
<script>
import BaseTablePaging from "../base/BaseTablePaging.vue";
import DialogStaffDetail from "../dialog/DialogStaffDetail.vue";
import BaseToast from "../base/BaseToast.vue";
import BasePopup from "../base/BasePopup.vue";
import axios from "axios";
import MISAEnum from "@/script/constants/MISAEnum";
import MISAResource from "@/script/constants/MISAResource";
import BaseApi from '../../script/constants/BaseApi'

export default {
  components: {
    BaseTablePaging,
    DialogStaffDetail,
    BaseToast,
    BasePopup,
  },
  props: [],
  data() {
    return {
      isHideLoader: false,
      isShowDialog: false,
      dialogName: "",
      isHideToast: true,
      toastTitle: "",
      toastText: "",
      isHidePopup: true,
      popupTitle: "",
      popupText: "",
      isHideDeleteButton: true,
      staffs: {},
      staffSelected: {},
      staffSelectedId: null,
      btnSaveMode: null,
      totalResult: null,
      totalPage: null,
      currentPage: null,
      pageSize: null,
      pageNumber: null,
      filterName: null,
      inputTextSearch: null,
      outputTextSearch: null,
      timeout: null,
      rowStaff: [],
      btnMoreType: "button-white",
      isCheckCheckbox: [],
      isCheckAll: false,
      downloadUrl: null,
    };
  },
  created() {
    this.loadData(25, 1, "");
    this.Resource = MISAResource;
  },
  watch: {
    /**
     * khi người dùng nhập xong thì thực hiện gọi api lấy dữ liệu theo từ khóa tìm kiếm
     * Author: VQBao - 27/7/2022
     */
    outputTextSearch: function (value) {
      try {
        this.loadData(25, 1, value);
      } catch (error) {
        console.log(error);
      }
    },
  },
  methods: {
    /**
     * khi người dùng nhập vào ô search input, sau khi ngừng 500ms thì lấy ra giá trị vừa nhập xong
     * Author: VQBao - 27/7/2022
     */
    searchByText() {
      clearTimeout(this.timeout);
      var self = this;
      this.timeout = setTimeout(function () {
        self.outputTextSearch = self.inputTextSearch;
      }, 500);
    },
    /**
     * thực hiện tải dữ liệu thông tin giáo viên và hiển thị trên bảng
     * Author: VQBao - 21/7/2022
     */
    loadData(pageSize, pageNumber, filterName) {
      this.staffSelected = {};
      this.staffSelectedId = null;
      this.pageSize = pageSize;
      this.pageNumber = pageNumber;
      this.filterName = filterName;
      this.isHideLoader = false;
      var me = this;
      // loadData khi chưa có thông tin tìm kiếm
      if (filterName == "") {
        try {
          axios
            .get(BaseApi.TeacherApi + "/filter?pageSize=" + me.pageSize + "&pageNumber=" + me.pageNumber)
            .then(function (respond) {
              // ẩn loading
              me.isHideLoader = true;
              me.staffs = respond.data.data;
              me.totalResult = respond.data.totalRecords;
              me.totalPage = respond.data.totalPages;
              me.currentPage = respond.data.pageNumber;
              me.isCheckAll = false;
              // ô checkbox ở các hàng đều bằng false
              for (let i = 0; i < me.staffs.length; i++) {
                me.isCheckCheckbox[i] = false;
              }
            })
            .catch(function (res) {
              console.log(res);
            });
        } catch (error) {
          me.isHideLoader = true;
          console.log(error);
        }
      }
      // loadData khi có từ khóa tìm kiếm
      else {
        try {
          axios
            .get(BaseApi.TeacherApi+"/filter?pageSize="+me.pageSize+"&pageNumber="+me.pageNumber+"&searchKey="+filterName)
            .then(function (respond) {
              // ẩn loading
              me.isHideLoader = true;
              me.staffs = respond.data.data;
              me.totalResult = respond.data.totalRecords;
              me.totalPage = respond.data.totalPages;
              me.currentPage = respond.data.pageNumber;
              me.isCheckAll = false;
              // ô checkbox ở các hàng đều bằng false
              for (let i = 0; i < me.staffs.length; i++) {
                me.isCheckCheckbox[i] = false;
              }
            })
            .catch(function (res) {
              console.log(res);
            });
        } catch (error) {
          this.isHideLoader = true;
          console.log(error);
        }
      }
    },
    /**
     * khi bấm thêm thì form chi tiết giáo viên hiện ra
     * Author: VQBao - 21/7/2022
     */
    addStaffOnClick() {
      // btnSaveMode = MISAEnum.EditMode.Add => thực hiện thêm mới
      this.btnSaveMode = MISAEnum.EditMode.Add;
      this.dialogName = MISAResource.DIALOG.InsertNewStaff;
      this.showStaffDialog(true);
      this.staffSelected = {};
      this.staffSelectedId = null;
      var me = this;
      // gọi api lấy mã nhân viên mới
      axios
        .get(BaseApi.TeacherApi + "/NewTeacherCode")
        .then((response) => {
          me.staffSelected.teacherCode = response.data;
        })
        .catch((err) => {
          console.error(err);
        });
    },
    /**
     * Thay đổi trạng thái ẩn hiện của form thêm mới
     * @param {Boolean} iShow - true: hiển thị, false: ẩn đi
     * Author: VQBao - 21/7/2022
     */
    showStaffDialog(isShow) {
      this.isShowDialog = isShow;
    },
    /**
     * Thay đổi trạng thái ẩn hiện của toast mesage
     * Author: VQBao - 23/7/2022
     */
    hideToastMessage(title, text) {
      var me = this;
      this.toastTitle = title;
      this.toastText = text;
      this.isHideToast = false;
      setTimeout(function () {
        me.isHideToast = true;
      }, 3000);
    },
    /**
     * Thay đổi trạng thái ẩn hiện của popup thông báo
     * Author: VQBao - 23/7/2022
     */
    hidePopup(isHide) {
      this.isHidePopup = isHide;
    },
    /**
     * Khi nhấn vào tên hoặc icon sửa trong bảng, hiện form thông tin chi tiết
     * Author: VQBao - 21/7/2022
     */
    rowOnClick(staff) {
      // btnSaveMode = MISAEnum.EditMode.Edit => thực hiện sửa
      this.btnSaveMode = MISAEnum.EditMode.Edit;
      this.staffSelected = staff;
      this.staffSelectedId = staff.teacherId;
      this.dialogName = MISAResource.DIALOG.EditStaff;
      this.showStaffDialog(true);
    },
    /**
     * khi bấm vào icon xóa thì hiển thị popup
     * Author: VQBao - 21/7/2022
     */
    removeStaffOnClick(staff) {
      this.staffSelected = staff;
      this.popupTitle = MISAResource.POPUP.Delete.Title;
      this.popupText = MISAResource.POPUP.Delete.DeleteTeacher;
      this.hidePopup(false);
    },
    /**
     * Thực hiện xóa giáo viên đang chọn
     * Author: VQBao - 21/7/2022
     */
    removeStaff(staff) {
      var me = this;
      me.isHideLoader = false;
      me.hidePopup(false);
      axios
        .delete(BaseApi.TeacherApi + `/${staff.teacherId}`)
        .then(() => {
          me.loadData(25, 1, "");
          me.hideToastMessage(MISAResource.TOAST.Delete.Title, MISAResource.TOAST.Delete.Message);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    /**
     * Thực hiện lấy object giáo viên khi tích chọn 1 hoặc nhiều checkbox
     * Author: VQBao - 22/7/2022
     */
    rowCheckboxSelected(staff, index) {
      this.staffSelected = staff;
      this.staffSelectedId = staff.teacherId;
      if (!this.isCheckCheckbox[index]) {
        this.isCheckCheckbox[index] = true;
        this.rowStaff.push(this.staffSelected);
        if (this.rowStaff.length == this.staffs.length) {
          this.isCheckAll = true;
        }
      } else {
        this.isCheckCheckbox[index] = false;
        let id = this.rowStaff.indexOf(staff);
        this.rowStaff.splice(id, 1);
        if (this.rowStaff.length != this.staffs.length) {
          this.isCheckAll = false;
        }
      }
    },
    /**
     * Khi bấm vào nút toolbar more thì hiện button xóa
     * Nếu có ít hơn 2 bản ghi được chọn thì disable button xóa nhiều
     * Author: VQBao - 4/8/2022
     */
    btnToolbarMoreOnclick() {
      if(this.isHideDeleteButton == true && this.rowStaff.length < 2){
        this.isHideDeleteButton = false;
        this.btnMoreType = "button-white--disable"
      }
      else if(this.isHideDeleteButton == true && this.rowStaff.length > 1){
        this.isHideDeleteButton = false;
        this.btnMoreType = "button-white"
      }
      else {
        this.isHideDeleteButton = true;
      }
    },
    /**
     * Thực hiện hiển thị popup xóa hàng loạt khi bấm chọn button xóa hàng loạt
     * Author: VQBao - 28/7/2022
     */
    removeStaffsOnClick() {
      if(this.rowStaff.length > 1){
        this.popupTitle = MISAResource.POPUP.Delete.Title;
        this.popupText = MISAResource.POPUP.Delete.DeleteMultiTeacher;
        this.hidePopup(false);
        this.isHideDeleteButton = true;
      } 
    },
    /**
     * Thực hiện xóa hàng loạt
     * Author: VQBao - 28/7/2022
     */
    removeStaffs(rowStaff) {
      for (let i = 0; i < rowStaff.length; i++) {
        this.removeStaff(rowStaff[i]);
      }
      this.rowStaff = [];
    },
    /**
     * Thực hiện check all các checkbox
     * Author: VQBao - 28/7/2022
     */
    checkAllCheckbox() {
      if (!this.isCheckAll) {
        this.rowStaff = [];
        this.isCheckAll = true;
        for (let i = 0; i < this.staffs.length; i++) {
          this.isCheckCheckbox[i] = true;
          this.rowStaff.push(this.staffs[i]);
        }
      } else {
        this.isCheckAll = false;
        for (let i = 0; i < this.staffs.length; i++) {
          this.isCheckCheckbox[i] = false;
        }
        this.rowStaff = [];
      }
    },
    /**
     * Thực hiện xuất danh sách giáo viên ra file excel
     * Author: VQBao - 10/8/2022
     */
    exportToExcel() {
      this.isHideLoader = false;
      axios({
        url: BaseApi.TeacherApi + "/ExportToExcel",
        method: "GET",
        responseType: "blob",
      }).then((response) => {
        var fileURL = window.URL.createObjectURL(new Blob([response.data]));
        var fileLink = document.createElement("a");

        fileLink.href = fileURL;
        fileLink.setAttribute("download", "Danh_sach_can_bo_giao_vien.xlsx");
        document.body.appendChild(fileLink);
        fileLink.click();
        this.isHideLoader = true;
      });
    },
  },
};
</script>
<style scoped>
input {
  min-width: 120px;
  height: 32px;
  outline: none;
  border: 1px solid;
  border-color: var(--button-border);
  border-radius: 4px;
  box-sizing: border-box;
  padding: 0 32px 0 16px;
}

input:focus {
  border-color: var(--input-focus-border-color);
}

input::placeholder {
  color: var(--button-grey-text);
}
.content-toolbar {
  height: 64px;
  width: 100%;
  position: relative;
  display: flex;
  align-items: center;
}

.input-search {
  width: 32px;
  height: 24px;
  position: absolute;
  left: 348px;
  border-left: 1px solid;
  border-color: var(--button-border);
  cursor: pointer;
  background-size: 24px 24px;
}

.toolbar-button {
  box-sizing: border-box;
  display: flex;
  align-items: center;
  position: absolute;
  right: 16px;
}

.toolbar-button button {
  margin-left: 8px;
}

.toolbar-more {
  background-color: #ffffff;
  width: 32px;
  height: 32px;
  background-size: 28px 28px;
  margin-left: 8px;
  border-radius: 4px;
  border: 1px solid;
  border-color: var(--button-border);
  box-sizing: border-box;
  cursor: pointer;
  position: relative;
}

.toolbar-more:hover {
  background-color: var(--background);
}

.hide-delete-button {
  display: none !important;
}

#btnDeleteMore {
  position: absolute;
  height: 40px;
  top: 40px;
  right: 0;
  z-index: 1;
}

button#btnDeleteMore {
  padding-left: 24px;
}

.page-table {
  position: relative;
  display: flex;
  flex-direction: column;
  align-content: stretch;
  flex: 1;
  justify-content: space-between;
  height: calc(100vh - 112px);
  width: 100%;
}

.content-table {
  width: 100%;
  flex: 1;
  flex-basis: 100px;
  height: calc(100vh - 168px);
  box-sizing: border-box;
  position: absolute;
  top: 0;
  left: 0;
  bottom: 56px;
  overflow: auto;
}

.toolbar-input {
  margin-left: 16px;
  width: 368px;
}

.col-full-name,
.col-checkbox {
  cursor: pointer;
}

/* table.css */
table {
  width: 100%;
  border-spacing: 0;
  box-sizing: border-box;
  padding-left: 16px;
  padding-right: 8px;
}

table thead {
  position: sticky;
  height: 64px;
  top: 0;
  height: 64px;
  background-color: var(--background);
  width: 100%;
  box-sizing: border-box;
}

table tbody {
  width: 100%;
}

table tr {
  height: 40px;
  box-sizing: border-box;
}

table tr,
td {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  max-width: 200px;
}

tbody tr:hover {
  background-color: var(--grid-line-focus-hover);
}

tbody .row-selected {
  background-color: var(--grid-line-focus-hover);
}

table tr td,
th {
  border-bottom: 1px solid #ccc;
  border-right: 1px solid #ccc;
  padding: 0 10px;
}

.col-staff-code {
  width: 120px;
}
.col-full-name {
  width: 220px;
}
.col-phone-number {
  width: 120px;
}
.col-department {
  min-width: 100px;
}
.col-subject {
  min-width: 160px;
}
.col-room {
  min-width: 160px;
}
.col-is-trained {
  width: 85px;
}
.col-is-working {
  width: 85px;
}
.col-button {
  width: 24px;
}

table .text-align-center {
  text-align: center;
}

.text-align-right {
  text-align: right;
}

.text-align-left {
  text-align: left;
}

tbody tr td:last-child,
thead tr th:last-child {
  border-right: none;
}
/* custom scroll*/
/* width */
.content-table::-webkit-scrollbar {
  width: 8px;
  height: 8px;
}

/* Track */
.content-table::-webkit-scrollbar-track {
  border-radius: 4px;
}

/* Handle */
.content-table::-webkit-scrollbar-thumb {
  background: var(--scroll-color);
  border-radius: 4px;
}

.content-table::-webkit-scrollbar-thumb:hover {
  background: var(--scroll-hover-color);
}

.cell-check {
  width: 16px;
  height: 16px;
  margin: auto;
}

.col-icon {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 40px;
  width: 100%;
  box-sizing: border-box;
}

.cell-edit-icon,
.cell-remove-icon {
  width: 28px;
  height: 28px;
  cursor: pointer;
}
</style>