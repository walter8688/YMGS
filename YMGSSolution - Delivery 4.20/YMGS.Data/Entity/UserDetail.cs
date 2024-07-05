using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;

namespace YMGS.Data.Entity
{
    public class UserDetail 
    {
        public DSSystemAccount.TB_SYSTEM_ACCOUNTDataTable ACCOUNT { get; set; }
        public DSRoleFuncMap.TB_ROLE_FUNC_MAPDataTable ROLE_FUNC { get; set; }
    }
}
