using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.EMIS.Common.Entities
{
    /// <summary>
    /// Lớp kế thừa
    /// </summary>
    public class BaseEntity
    {
        #region Constructor
        // Ngày tạo
        public DateTime? CreatedDate { get; set; }
        // Người tạo
        public string? CreatedBy { get; set; }
        // Ngày sửa
        public DateTime? ModifiedDate { get; set; }
        // Người sửa
        public string? ModifiedBy { get; set; }
#endregion
    }
}
