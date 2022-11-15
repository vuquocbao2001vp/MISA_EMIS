using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.EMIS.DAL.Interface
{
    // interface chung
    public interface IBaseRepository<MISAInterface>
    {
        #region Methods
        // Phương thức lấy toàn bộ dữ liệu
        IEnumerable<MISAInterface> GetAll();
        // Phương thức lấy dữ liệu theo Id
        MISAInterface GetById(Guid id);
        // Phương thức thêm mới 1 bản ghi
        int Insert(MISAInterface misaInterface);
        // Phương thức xóa 1 bản ghi
        int Delete(Guid id);
        #endregion
    }
}
