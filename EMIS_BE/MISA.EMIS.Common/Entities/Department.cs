using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.EMIS.Common.Entities
{
    // Class Tổ bộ môn
    public class Department: BaseEntity
    {
        #region Constructor
        // Id tổ bộ môn
        public Guid DepartmentId { get; set; }
        // Tên tổ bộ môn
        public string DepartmentName { get; set; }
        #endregion
    }
}
