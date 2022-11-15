using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.EMIS.Common.Entities
{
    // Class giáo viên
    public class Teacher: BaseEntity
    {
        #region Constructor
        // Id giáo viên
        public Guid TeacherId { get; set; }
        // Mã giáo viên
        public string TeacherCode { get; set; }
        // Họ và tên giáo viên
        public string FullName { get; set; }
        // Số điện thoại
        public string? PhoneNumber { get; set; }
        // Email giáo viên
        public string? Email { get; set; }
        // Id môn học của giáo viên
        public Guid? DepartmentId { get; set; }
        // Tên tổ bộ môn
        public String? DepartmentName { get; set; }
        // Mảng id môn học
        public List<string>? SubjectId { get; set; } 
        // Tên môn học
        public String? SubjectName { get; set; }
        // Mảng id kho phòng
        public List<string>? RoomId { get; set; }
        // Tên phòng ban
        public String? RoomName { get; set; }
        // Trình độ nghiệp vụ quản lý thiết bị
        public int? IsTrained { get; set; }
        // Tình trạng làm việc
        public int? IsWorking { get; set; }
        // Ngày nghỉ việc
        public DateTime? QuitWorkDate { get; set; }

        #endregion
    }
}
