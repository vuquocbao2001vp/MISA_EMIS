const MISAResource = {
    STAFF_INFO: {
        StaffCode: "Số hiệu cán bộ",
        FullName: "Họ và tên",
        PhoneNumber: "Số điện thoại",
        Email: "Email",
        Department: "Tổ chuyên môn",
        Subject: "QL theo môn",
        Room: "QL kho, phòng",
        IsTrained: "Đào tạo QLTB",
        IsWorking: "Đang làm việc",
        TrainingQualification: "Trình độ nghiệp vụ QLTB",
        DayQuitWork: "Ngày nghỉ việc"
    },
    DIALOG: {
        InsertNewStaff: "Thêm hồ sơ Cán bộ, giáo viên",
        EditStaff: "Sửa hồ sơ Cán bộ, giáo viên"
    },
    ERROR: {
        EmptyStaffCode: "Số hiệu cán bộ không được phép để trống",
        LongStaffCode: "Số hiệu cán bộ quá dài",
        EmptyFullName: "Họ và tên không được phép để trống",
        LongFullName: "Họ và tên quá dài",
        InValidEmail: "Email không đúng định dạng",
        ShortPhoneLength: "Số điện thoại không đúng độ dài",
        DuplicatedCode: "Số hiệu cán bộ đã tồn tại trong hệ thống",
        UserMessage: "Có lỗi, vui lòng liên hệ MISA để được hỗ trợ"
    },
    HEADER: {
        HeaderTitle: "Cán bộ, giáo viên",
        UserName: "Admin"
    }, 
    MENU: {
        MenuHeader: "Thiết bị",
    },
    TITLE: {
        SubjectManagement: "Quản lý theo môn",
        RoomManagement: "Quản lý kho, phòng",
        TrainingQualify: "Đào tạo quản lý thiết bị",
        TrainedQualify: "Trình độ nghiệp vụ quản lý thiết bị"
    },
    TOAST: {
        Add : {
            Title: "Thành công",
            Message: "Thêm giáo viên thành công",
        },
        Edit: {
            Title: "Thành công",
            Message: "Sửa giáo viên thành công",
        },
        Delete: {
            Title: "Thành công",
            Message: "Sao chép dữ liệu thành công"
        }
    },
    POPUP: {
        Delete: {
            Title: "Thông báo",
            DeleteTeacher: "Bạn có chắc chắn muốn xóa giáo viên đang chọn không?",
            DeleteMultiTeacher: "Bạn có chắc chắn muốn xóa những giáo viên đang chọn không?"
        },
        Edit: {
            Title: "Thông báo",
            IsChange: "Dữ liệu đã thay đổi, bạn có muốn lưu lại không?"
        }
    },
    LOADER: {
        LoadingText: "Đang tải"
    }

};
export default MISAResource