using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.EMIS.Common.Entities
{
    // Class kho phòng
    public class Room: BaseEntity
    {
        #region Constructor
        // Id kho phòng
        public Guid RoomId { get; set; }
        // Tên kho phòng 
        public string RoomName { get; set; }
        #endregion
    }
}
