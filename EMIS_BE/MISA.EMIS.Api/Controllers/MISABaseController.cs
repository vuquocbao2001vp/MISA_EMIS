using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.EMIS.Common;
using MISA.EMIS.DAL.Interface;

namespace MISA.EMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISABaseController<MISAEntity> : ControllerBase
    {

        #region Constructor
        IBaseRepository<MISAEntity> baseRepository;
        public MISABaseController(IBaseRepository<MISAEntity> _baseRepository)
        {
            baseRepository = _baseRepository;
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
                var data = baseRepository.GetAll();
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
                var data = baseRepository.GetById(id);
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
        public IActionResult Post(MISAEntity entity)
        {
            try
            {
                var data = baseRepository.Insert(entity);
                return StatusCode(201, data);
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
                var data = baseRepository.Delete(id);
                return StatusCode(201, data);
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
