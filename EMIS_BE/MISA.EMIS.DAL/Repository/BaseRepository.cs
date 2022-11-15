using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace MISA.EMIS.DAL.Repository
{
    // Repository dùng chung
    public class BaseRepository<MISAEntity>: IDisposable
    {
        #region Constructor
        protected readonly string connectionString = "";
        protected MySqlConnection connection;
        public BaseRepository()
        {
            // khởi tạo kết nối với dataabse
            connectionString =
                "Host=3.0.89.182; " +
                "Port=3306; " +
                "Database=MISA.WEB06.VQBAO; " +
                "User Id=dev; " +
                "Password=12345678;";
            connection = new MySqlConnection(connectionString);
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        /// <summary>
        /// Đóng kết nối với database
        /// </summary>
        /// Author: VQBao - 8/8/2022
        public void Dispose()
        {
            connection.Dispose();
            connection.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Thực hiện lấy toàn bộ dữ liệu từ bảng database
        /// </summary>
        /// <returns>Dữ liệu của bảng</returns>
        /// Author: VQBao - 8/8/2022
        public IEnumerable<MISAEntity> GetAll()
        {
            var className = typeof(MISAEntity).Name;
            // Thực thi lấy dữ liệu
            var sqlCommand = $"SELECT * FROM {className}";

            // Trả về dữ liệu cho client
            var data = connection.Query<MISAEntity>(sqlCommand);
            return data;
        }

        /// <summary>
        /// Thực hiện lấy dữ liệu theo khóa chính
        /// </summary>
        /// <param name="id">Khóa chính của dữ liệu muốn trả về</param>
        /// <returns>Dữ liệu của bản ghi thỏa mãn</returns>
        /// Author: VQBao - 8/8/2022
        public MISAEntity GetById(Guid id)
        {
            var className = typeof(MISAEntity).Name;
            // Thực thi lấy dữ liệu
            var sqlCommand = $"SELECT * FROM {className} WHERE {className}Id = @id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            // Trả về dữ liệu cho client
            var data = connection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: parameters);
            return data;
        }
    
        /// <summary>
        /// Thêm mới 1 bản ghi vào database
        /// </summary>
        /// <param name="entity">Object cần thêm mới</param>
        /// <returns>1 - Thêm mới thành công, nếu không thì báo lỗi</returns>
        /// Author: VQBao - 8/8/2022
        public int Insert(MISAEntity entity)
        {
            var className = typeof (MISAEntity).Name;
            // khởi tạo transaction
            using (var transaction = connection.BeginTransaction())
            {
                var storeProc = $"Proc_Insert{className}";
                var rowEffect = connection.Execute(storeProc, param: entity, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                transaction.Commit();
                return rowEffect;
            }
        }

        /// <summary>
        /// Thực hiện xóa 1 bản ghi trong database
        /// </summary>
        /// <param name="id">Khóa chính của object cần xóa</param>
        /// <returns>1 - Xóa thành công, không thì báo lỗi</returns>
        /// Author: VQBao - 8/8/2022
        public int Delete(Guid id)
        {
            var className = typeof(MISAEntity).Name;
            var sqlCommand = $"DELETE FROM {className} WHERE {className}Id = @id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var respond = connection.Execute(sqlCommand, param: parameters);
            return respond;
        }
        #endregion
    }
}
