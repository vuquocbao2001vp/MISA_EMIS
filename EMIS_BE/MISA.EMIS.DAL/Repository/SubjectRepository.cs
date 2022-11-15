using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA.EMIS.Common.Entities;
using MISA.EMIS.DAL.Interface;

namespace MISA.EMIS.DAL.Repository
{
    // Repository của môn học
    public class SubjectRepository: BaseRepository<Subject>, ISubjectRepository
    {
    }
}
