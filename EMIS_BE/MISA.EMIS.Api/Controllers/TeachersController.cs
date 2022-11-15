using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.EMIS.Common.Entities;
using MISA.EMIS.Common;
using MISA.EMIS.DAL.Repository;
using MISA.EMIS.BLL.Services;
using MISA.EMIS.BLL.Interface;
using MISA.EMIS.DAL.Interface;
using OfficeOpenXml;
using System.Net;

namespace MISA.EMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        #region Constructor

        ITeacherService service;
        ITeacherRepository repository;
        public TeachersController(ITeacherService _service, ITeacherRepository _repository)
        {
            service = _service;
            repository = _repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Thực hiện lấy toàn bộ dữ liệu từ database
        /// </summary>
        /// <returns>Danh sách từ database</returns>
        /// Author: VQBao - 10/8/2022
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = repository.GetAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Phân trang danh sách giáo viên
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi 1 trang</param>
        /// <param name="pageNumber">Số thứ tự bản ghi hiển thị</param>
        /// <param name="searchKey">Từ khóa tìm kiếm</param>
        /// <returns>Object chưa các thông tin tổng số trang, tổng số bản ghi, danh sách giáo viên</returns>
        /// Author: VQBao - 10/8/2022
        [HttpGet("filter")]
        public Object Get(int pageSize, int pageNumber, string? searchKey)
        {
            try
            {
                var data = repository.GetTeacherPaging(pageSize, pageNumber, searchKey);
                return Ok(data);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo khóa chính
        /// </summary>
        /// <param name="id">Khóa chính của bản ghi</param>
        /// <returns>data-dữ liệu trả về từ database (code 200); nếu không thì báo lỗi</returns>
        /// Author: VQBao - 10/8/2022
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var data = repository.GetById(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Thêm mới 1 bản ghi vào database
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới và database</param>
        /// <returns>Mã 201 thành công, nếu không thì báo lỗi</returns>
        /// Author: VQBao - 4/8/2022
        [HttpPost]
        public IActionResult Post(Teacher teacher)
        {
            try
            {
                var dataTeacher = service.InsertTeacherService(teacher);
                return StatusCode(201, dataTeacher);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Thực hiện xóa 1 bản ghi theo khóa chính
        /// </summary>
        /// <param name="id">Khóa chính của bản ghi</param>
        /// <returns>Mã 201: xóa thành công, nếu không báo lỗi</returns>
        /// Author: VQBao - 4/8/2022
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var data = repository.Delete(id);
                return StatusCode(201, data);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        /// <summary>
        /// Sửa thông tin giáo viên
        /// </summary>
        /// <param name="id">Khóa chính giáo viên cần sửa</param>
        /// <param name="teacher">Thông tin sửa</param>
        /// <returns>1-Sửa thành công, nếu không thì hiển thị lỗi</returns>
        /// Author: VQBao - 4/8/2022
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Teacher teacher)
        {
            try
            {
                var respond = service.UpdateTeacherService(id, teacher);
                return StatusCode(200, respond);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã 200 + mã nhân viên mới, nếu không thì báo lỗi</returns>
        /// Author: VQBao - 10/8/2022
        [HttpGet("NewTeacherCode")]
        public IActionResult GetNewTeacherCode()
        {
            try
            {
                var respond = repository.GetNewTeacherCode();
                return StatusCode(200, respond);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xuất khẩu danh sách giáo viên ra file excel
        /// </summary>
        /// <returns>link download file excel</returns>
        /// Author: VQBao - 15/8/2022
        [HttpGet("ExportToExcel")]
        public async Task<IActionResult> GetExcelFile()
        {
            try
            {
                // Lấy danh sách giáo viên
                var teacherList = repository.GetAll();
                List<Teacher> teachers = new List<Teacher>();
                foreach (Teacher teacher in teacherList)
                {
                    teachers.Add(teacher);
                }

                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    // tạo worksheet để hiển thị bảng danh sách giáo viên
                    var workSheet = package.Workbook.Worksheets.Add("MISA_EMIS_Excel");

                    // Tạo tiêu đề của bảng
                    workSheet.Cells["A1:I1"].Merge = true;
                    workSheet.Cells["A1"].Value = "DANH SÁCH CÁN BỘ - GIÁO VIÊN";
                    workSheet.Cells["A1:I1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    workSheet.Cells["A1:I1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A1:I1"].Style.Font.Size = 16;
                    workSheet.Row(1).Height = 24;
                    workSheet.Row(2).Height = 24;
                    workSheet.Cells["A1"].Style.Font.Bold = true;
                    workSheet.Cells["A2:I2"].Merge = true;

                    List<string> listHeader = new List<string>()
                    {
                        "A3", "B3", "C3", "D3", "E3", "F3", "G3", "H3", "I3"
                    };

                    // Tạo border cho bảng
                    listHeader.ForEach(c =>
                    {
                        workSheet.Cells[c].Style.Font.Bold = true;
                        workSheet.Cells[c].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells[c].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells[c].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells[c].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells[c].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[c].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(211, 211, 211));

                    });
                    // Tạo tiêu đề cho các cột trường thông tin
                    workSheet.Cells[listHeader[0]].Value = MISA.EMIS.Common.Resource.ResourceText.Table_A3;
                    workSheet.Cells[listHeader[1]].Value = MISA.EMIS.Common.Resource.ResourceText.Table_B3;
                    workSheet.Cells[listHeader[2]].Value = MISA.EMIS.Common.Resource.ResourceText.Table_C3;
                    workSheet.Cells[listHeader[3]].Value = MISA.EMIS.Common.Resource.ResourceText.Table_D3;
                    workSheet.Cells[listHeader[4]].Value = MISA.EMIS.Common.Resource.ResourceText.Table_E3;
                    workSheet.Cells[listHeader[5]].Value = MISA.EMIS.Common.Resource.ResourceText.Table_F3;
                    workSheet.Cells[listHeader[6]].Value = MISA.EMIS.Common.Resource.ResourceText.Table_G3;
                    workSheet.Cells[listHeader[7]].Value = MISA.EMIS.Common.Resource.ResourceText.Table_H3;
                    workSheet.Cells[listHeader[8]].Value = MISA.EMIS.Common.Resource.ResourceText.Table_I3;

                    // điền thông tin của giáo viên vào các ô tương ứng trong bảng
                    for (int i = 0; i < teachers.Count; i++)
                    {
                        workSheet.Cells[i + 4, 1].Value = i + 1;
                        workSheet.Cells[i + 4, 2].Value = teachers[i].TeacherCode;
                        workSheet.Cells[i + 4, 3].Value = teachers[i].FullName;
                        if (!string.IsNullOrEmpty(teachers[i].PhoneNumber))
                        {
                            workSheet.Cells[i + 4, 4].Value = teachers[i].PhoneNumber;
                        }
                        else
                        {
                            workSheet.Cells[i + 4, 4].Value = "";
                        }
                        workSheet.Cells[i + 4, 5].Value = teachers[i].DepartmentName;
                        workSheet.Cells[i + 4, 6].Value = teachers[i].SubjectName;
                        workSheet.Cells[i + 4, 7].Value = teachers[i].RoomName;
                        if (teachers[i].IsTrained == 1)
                        {
                            workSheet.Cells[i + 4, 8].Value = "x";
                        }
                        else
                        {
                            workSheet.Cells[i + 4, 8].Value = "";
                        }
                        if (teachers[i].IsWorking == 1)
                        {
                            workSheet.Cells[i + 4, 9].Value = "x";
                        }
                        else
                        {
                            workSheet.Cells[i + 4, 9].Value = "";
                        }
                    }

                    // Tạo độ rộng hiển thị cho các cột của bảng
                    workSheet.Column(1).Width = 4;
                    workSheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    workSheet.Column(2).AutoFit();
                    workSheet.Column(3).AutoFit();
                    workSheet.Column(4).AutoFit();
                    workSheet.Column(5).AutoFit();
                    workSheet.Column(6).AutoFit();
                    workSheet.Column(7).AutoFit();
                    workSheet.Column(8).AutoFit();
                    workSheet.Column(8).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    workSheet.Column(9).AutoFit();
                    workSheet.Column(9).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // Tạo border cho các ô trong bảng
                    for (int i = 0; i < teachers.Count; i++)
                    {
                        for (int j = 1; j < 10; j++)
                        {
                            workSheet.Cells[i + 4, j].Style.Font.Size = 11;
                            workSheet.Cells[i + 4, j].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            workSheet.Cells[i + 4, j].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            workSheet.Cells[i + 4, j].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            workSheet.Cells[i + 4, j].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        }
                    }
                    package.Save();
                }
                // tạo link download file excel danh sách giáo viên
                stream.Position = 0;
                string excelName = "Danh_sach_can_bo_giao_vien.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
            
        }

        /// <summary>
        /// xử lý exception
        /// </summary>
        /// <returns>Mã lỗi, Thông tin lỗi</returns>
        /// Author: VQBao - 4/8/2022
        protected IActionResult HandleException(Exception e)
        {
            var message = new
            {
                userMsg = MISA.EMIS.Common.Resource.ResourceText.Error_UserMsg,
                errorMsg = e.Data["validateError"]
            };
            if (e is MISAException)
            {
                return BadRequest(message);
            }
            return StatusCode(500, message);
        }
        #endregion
    }
}
