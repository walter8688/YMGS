using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Data.DataBase;
using System.Data;

namespace YMGS.DataAccess.AssistManage
{
    public class HelperDA
    {
        public static DSHelper QueryHelper()
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@ITEMID",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@PITEMID",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@CNITEMNAME",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@ENITEMNAME",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@WEBLINK",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@ENWEBLINK",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@OrderNO",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@RulesID",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@LevelNO",ParameterType=DbType.Int32,ParameterValue=0}
            };
            var result = SQLHelper.ExecuteStoredProcForDataSet<DSHelper>("pr_edit_helper", parameters);
            return result;
        }

        public static void EditHelper(DSHelper.TB_HELPERRow Row, int flag)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=flag},
                new ParameterData(){ParameterName="@ITEMID",ParameterType=DbType.Int32,ParameterValue=Row.ITEMID},
                new ParameterData(){ParameterName="@PITEMID",ParameterType=DbType.Int32,ParameterValue=Row.PITEMID},
                new ParameterData(){ParameterName="@CNITEMNAME",ParameterType=DbType.String,ParameterValue=Row.CNITEMNAME},
                new ParameterData(){ParameterName="@ENITEMNAME",ParameterType=DbType.String,ParameterValue=Row.ENITEMNAME},
                new ParameterData(){ParameterName="@WEBLINK",ParameterType=DbType.String,ParameterValue=Row.IsWEBLINKNull()?"":Row.WEBLINK},
                new ParameterData(){ParameterName="@ENWEBLINK",ParameterType=DbType.String,ParameterValue=Row.IsENITEMNAMENull()?"": Row.ENWEBLINK},
                new ParameterData(){ParameterName="@OrderNO",ParameterType=DbType.Int32,ParameterValue=string.IsNullOrEmpty(Row.OrderNO)?null:Row.OrderNO},
                new ParameterData(){ParameterName="@RulesID",ParameterType=DbType.String,ParameterValue=Row.RulesID},
                new ParameterData(){ParameterName="@LevelNO",ParameterType=DbType.Int32,ParameterValue=Row.LevelNO}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_edit_helper", parameters);
        }
    }
}
