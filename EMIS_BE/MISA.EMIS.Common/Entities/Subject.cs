using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.EMIS.Common.Entities
{
    // Class môn học
    public class Subject: BaseEntity
    {
        #region Constructor
        // Id môn học
        public Guid SubjectId { get; set; }
        // Tên môn học
        public string SubjectName { get; set; }
        #endregion
    }
}
