using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.EMIS.Common.Entities;
using MISA.EMIS.Common;
using MISA.EMIS.DAL.Interface;
using MISA.EMIS.BLL.Interface;
using System.Text.RegularExpressions;

namespace MISA.EMIS.BLL.Services
{
    /// <summary>
    /// Xử lý nghiệp vụ của giáo viên
    /// </summary>
    public class TeacherService: ITeacherService
    {
        #region Constructor
        ITeacherRepository repository;
        public TeacherService(ITeacherRepository _repository)
        {
            repository = _repository;
        }
        #endregion

        #region Methods
        public int InsertTeacherService(Teacher teacher)
        {
            var listMsgErrors = new List<string>();
            // xử lý nghiệp vụ
            // Mã giáo viên không được phép để trống
            if (string.IsNullOrEmpty(teacher.TeacherCode))
            {
                listMsgErrors.Add(MISA.EMIS.Common.Resource.ResourceText.Error_EmptyTeacherCode);
            }
            // Họ và tên không được phép để trống
            if (string.IsNullOrEmpty(teacher.FullName))
            {
                listMsgErrors.Add(MISA.EMIS.Common.Resource.ResourceText.Error_EmptyFullName);
            }
            // Email không đúng định dạng
            if (teacher.Email != null && teacher.Email != "" && !IsEmail(teacher.Email))
            {
                listMsgErrors.Add(MISA.EMIS.Common.Resource.ResourceText.Error_NotEmail);
            }

            // Mã giáo viên không được phép trùng
            var isDuplicate = repository.IsDuplicateTeacherCode(teacher.TeacherCode);
            if (isDuplicate == true)
            {
                listMsgErrors.Add(MISA.EMIS.Common.Resource.ResourceText.Error_DuplicateTeacherCode);
            }

            if (listMsgErrors.Count > 0)
            {
                throw new MISAException(listMsgs: listMsgErrors);
            }
            // Sau khi dữ liệu hợp lệ thì thêm mới vào database
            var result = repository.Insert(teacher);
            return result;
        }

        /// <summary>
        /// Xử lý nghiệp vụ khi thêm mới giáo viên
        /// </summary>
        /// <param name="id">Id của giáo viên</param>
        /// <param name="teacher">Giáo viên cần sửa thông tin</param>
        /// <returns>1-Sửa thành công, nếu không thì hiển thị lỗi</returns>
        /// <exception cref="MISAException">custom exception</exception>
        /// Author: VQBao - 10/8/2022
        public int UpdateTeacherService(Guid id, Teacher teacher)
        {
            var listMsgErrors = new List<string>();
            // xử lý nghiệp vụ
            // Mã giáo viên không được phép để trống
            if (string.IsNullOrEmpty(teacher.TeacherCode))
            {
                listMsgErrors.Add(MISA.EMIS.Common.Resource.ResourceText.Error_EmptyTeacherCode);
            }
            // Họ và tên không được phép để trống
            if (string.IsNullOrEmpty(teacher.FullName))
            {
                listMsgErrors.Add(MISA.EMIS.Common.Resource.ResourceText.Error_EmptyFullName);
            }
            // Mã giáo viên không được phép trùng
            // Lấy mã giáo viên khi chưa update để kiểm tra trùng mã
            var oldTeacher = repository.GetById(id);
            var oldTeacherCode = oldTeacher.GetType().GetProperty("Teacher").GetValue(oldTeacher, null);
            var code = oldTeacherCode.GetType().GetProperty("TeacherCode").GetValue(oldTeacherCode, null).ToString();
            if (teacher.TeacherCode != code)
            {
                var isDuplicate = repository.IsDuplicateTeacherCode(teacher.TeacherCode);
                if(isDuplicate)
                {
                    listMsgErrors.Add(MISA.EMIS.Common.Resource.ResourceText.Error_DuplicateTeacherCode);
                }
            }
            // Email không đúng định dạng
            if (teacher.Email != null && teacher.Email != "" && !IsEmail(teacher.Email))
            {
                listMsgErrors.Add(MISA.EMIS.Common.Resource.ResourceText.Error_NotEmail);
            }

            if (listMsgErrors.Count > 0)
            {
                throw new MISAException(listMsgs: listMsgErrors);
            }
            // Sau khi dữ liệu hợp lệ thì thêm mới vào database
            var result = repository.Update(id, teacher);
            return result;
        }

        /// <summary>
        /// Hàm kiểm tra email có hợp lệ hay không
        /// </summary>
        /// <param name="email">Tham số đầu vào muốn kiểm tra</param>
        /// <returns>true: là email, false: không là email</returns>
        /// Author: VQBao - 15/8/2020
        public Boolean IsEmail(string email)
        {
            Regex validateEmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (validateEmailRegex.IsMatch(email)) { return true; }
            else return false;
        }
        #endregion
    }
}
