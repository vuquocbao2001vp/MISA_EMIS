<template>
  <BaseLoader :isHideLoader="isHideLoader" />
  <div class="dialog" v-if="isShow" :class="{ isShowDialog: isShow }">
    <div class="dialog-avatar">
      <div class="avatar-image icon-center icon-default-avatar"></div>
      <BaseButton
        class="avatar-button-picker"
        buttonName="Chọn ảnh"
        buttonType="button-green"
      />
      <span
        class="avatar-name"
        v-text="
          staff.fullName === '' || staff.fullName == null
            ? 'Họ và tên'
            : staff.fullName
        "
        :title="staff.fullName"
      ></span>
      <span
        class="avatar-code"
        v-text="
          staff.teacherCode === '' || staff.teacherCode == null
            ? 'Số hiệu cán bộ'
            : staff.teacherCode
        "
        :title="staff.teacherCode"
      ></span>
    </div>
    <div class="space"><div class="space-border"></div></div>
    <div class="dialog-content">
      <div
        @click="btnCloseOnClick"
        class="content-icon icon-center icon-X-2"
      ></div>
      <div class="dialog-body">
        <div class="dialog-body-text">{{dialogName}}</div>
        <div class="body-row">
          <div class="dialog-body-item body-col-left">
            <div class="body-item-text">
              {{ Resource.STAFF_INFO.StaffCode
              }}<span class="requiredInput">*</span>
            </div>
            <input
              :tabindex="1"
              type="text"
              class="body-item-input"
              v-model="staff.teacherCode"
              ref="staffCode"
              @blur="validateStaffCode"
              @mouseover="isShowError[0] = true"
              @mouseleave="isShowError[0] = false"
            />
            <div class="input-validate-error" v-if="errorCode!=''" :class="{'show-validate-error': isShowError[0]}">
              <div class="validate-error-arrow"></div>
              <span class="validate-error-text">{{errorCode}}</span>
            </div>
          </div>
          <div class="dialog-body-item body-col-right">
            <div class="body-item-text">
              {{ Resource.STAFF_INFO.FullName
              }}<span class="requiredInput">*</span>
            </div>
            <input
              :tabindex="2"
              type="text"
              class="body-item-input"
              ref="staffName"
              v-model="staff.fullName"
              @blur="validateFullName"
              @mouseover="isShowError[1] = true"
              @mouseleave="isShowError[1] = false"
            />
            <div class="input-validate-error" v-if="errorName!=''" :class="{'show-validate-error': isShowError[1]}">
              <span class="validate-error-text">{{errorName}}</span>
              <div class="validate-error-arrow"></div>
            </div>
          </div>
        </div>
        <div class="body-row">
          <div class="dialog-body-item body-col-left">
            <div class="body-item-text">
              {{ Resource.STAFF_INFO.PhoneNumber }}
            </div>
            <input
              :tabindex="3"
              type="text"
              class="body-item-input"
              ref="staffPhone"
              v-model="staff.phoneNumber"
              @blur="validatePhoneNumber"
              @mouseover="isShowError[2] = true"
              @mouseleave="isShowError[2] = false"
            />
            <div class="input-validate-error" v-if="errorPhone!=''" :class="{'show-validate-error': isShowError[2]}">
              <div class="validate-error-arrow"></div>
              <div class="validate-error-text">{{errorPhone}}</div>
            </div>
          </div>
          <div class="dialog-body-item body-col-right">
            <div class="body-item-text">{{ Resource.STAFF_INFO.Email }}</div>
            <input
              :tabindex="4"
              type="text"
              class="body-item-input"
              ref="staffEmail"
              v-model="staff.email"
              @blur="validateEmail"
              @mouseover="isShowError[3] = true"
              @mouseleave="isShowError[3] = false"
            />
            <div class="input-validate-error" v-if="errorEmail!=''" :class="{'show-validate-error': isShowError[3]}">
              <div class="validate-error-text">{{errorEmail}}</div>
              <div class="validate-error-arrow"></div>
            </div>
          </div>
        </div>
        <div class="body-row">
          <div class="dialog-body-item body-col-left">
            <div class="body-item-text">
              {{ Resource.STAFF_INFO.Department }}
            </div>
            <div class="select-box">
              <DxSelectBox
                :search-enabled="true"
                :items="departments"
                display-expr="departmentName"
                value-expr="departmentId"
                placeholder=""
                v-model:value="staff.departmentId"
                :tabIndex="5"
                noDataText="Không có dữ liệu"
              />
            </div>
          </div>
          <div class="dialog-body-item body-col-right">
            <div
              class="body-item-text"
              :title="Resource.TITLE.SubjectManagement"
            >
              {{ Resource.STAFF_INFO.Subject }}
            </div>
            <div class="tag-box">
              <DxTagBox
                :search-enabled="true"
                :show-selection-controls="true"
                :max-display-tag="3"
                :multiline="false"
                :showDropDownButton="true"
                :items="subjects"
                display-expr="subjectName"
                value-expr="subjectId"
                placeholder=""
                :tabIndex="6"
                selectAllText="Tất cả"
                noDataText="Không có dữ liệu"
                v-model:value="subjectIdList"
              />
            </div>
          </div>
        </div>
        <div class="body-row">
          <div class="dialog-body-item body-col-left">
            <div class="body-item-text" :title="Resource.TITLE.RoomManagement">
              {{ Resource.STAFF_INFO.Room }}
            </div>
            <div class="tag-box tag-box-room">
              <DxTagBox
                :search-enabled="true"
                :show-selection-controls="true"
                :max-display-tag="3"
                :multiline="false"
                :showDropDownButton="true"
                :items="rooms"
                display-expr="roomName"
                value-expr="roomId"
                placeholder=""
                :tabIndex="7"
                selectAllText="Tất cả"
                noDataText="Không có dữ liệu"
                v-model:value="roomIdList"
              />
            </div>
          </div>
        </div>
        <div class="body-row">
          <div class="dialog-body-item">
            <BaseCheckbox
              :tabindex="8"
              :class="isTrained ? 'checkbox-active' : 'checkbox-inactive'"
              @click="checkIsTrained"
              @keyup.space="checkIsTrained"
              @keyup.enter="checkIsTrained"
            />
            <div
              class="body-item-text row-item-checkbox"
              :title="Resource.TITLE.TrainedQualify"
            >
              {{ Resource.STAFF_INFO.TrainingQualification }}
            </div>
          </div>
          <div class="dialog-body-item">
            <BaseCheckbox
              :tabindex="9"
              :class="isWorking ? 'checkbox-active' : 'checkbox-inactive'"
              @click="checkIsWorking"
              @keyup.space="checkIsWorking"
              @keyup.enter="checkIsWorking"
            />
            <div class="body-item-text row-item-checkbox">
              {{ Resource.STAFF_INFO.IsWorking }}
            </div>
          </div>
          <div
            class="dialog-body-item"
            style="position: absolute; right: 24px"
            v-show="!isWorking"
          >
            <div class="body-item-text" style="margin-right: 8px">
              {{ Resource.STAFF_INFO.DayQuitWork }}
            </div>
            <div class="day-quit-work">
              <DxDateBox
                v-model:value="quitWorkDate"
                type="date"
                :tabIndex="10"
                display-format="dd/MM/yyyy"
                invalidDateMessage="Ngày nghỉ việc không đúng định dạng"
                :use-mask-behavior="true"
                placeholder="dd/mm/yyyy"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="dialog-footer">
        <BaseButton
          :tabindex="12"
          id="btnClose"
          buttonName="Đóng"
          buttonType="button-white"
          @click="btnCloseOnClick"
        />
        <BaseButton
          :tabindex="11"
          id="btnSave"
          buttonName="Lưu"
          buttonType="button-green"
          @click="saveStaffOnClick"
        />
      </div>
    </div>
  </div>
  <BasePopupChange
    :isHidePopup="isHidePopup"
    :title="popupTitle"
    :text="popupText"
    :btnSaveMode="btnSaveMode"
    @hidePopup="hidePopup"
    @hideDialog="hideDialog"
    @saveStaffOnClick="saveStaffOnClick"
    @showToast="showToast"
    @rollBackOldTeacher="rollBackOldTeacher"
  />
</template>
<script>
import axios from "axios";
import MISAEnum from "@/script/constants/MISAEnum.js";
import MISAResource from "../../script/constants/MISAResource.js";
import DxSelectBox from "devextreme-vue/select-box";
import DxTagBox from "devextreme-vue/tag-box";
import DxDateBox from "devextreme-vue/date-box";
import BasePopupChange from "../base/BasePopupChange.vue";
import BaseApi from '../../script/constants/BaseApi'
export default {
  components: {
    DxSelectBox,
    DxTagBox,
    DxDateBox,
    BasePopupChange,
  },
  props: ["isShow", "staffSelected", "staffSelectedId", "btnSaveMode", "dialogName"],
  data() {
    return {
      isHideLoader: true,
      isHidePopup: true,
      popupTitle: "",
      popupText: "",
      staff: {},
      isTrained: null,
      isWorking: null,
      quitWorkDate: null,
      departments: {},
      subjects: {},
      subjectIdList: [],
      teacherSubject: {},
      rooms: {},
      roomIdList: [],
      teacherRoom: {},
      isChangeData: false,
      oldTeacher: {},
      errorCode: "",
      errorName: "",
      errorPhone: "",
      errorEmail: "",
      isShowError: [],
    };
  },
  created() {
    this.Resource = MISAResource;
    var me = this;
    /**
     * Gọi api lấy dữ liệu các môn học
     * Author: VQBao - 15/8/2022
     */
    axios
      .get(BaseApi.SubjectApi)
      .then(function (response) {
        me.subjects = response.data;
      })
      .catch(function (err) {
        console.log(err);
      });
    /**
     * Gọi api lấy dữ liệu các tổ bộ môn
     * Author: VQBao - 15/8/2022
     */
    axios
      .get(BaseApi.DepartmentApi)
      .then(function (response) {
        me.departments = response.data;
      })
      .catch(function (err) {
        console.log(err);
      });
    /**
     * Gọi api lấy dữ liệu các kho phòng
     * Author: VQBao - 15/8/2022
     */
    axios
      .get(BaseApi.RoomApi)
      .then(function (response) {
        me.rooms = response.data;
      })
      .catch(function (err) {
        console.log(err);
      });
  },
  watch: {
    isShow: function (value) {
      // khi form thông tin chi tiết mở ra, focus vào ô input đầu tiên
      if (value == true) {
        this.$nextTick(() => {
          this.$refs.staffCode.focus();
        });
        // khi mở form, reset lại các trường thông tin
        this.errorCode = "";
        this.errorName = "";
        this.errorPhone = "";
        this.errorEmail = "";
        if (this.btnSaveMode == MISAEnum.EditMode.Add) {
          this.isTrained = false;
          this.isWorking = false;
          this.quitWorkDate = null;
          this.roomIdList = [];
          this.subjectIdList = [];
        }
        this.isChangeData = false;
      }
    },
    staffSelected: function (value) {
      this.staff = value;
    },
    staffSelectedId: function (value) {
      var me = this;
      // biến danh sách các id của môn học, kho phòng
      var subjectList = [];
      var roomList = [];
      if (value) {
        /**
         * gọi api lấy dữ liệu thông tin chi tiết 1 giáo viên
         * Author: VQBao - 21/7/2022
         */
        axios
          .get(BaseApi.TeacherApi + `/${me.staffSelectedId}`)
          .then(function (response) {
            switch (response.status) {
              case 200:
                me.staff = response.data.teacher;
                me.isTrained = response.data.teacher.isTrained;
                me.isWorking = response.data.teacher.isWorking;
                me.quitWorkDate = response.data.teacher.quitWorkDate;

                me.teacherRoom = response.data.room;
                me.teacherSubject = response.data.subject;
                for (let i = 0; i < me.teacherSubject.length; i++) {
                  subjectList.push(me.teacherSubject[i].subjectId);
                }
                me.subjectIdList = subjectList;
                for (let i = 0; i < me.teacherRoom.length; i++) {
                  roomList.push(me.teacherRoom[i].roomId);
                }
                me.roomIdList = roomList;

                // xây dựng object lưu thông tin khi api trả về chưa thay đổi
                me.oldTeacher = {
                    teacherCode: response.data.teacher.teacherCode,
                    fullName: response.data.teacher.fullName,
                    phoneNumber: response.data.teacher.phoneNumber,
                    email: response.data.teacher.email,
                    departmentId: response.data.teacher.departmentId,
                    isTrained: response.data.teacher.isTrained,
                    isWorking: response.data.teacher.isWorking,
                    quitWorkDate: response.data.teacher.quitWorkDate,
                  
                  roomList: me.roomIdList,
                  subjectList: me.subjectIdList,
                };
                break;
            }
          })
          .catch(function (res) {
            switch (res.status) {
              case 404:
                console.log("404 Not Found");
                break;
            }
          });
      }
    },
  },
  methods: {
    /**
     * Hiển thị toast thành công
     * Author: VQBao - 15/8/2022
     */
    showToast() {
      if (this.btnSaveMode == MISAEnum.EditMode.Edit) {
        this.$emit(
          "hideToastMessage",
          MISAResource.TOAST.Edit.Title,
          MISAResource.TOAST.Edit.Message
        );
      } else {
        this.$emit(
          "hideToastMessage",
          MISAResource.TOAST.Add.Title,
          MISAResource.TOAST.Add.Message
        );
      }
    },
    /**
     * Ẩn form thông tin chi tiết
     * Author: VQBao - 15/8/2022
     */
    hideDialog() {
      this.$emit("isShowStaffDialog", false);
    },
    /**
     * Ẩn popup thông báo
     * Author: VQBao - 15/8/2022
     */
    hidePopup(isHide) {
      this.isHidePopup = isHide;
    },
    /**
     * khi click vào nút đóng thì đóng form giáo viên chi tiết
     * Author: VQBao - 21/7/2022
     */
    btnCloseOnClick() {
      // nếu đang sửa thì check thôn tin hiện tại có thay đổi so với thông tin ban đầu không, nếu có thì hiển thị popup
      if (this.btnSaveMode == MISAEnum.EditMode.Edit) {
        var isRoomChange = false;
        var countRoom = 0;
        var countSubject = 0;
        if(this.oldTeacher.roomList.length != this.roomIdList.length) isRoomChange=true;
        else {
          for(let i=0; i<this.oldTeacher.roomList.length; i++){
            for(let j=0; j<this.oldTeacher.roomList.length; j++){
              if(this.oldTeacher.roomList[i] == this.roomIdList[j]) countRoom++;
            }
          }
          if(countRoom < this.oldTeacher.roomList.length) isRoomChange=true;
        }
        if(this.oldTeacher.subjectList.length != this.subjectIdList.length) isRoomChange=true;
        else {
          for(let i=0; i<this.oldTeacher.subjectList.length; i++){
            for(let j=0; j<this.oldTeacher.subjectList.length; j++){
              if(this.oldTeacher.subjectList[i] == this.subjectIdList[j]) countSubject++;
            }
          }
          if(countSubject < this.oldTeacher.subjectList.length) isRoomChange=true;
        }
        if (
          this.oldTeacher.teacherCode != this.staff.teacherCode ||
          this.oldTeacher.fullName != this.staff.fullName ||
          ((this.oldTeacher.phoneNumber != this.staff.phoneNumber) && this.oldTeacher.phoneNumber != null) ||
          ((this.oldTeacher.email != this.staff.email) && this.oldTeacher.email!= null) ||
          this.oldTeacher.departmentId != this.staff.departmentId ||
          this.oldTeacher.isTrained != this.isTrained ||
          this.oldTeacher.isWorking != this.isWorking ||
          this.oldTeacher.quitWorkDate != this.quitWorkDate ||
          isRoomChange == true
        ) {
          this.popupTitle = MISAResource.POPUP.Edit.Title;
          this.popupText = MISAResource.POPUP.Edit.IsChange;
          this.isHidePopup = false;
        } else {
          this.$emit("isShowStaffDialog", false);
        }
      }
      // nếu đang thêm mới thì kiểm tra có trường thông tin nào khác rỗng không, nếu có thì hiển thị popup
      else {
        var isChange = false;
        for(let key in this.staff){
          if(this.staff[key] != "") isChange=true;
        }
        if (
          isChange==true ||
          this.isTrained == true ||
          this.isWorking == true ||
          this.quitWorkDate != null ||
          this.roomIdList.length > 0 ||
          this.subjectIdList.length > 0
        ) {
          this.popupTitle = MISAResource.POPUP.Edit.Title;
          this.popupText = MISAResource.POPUP.Edit.IsChange;
          this.isHidePopup = false;
        } else {
          this.$emit("isShowStaffDialog", false);
        }
      }
    },
    /**
     * Thực hiện quay lại giá trị ban đầu khi không đồng ý sửa thông tin giáo viên
     * Author: VQBao - 15/8/2022
     */
    rollBackOldTeacher() {
      this.staff.teacherCode = this.oldTeacher.teacherCode;
      this.staff.fullName = this.oldTeacher.fullName;
      this.staff.phoneNumber = this.oldTeacher.phoneNumber;
      this.staff.email = this.oldTeacher.email;
      this.staff.departmentId = this.oldTeacher.departmentId;
      this.staff.isTrained = this.oldTeacher.isTrained;
      this.staff.isWorking = this.oldTeacher.isWorking;
      this.quitWorkDate = this.oldTeacher.quitWorkDate;
      this.roomIdList = this.oldTeacher.roomList;
      this.subjectIdList = this.oldTeacher.subjectList;
    },
    /**
     * Thực hiện cất dữ liệu
     * Author: VQBao - 21/7/2022
     */
    saveStaffOnClick() {
      var me = this;
      if (me.isTrained) {
        me.staff.isTrained = 1;
      } else {
        me.staff.isTrained = 0;
      }
      if (me.isWorking) {
        me.staff.isWorking = 1;
      } else {
        me.staff.isWorking = 0;
      }
      me.staff.roomId = me.roomIdList;
      me.staff.subjectId = me.subjectIdList;
      // 1. validate dữ liệu, nếu dữ liệu hợp lệ thì gọi api thêm mới giáo viên
      var isValid = me.validateStaffData();
      if (isValid) {
        me.isHideLoader = false;
        // 2. gọi api thực hiên thêm mới dữ liệu
        // nếu saveMode là Add thì thực hiện thêm mới, không thì thực hiện sửa
        if (this.btnSaveMode == MISAEnum.EditMode.Add) {
          if(me.quitWorkDate != null){
            me.quitWorkDate.setDate(me.quitWorkDate.getDate() + 1);
            me.staff.quitWorkDate = me.quitWorkDate;
          }
          axios
            .post(BaseApi.TeacherApi, me.staff)
            .then(function () {
              // ẩn loading
              me.isHideLoader = true;
              me.isHidePopup = true;
              // tắt form sửa thông tin
              me.$emit("isShowStaffDialog", false);
              // load lại dữ liệu của bảng
              me.$emit("loadData", 25, 1, "");
              // hiển thị toast message thành công
              me.$emit(
                "hideToastMessage",
                MISAResource.TOAST.Add.Title,
                MISAResource.TOAST.Add.Message
              );
              
            })
            .catch(function (err) {
              if(err.response.data.errorMsg == "Mã giáo viên đã tồn tại trong hệ thống"){
                me.$refs.staffCode.classList.add("input-error");
                me.errorCode = MISAResource.ERROR.DuplicatedCode;
                me.$nextTick(() => {me.$refs.staffCode.focus()});
                me.isHideLoader = true;
              } else {
                me.isHideLoader = true;
              }
            });
        } else {
          if(me.staffSelected.quitWorkDate == null && me.quitWorkDate != null){
            me.quitWorkDate.setDate(me.quitWorkDate.getDate() + 1);
            me.staff.quitWorkDate = me.quitWorkDate;
          } else {
            me.staff.quitWorkDate = me.quitWorkDate;
          }
          axios
            .put(BaseApi.TeacherApi + `/${me.staffSelectedId}`, me.staff)
            .then(function () {
              // ẩn loading
              me.isHideLoader = true;
              // tắt form sửa thông tin
              me.$emit("isShowStaffDialog", false);
              // load lại dữ liệu của bảng
              me.$emit("loadData", 25, 1, "");
              // hiển thị toast message thành công
              me.$emit(
                "hideToastMessage",
                MISAResource.TOAST.Edit.Title,
                MISAResource.TOAST.Edit.Message
              );
            })
            .catch(function (err) {
              if(err.response.data.errorMsg == "Mã giáo viên đã tồn tại trong hệ thống"){
                me.$refs.staffCode.classList.add("input-error");
                me.errorCode = MISAResource.ERROR.DuplicatedCode;
                me.$nextTick(() => {me.$refs.staffCode.focus()});
                me.isHideLoader = true;
              } else {
                me.isHideLoader = true;
              }
            });
        }
      } else {
        // focus vào input đầu tien bị lỗi
        if(me.errorCode != ''){
          me.$nextTick(() => {me.$refs.staffCode.focus()});
        } else {
          if(me.errorName != ''){
            me.$nextTick(() => {me.$refs.staffName.focus()});
          } else {
            if(me.errorPhone != '') {
              me.$nextTick(() => {me.$refs.staffPhone.focus()});
            } else {
              me.$nextTick(() => {me.$refs.staffEmail.focus()});
            }
          }
        }
      }
    },
    /**
     * Khi blur, nếu mã giáo viên để trống thì hiện lỗi
     * Author: VQBao - 27/7/2022
     */
    validateStaffCode() {
      if (!this.staff.teacherCode) {
        this.$refs.staffCode.classList.add("input-error");
        this.errorCode = MISAResource.ERROR.EmptyStaffCode;
      } 
      else if(this.staff.teacherCode.length > 20){
        this.$refs.staffCode.classList.add("input-error");
        this.errorCode = MISAResource.ERROR.LongStaffCode;
      }
      else {
        this.$refs.staffCode.classList.remove("input-error");
        this.errorCode = '';
        this.imageStaffCode = this.staff.teacherCode;
      }
    },
    /**
     * Khi blur khỏi input Họ tên, nếu họ tên trống hoặc quá dài thì báo lỗi
     * Author: VQBao - 27/7/2022
     */
    validateFullName() {
      if (!this.staff.fullName) {
        this.$refs.staffName.classList.add("input-error");
        this.errorName = MISAResource.ERROR.EmptyFullName;
      } 
      else if (this.staff.fullName.length > 30) {
        this.$refs.staffName.classList.add("input-error");
        this.errorName = MISAResource.ERROR.LongFullName;
      }
      else {
        this.$refs.staffName.classList.remove("input-error");
        this.errorName = '';
        this.imageStaffName = this.staff.fullName;
      }
    },
    /**
     * Khi blur, nếu email không đúng định dạng thì báo lỗi
     * Author: VQBao - 27/7/2022
     */
    validateEmail() {
      var email = event.currentTarget.value;
      if (email) {
        var emailFormat =
          /^([a-zA-Z0-9_\-.]+)@([a-zA-Z0-9_\-.]+)\.([a-zA-Z]{2,5})$/;
        if (!emailFormat.test(email)) {
          event.currentTarget.classList.add("input-error");
          this.errorEmail = MISAResource.ERROR.InValidEmail;
        } else {
          event.currentTarget.classList.remove("input-error");
          this.errorEmail = '';
        }
      } else {
        event.currentTarget.classList.remove("input-error");
        this.errorEmail = '';
      }
    },

    /**
     * Kiểm tra độ dài số điện thoại
     * Author: VQBao - 27/7/2022
     */
    validatePhoneNumber() {
      var phone = event.currentTarget.value;
      if ((phone.length < 10 && phone.length > 0) || phone.length > 11) {
        event.currentTarget.classList.add("input-error");
        this.errorPhone = MISAResource.ERROR.ShortPhoneLength;
      } else {
        event.currentTarget.classList.remove("input-error");
        this.errorPhone = '';
      }
    },
    /**
     * Thực hiện validate dữ liệu
     * Author: VQBao - 27/7/2022
     */
    validateStaffData() {
      var isValid = true;
      // validate mã giáo viên bỏ trống
      if (!this.staff.teacherCode) {
        isValid = false;
        this.$refs.staffCode.classList.add("input-error");
        this.errorCode = MISAResource.ERROR.EmptyStaffCode;
      }
      // validate mã giáo viên quá dài
      if (this.staff.teacherCode.length > 20) {
        isValid = false;
        this.$refs.staffCode.classList.add("input-error");
        this.errorCode = MISAResource.ERROR.LongStaffCode;
      }
      // validate họ và tên để trống
      if (!this.staff.fullName) {
        isValid = false;
        this.$refs.staffName.classList.add("input-error");        
        this.errorName = MISAResource.ERROR.EmptyFullName;
      }
      // validate họ tên quá dài
      if (this.staff.fullName.length > 30) {
        isValid = false;
        this.$refs.staffName.classList.add("input-error");        
        this.errorName = MISAResource.ERROR.LongFullName;
      }
      // validate email
      if (this.staff.email) {
        var email = this.staff.email;
        if (email) {
          var emailFormat =
            /^([a-zA-Z0-9_\-.]+)@([a-zA-Z0-9_\-.]+)\.([a-zA-Z]{2,5})$/;
          if (!emailFormat.test(email)) {
            isValid = false;
            this.$refs.staffEmail.classList.add("input-error");
            this.errorEmail = MISAResource.ERROR.InValidEmail;
          }
        }
      }
      // validate số điện thoại
      if (this.staff.phoneNumber) {
        var phone = this.staff.phoneNumber;
        if ((phone.length < 10 && phone.length > 0) || phone.length > 11) {
          isValid = false;
          this.$refs.staffPhone.classList.add("input-error");
          this.errorPhone = MISAResource.ERROR.ShortPhoneLength;
        }
      }           
      return isValid;
    },
    /**
     * Hàm check trình độ quản lý thiết bị
     * Author: VQBao - 28/7/2022
     */
    checkIsTrained() {
      this.isTrained = !this.isTrained;
    },
    /**
     * Hàm check trạng thái làm việc
     * Author: VQBao - 28/7/2022
     */
    checkIsWorking() {
      this.isWorking = !this.isWorking;
    },
  },
};
</script>
<style scoped>
.isShowDialog {
  display: flex !important;
}

.dialog {
  position: fixed;
  align-items: center;
  justify-content: center;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.24);
  display: none;
}

.dialog-avatar {
  width: 224px;
  height: 384px;
  background-color: var(--white-background);
  display: flex;
  flex-direction: column;
  align-items: center;
  box-sizing: border-box;
  border-color: var(--button-border);
}

.avatar-image {
  width: 160px;
  height: 160px;
  background-color: #ccc;
  margin-top: 24px;
  border-radius: 2px 2px 0 0;
  box-sizing: border-box;
  background-size: 160px 160px;
}

.img-avatar {
  width: 160px;
  height: 160px;
}

.avatar-button-picker {
  width: 160px;
  box-sizing: border-box;
}

.avatar-name {
  margin-top: 16px;
  font-size: 16px;
  max-width: 180px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.avatar-code {
  font-size: 12px;
  margin-top: 4px;
  max-width: 140px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.dialog-content {
  position: relative;
  display: flex;
  flex-direction: column;
  background-color: var(--white-background);
}
.content-icon {
  position: absolute;
  width: 36px;
  height: 36px;
  top: 0;
  right: 0;
  cursor: pointer;
}
.dialog-body {
  width: 100%;
  display: flex;
  flex-direction: column;
}

.dialog-body-text {
  background-color: var(--white-backgound);
  font-family: OpenSans-Semibold;
  font-size: 20px;
  width: 400px;
  height: 26px;
  margin-top: 18px;
  margin-left: 32px;
}

.dialog-body-item {
  display: flex;
  align-items: center;
  margin-left: 24px;
  margin-top: 24px;
  height: 32px;
  box-sizing: border-box;
  position: relative;
}

.body-item-text {
  color: #8d8d8d;
  display: flex;
  align-items: center;
}

.body-item-input {
  width: 200px;
}

.show-validate-error {
  display: flex !important;
}
.input-validate-error {
  position: absolute;
  z-index: 2;
  display: none;
  align-items: center;
}

.validate-error-arrow {
  width: 16px;
  height: 16px;
  transform: rotate(45deg);
  background-color: var(--delete-icon-hover);
}

.validate-error-text {
  color: var(--white-text);
  background-color: var(--delete-icon-hover);
  border-radius: 4px;
  padding-left: 8px;
  padding-right: 8px;
  display: flex;
  align-items: center;
  height: 28px;
  z-index: 3;
  width: max-content;
}

.body-col-left .input-validate-error {
  left: 306px;
}

.body-col-left .input-validate-error .validate-error-text {
  margin-left: -14px;
}

.body-col-right .input-validate-error {
  right: 202px;
}

.body-col-right .input-validate-error .validate-error-arrow {
  margin-left: -14px;
}

.body-row {
  width: 100%;
  display: flex;
  margin-right: 24px;
  position: relative;
}

.body-col-left .body-item-text {
  width: 104px;
}

.body-col-right .body-item-text {
  width: 86px;
}

.row-item-checkbox {
  color: var(--button-black-text);
  margin-left: 8px;
}

.dialog-footer {
  width: 100%;
  height: 60px;
  display: flex;
  position: relative;
}

#btnPickImage {
  border-radius: 0 0 2px 2px;
}

#btnClose {
  position: absolute;
  right: 128px;
  top: 14px;
}

#btnSave {
  position: absolute;
  right: 24px;
  top: 14px;
}

.day-quit-work {
  width: 150px;
  box-sizing: border-box;
}

#btnDialogClose {
  width: 24px;
  height: 24px;
  position: absolute;
  right: 8px;
  top: 8px;
  cursor: pointer;
}

.requiredInput {
  color: red;
  margin-left: 4px;
}

.input-error {
  border-color: var(--delete-icon-hover);
}

input {
  min-width: 120px;
  height: 32px;
  outline: none;
  border: 1px solid #cccccc;
  border-radius: 4px;
  box-sizing: border-box;
  padding: 0 32px 0 16px;
}

input:focus {
  border-color: #08bf1e;
}

input::placeholder {
  color: var(--button-grey-text);
}

.select-box {
  width: 200px;
  box-sizing: border-box;
}
.tag-box {
  width: 200px;
  box-sizing: border-box;
}

.tag-box-room {
  width: 510px;
}

.space {
  height: 384px;
  width: 2px;
  background-color: #fff;
  display: flex;
  align-items: center;
  box-sizing: border-box;
}
.space-border {
  height: 336px;
  width: 2px;
  background-color: var(--button-border);
}
</style>