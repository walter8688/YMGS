using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Presentation;
using YMGS.DataAccess.SystemSetting;

namespace YMGS.Business.SystemSetting
{
    public class ADManager
    {
        public static DSADWords QueryDSADWords()
        {
            return ADDA.QueryDSADWords();
        }
        public static DSADPic QueryDSADPic()
        {
            return ADDA.QueryADPic();
        }

        public static void EditADPic(DSADPic.TB_AD_PICRow betRow, int flag)
        {
             ADDA.EditADPic(betRow, flag);
        }

        public static void EditDSADWords(DSADWords.TB_AD_WORDSRow betRow, int flag)
        {
             ADDA.EditDSADWords(betRow, flag);
        }
    }
}
