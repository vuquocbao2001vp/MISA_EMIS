using MISA.EMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.EMIS.BLL.Interface
{
    /// <summary>
    /// interface xử lý nghiệp vụ của giáo viên
    /// </summary>
    public interface ITeacherService
    {
        #region Methods
        // service thêm mới giáo viên
        public int InsertTeacherService(Teacher teacher);
        // servive sửa thông tin giáo viên
        public int UpdateTeacherService(Guid id, Teacher teacher);
        // check email đúng định dạng
        Boolean IsEmail(string email);
        #endregion
    }
}
