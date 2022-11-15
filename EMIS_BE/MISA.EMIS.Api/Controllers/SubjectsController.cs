using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.EMIS.Common.Entities;
using MISA.EMIS.Common;
using MISA.EMIS.DAL.Repository;
using MISA.EMIS.BLL.Services;
using MISA.EMIS.DAL.Interface;

namespace MISA.EMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubjectsController : MISABaseController<Subject>
    {
        ISubjectRepository repository;
        public SubjectsController(ISubjectRepository _repository):base(_repository)
        {
            repository = _repository;
        }
    }
}
