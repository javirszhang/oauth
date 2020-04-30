using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winner.OAuth.DataAccess.MySQL
{
    public partial class Tnet_User
    {
        public string GenerateCustomCode()
        {
            string sql = "insert into tusr_code values(0)";
            int id;
            if (!InsertBySql(sql, out id))
            {
                return null;
            }
            return id.ToString();
        }
    }
}
