using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.EMIS.Common.Entities;

namespace MISA.EMIS.DAL.Interface
{
    // interface giáo viên
    public interface ITeacherRepository
    {
        #region Methods
        // Lấy toàn bộ giáo viên
        IEnumerable<Teacher> GetAll();
        // Lấy giáo viên theo Id
        Object GetById(Guid id);
        // Kiểm tra trùng mã giáo viên
        bool IsDuplicateTeacherCode(string teacherCode);
        // Thêm giáo viên mới
        int Insert(Teacher teacher);
        // Sửa thông tin giáo viên
        int Update(Guid id, Teacher teacher);
        // Xóa giáo viên theo id
        int Delete(Guid id);
        // Lấy dữ liệu giáo viên phân trang
        Object GetTeacherPaging(int pageSize, int pageNumber, string? searchKey);
        // Lấy mã giáo viên mới
        string GetNewTeacherCode();
        #endregion
    }
}
