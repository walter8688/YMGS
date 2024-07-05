using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Presentation;
using YMGS.Framework;
using System.Data;

namespace YMGS.DataAccess.SystemSetting
{
   public class ADDA
    {

       public static DSADPic QueryADPic()
       {
           IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@id",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@address",ParameterType=DbType.Binary,ParameterValue=new byte[]{}},
                new ParameterData(){ParameterName="@address_en",ParameterType=DbType.Binary,ParameterValue=new byte[]{}}
            };
           var result = SQLHelper.ExecuteStoredProcForDataSet<DSADPic>("pr_get_ADDAPic", parameters);
           return result;
       }
       public static void EditADPic(DSADPic.TB_AD_PICRow betRow, int flag)
       {
           IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=flag},
                new ParameterData(){ParameterName="@id",ParameterType=DbType.Int32,ParameterValue=betRow.AD_PIC_ID},
                new ParameterData(){ParameterName="@address",ParameterType=DbType.Binary,ParameterValue=betRow.PIC_ADDRESS},
                new ParameterData(){ParameterName="@address_en",ParameterType=DbType.Binary,ParameterValue=betRow.PIC_ADDRESS_EN}
            };
           SQLHelper.ExecuteStoredProcForScalar("pr_get_ADDAPic", parameters);
       }
       public static DSADWords QueryDSADWords()
       {
           IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@id",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@title",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@title_en",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@DESC",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@DESC_en",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@WEBLINK",ParameterType=DbType.String,ParameterValue=""}
            };
           var result = SQLHelper.ExecuteStoredProcForDataSet<DSADWords>("pr_get_DSADWords", parameters);
           return result;
       }

       public static void EditDSADWords(DSADWords.TB_AD_WORDSRow betRow,int flag)
       {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=flag},
                new ParameterData(){ParameterName="@id",ParameterType=DbType.Int32,ParameterValue=betRow.AD_WORDS_ID},
                new ParameterData(){ParameterName="@title",ParameterType=DbType.String,ParameterValue=betRow.TITLE},
                new ParameterData(){ParameterName="@title_en",ParameterType=DbType.String,ParameterValue=betRow.TITLE_EN},
                new ParameterData(){ParameterName="@DESC",ParameterType=DbType.String,ParameterValue=betRow.DESC},
                new ParameterData(){ParameterName="@DESC_en",ParameterType=DbType.String,ParameterValue=betRow.DESC_EN},
                new ParameterData(){ParameterName="@WEBLINK",ParameterType=DbType.String,ParameterValue=betRow.WEBLINK}
            };
             SQLHelper.ExecuteStoredProcForScalar("pr_get_DSADWords", parameters);
           
       }
    }
}
