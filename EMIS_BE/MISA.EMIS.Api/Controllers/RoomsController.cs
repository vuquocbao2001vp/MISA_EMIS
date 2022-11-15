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
    public class RoomsController : MISABaseController<Room>
    {
        IRoomRepository repository;
        public RoomsController(IRoomRepository _repository):base(_repository)
        {
            repository = _repository;
        }
    }
}
