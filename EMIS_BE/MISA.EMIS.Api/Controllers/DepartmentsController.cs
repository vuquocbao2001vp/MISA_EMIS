using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.EMIS.Common.Entities;
using MISA.EMIS.Common;
using MISA.EMIS.DAL.Repository;
using MISA.EMIS.DAL.Interface;
using MISA.EMIS.BLL.Services;

namespace MISA.EMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : MISABaseController<Department>
    {
        IDepartmentRepository repository;
        public DepartmentsController(IDepartmentRepository _repository):base(_repository)
        {
            repository = _repository;
        }
        
    }
}
