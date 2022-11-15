using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA.EMIS.Common.Entities;
using MISA.EMIS.DAL.Interface;
using OfficeOpenXml;

namespace MISA.EMIS.DAL.Repository
{
    // Repository của giáo viên
    public class TeacherRepository: ITeacherRepository, IDisposable
    {
        #region Constructor
        protected readonly string connectionString = "";
        protected MySqlConnection connection;
        public TeacherRepository()
        {
            // khởi tạo kết nối với dataabse
            connectionString =
                "Host=3.0.89.182; " +
                "Port=3306; " +
                "Database=MISA.WEB06.VQBAO; " +
                "User Id=dev; " +
                "Password=12345678;";
            connection = new MySqlConnection(connectionString);
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        /// <summary>
        /// Đóng kết nối với database
        /// </summary>
        /// Author: VQBao - 8/8/2022
        public void Dispose()
        {
            connection.Dispose();
            connection.Close();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Thực hiện sửa giáo viên
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public int Update(Guid id, Teacher teacher)
        {
            // khởi tạo transaction
            using (var transaction = connection.BeginTransaction())
            {
                var storeProc = "Proc_UpdateTeacher";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@_TeacherId", id);
                parameters.Add("@_TeacherCode", teacher.TeacherCode);
                parameters.Add("@_FullName", teacher.FullName);
                parameters.Add("@_PhoneNumber", teacher.PhoneNumber);
                parameters.Add("@_Email", teacher.Email);
                parameters.Add("@_DepartmentID", teacher.DepartmentId);
                parameters.Add("@_IsTrained", teacher.IsTrained);
                parameters.Add("@_IsWorking", teacher.IsWorking);
                parameters.Add("@_QuitWorkDate", teacher.QuitWorkDate);
                var rowEffect = connection.Execute(storeProc, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                // Xóa các kho phòng cũ của giáo viên
                var roomDeleteProc = "Proc_DeleteTeacherRoom";
                var deleteRoom = connection.Execute(roomDeleteProc, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                // Xóa các môn học cũ của giáo viên
                var subjectDeleteProc = "Proc_DeleteTeacherSubject";
                var deleteSubject = connection.Execute(subjectDeleteProc, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                // Thêm mới các kho phòng của giáo viên vào bảng giáo viên-kho phòng
                var roomProc = "Proc_InsertTeacherRoom";
                DynamicParameters roomParam = new DynamicParameters();
                roomParam.Add("@_TeacherId", id);
                if(teacher.RoomId != null)
                {
                    for (int i = 0; i < teacher.RoomId.Count; i++)
                    {
                        roomParam.Add("@_RoomId", teacher.RoomId[i]);
                        var rowRoomEffect = connection.Execute(roomProc, param: roomParam, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                    }
                }
                // Thêm mới các môn học của giáo viên vào bảng giáo viên-môn học
                var subjectProc = "Proc_InsertTeacherSubject";
                DynamicParameters subjectParam = new DynamicParameters();
                subjectParam.Add("@_TeacherId", id);
                if(teacher.SubjectId != null)
                {
                    for (int i = 0; i < teacher.SubjectId.Count; i++)
                    {
                        subjectParam.Add("@_SubjectId", teacher.SubjectId[i]);
                        var rowSubjectEffect = connection.Execute(subjectProc, param: subjectParam, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                    }
                }
                
                transaction.Commit();
                return rowEffect;
            }
        }

        /// <summary>
        /// Kiểm tra trùng mã giáo viên
        /// </summary>
        /// <param name="teacherCode">Mã giáo viên</param>
        /// <returns>true - Đã tồn tại mã giáo viên; false - Chưa tồn tại mã giáo viên</returns>
        /// Author: VQBao - 4/8/2022
        public bool IsDuplicateTeacherCode(string teacherCode)
        {
            var sqlCheck = $"SELECT TeacherCode FROM Teacher WHERE TeacherCode = @code ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@code", teacherCode);
            var res = connection.QueryFirstOrDefault(sqlCheck, param: parameters);
            if(res == null)
            {
                return false;
            } else
            {
                return true;
            }
        }

        /// <summary>
        /// Thực hiện lấy toàn bộ dữ liệu giáo viên từ bảng database
        /// </summary>
        /// <returns>Danh sách giáo viên</returns>
        /// Author: VQBao - 8/8/2022
        public IEnumerable<Teacher> GetAll()
        {
            var storeProc = "Proc_GetAllTeacher";
            var data = connection.Query<Teacher>(storeProc, commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }
        /// <summary>
        /// lấy dữ liệu giáo viên theo phân trang
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi 1 trang</param>
        /// <param name="pageNumber">Số thứ tự trang muốn hiển thị</param>
        /// <param name="searchKey">Từ khóa tìm kiếm</param>
        /// <returns>Object bao gồm các thông tin: tổng số bản ghi, tổng số trang, Số lượng bản ghi 1 trang, Số thứ tự trang muốn hiển thị, danh sách giáo viên</returns>
        public Object GetTeacherPaging(int pageSize, int pageNumber, string? searchKey)
        {
            var storeProc = "Proc_GetTeacherPaging";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@_PageSize", pageSize);
            parameters.Add("@_PageNumber", pageNumber);
            parameters.Add("@_SearchKey", searchKey);
            parameters.Add("@_TotalRecords", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add("@_TotalPages", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var data = connection.Query<Teacher>(storeProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            int totalRecords = parameters.Get<int>("@_TotalRecords");
            int totalPages = parameters.Get<int>("@_TotalPages");
            var respond = new
            {
                totalRecords = totalRecords,
                totalPages = totalPages,
                pageSize = pageSize,
                pageNumber = pageNumber,
                data = data,
            };
            return respond;
        }

        /// <summary>
        /// Thực hiện lấy giáo viên theo khóa chính
        /// </summary>
        /// <param name="id">Khóa chính của giáo viên muốn trả về</param>
        /// <returns>Object bao gôm thông tin giáo viên, phòng kho và môn học của giáo viên đó</returns>
        /// Author: VQBao - 8/8/2022
        public Object GetById(Guid id)
        {
            var teacherProc = "Proc_GetTeacherById";
            DynamicParameters teacherParam = new DynamicParameters();
            teacherParam.Add("@_TeacherId", id);
            var teacherData = connection.QueryFirstOrDefault<Teacher>(teacherProc, param: teacherParam, commandType: System.Data.CommandType.StoredProcedure);

            // lấy các kho phòng của giáo viên
            var roomProc = "Proc_GetRoomOfTeacher";
            DynamicParameters roomParam = new DynamicParameters();
            roomParam.Add("@_TeacherId", id);
            var roomData = connection.Query<Room>(roomProc, param: roomParam, commandType: System.Data.CommandType.StoredProcedure);

            // lấy các môn học của giáo viên
            var subjectProc = "Proc_GetSubjectOfTeacher";
            DynamicParameters subjectParam = new DynamicParameters();
            subjectParam.Add("@_TeacherId", id);
            var subjectData = connection.Query<Subject>(subjectProc, param: subjectParam, commandType: System.Data.CommandType.StoredProcedure);

            var res = new
            {
                Teacher = teacherData,
                Room = roomData,
                Subject = subjectData,
            };
            return res;
        }

        /// <summary>
        /// Thêm mới 1 giáo viên vào database
        /// </summary>
        /// <param name="teacher">Giáo viên cần thêm mới</param>
        /// <returns>1 - Thêm mới thành công, nếu không thì báo lỗi</returns>
        /// Author: VQBao - 8/8/2022
        public int Insert(Teacher teacher)
        {
            using (var transaction = connection.BeginTransaction())
            {
                Guid id = Guid.NewGuid();
                teacher.TeacherId = id;
                // Thêm mới giáo viên vào bảng giáo viên
                var storeProc = "Proc_InsertTeacher";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@_TeacherID", teacher.TeacherId);
                parameters.Add("@_TeacherCode", teacher.TeacherCode);
                parameters.Add("@_FullName", teacher.FullName);
                parameters.Add("@_PhoneNumber", teacher.PhoneNumber);
                parameters.Add("@_Email", teacher.Email);
                parameters.Add("@_DepartmentID", teacher.DepartmentId);
                parameters.Add("@_IsTrained", teacher.IsTrained);
                parameters.Add("@_IsWorking", teacher.IsWorking);
                parameters.Add("@_QuitWorkDate", teacher.QuitWorkDate);
                var rowTeacherEffect = connection.Execute(storeProc, param: parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                // Thêm mới các kho phòng của giáo viên vào bảng giáo viên-kho phòng
                var roomProc = "Proc_InsertTeacherRoom";
                DynamicParameters roomParam = new DynamicParameters();
                roomParam.Add("@_TeacherId", teacher.TeacherId);
                if (teacher.RoomId != null)
                {
                    for (int i = 0; i < teacher.RoomId.Count; i++)
                    {
                        roomParam.Add("@_RoomId", teacher.RoomId[i]);
                        var rowRoomEffect = connection.Execute(roomProc, param: roomParam, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                    }
                }
                // Thêm mới các môn học của giáo viên vào bảng giáo viên-môn học
                var subjectProc = "Proc_InsertTeacherSubject";
                DynamicParameters subjectParam = new DynamicParameters();
                subjectParam.Add("@_TeacherId", teacher.TeacherId);
                if (teacher.SubjectId != null)
                {
                    for (int i = 0; i < teacher.SubjectId.Count; i++)
                    {
                        subjectParam.Add("@_SubjectId", teacher.SubjectId[i]);
                        var rowSubjectEffect = connection.Execute(subjectProc, param: subjectParam, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                    }
                }
                transaction.Commit();
                return rowTeacherEffect;
            }
        }


        /// <summary>
        /// Thực hiện xóa 1 giáo viên trong database
        /// </summary>
        /// <param name="id">Khóa chính của giáo viên cần xóa</param>
        /// <returns>1 - Xóa thành công, không thì báo lỗi</returns>
        /// Author: VQBao - 8/8/2022
        public int Delete(Guid id)
        {
            using (var transaction = connection.BeginTransaction())
            {
                var storeProc = $"Proc_DeleteTeacher";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@_TeacherId", id);
                var data = connection.Execute(storeProc, param: parameters,transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                transaction.Commit();
                return data;
            }
            
        }
        /// <summary>
        /// Thực hiện lấy mã nhân viên mới = mã nhân viên cũ lớn nhất cộng 1
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: VQBao - 10/8/2022
        public string GetNewTeacherCode()
        {
            var storeProc = "Proc_GetNewTeacherCode";
            DynamicParameters parameters = new DynamicParameters();
            var data = connection.QuerySingle<string>(storeProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            string res = data.ToString();
            return res;
        }
        #endregion
    }
}
