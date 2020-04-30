using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winner.OAuth.DataAccess.Oracle
{
    public partial class Tnet_User
    {
        public int GetNewUserId()
        {
            string sql = "SELECT SEQ_TNET_USER.NEXTVAL FROM DUAL";
            return GetSequence(sql);
        }

        public string GenerateCustomCode()
        {
            string sql = "SELECT SEQ_TNET_USER_CODE.NEXTVAL FROM DUAL";
            int seq = GetSequence(sql);
            return seq.ToString();
        }
    }
}
