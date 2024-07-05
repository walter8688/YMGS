using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.Presentation;

namespace YMGS.DataAccess.SystemSetting
{
    public class VCardDA
    {
        public static int GenerateVCard(string VCardNo,string VCardActivateNo,int VCardFaceValue,int createUserId)
        {
            var parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@VCard_No", ParameterType = DbType.String, ParameterValue = VCardNo},
                new ParameterData(){ ParameterName = "@VCard_Activate_No", ParameterType = DbType.String, ParameterValue = VCardActivateNo},
                new ParameterData(){ ParameterName = "@VCard_Face_Value", ParameterType = DbType.Int32, ParameterValue = VCardFaceValue},
                new ParameterData(){ ParameterName = "@Create_User_Id", ParameterType = DbType.Int32, ParameterValue = createUserId}
            };
            var returnObject = SQLHelper.ExecuteStoredProcForScalar("pr_add_vcard", parameters);
            return Convert.ToInt32(returnObject);
        }

        public static DSVCardDetail QueryAllVCardInfo(int VCardFaceValue, int VCardStatus, DateTime startDate, DateTime endDate)
        {
            var parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@VCard_Face_Value", ParameterType = DbType.Int32, ParameterValue = VCardFaceValue},
                new ParameterData(){ ParameterName = "@VCard_Status", ParameterType = DbType.Int32, ParameterValue = VCardStatus}
            };
            if (startDate != DateTime.MinValue)
                parameters.Add(new ParameterData() { ParameterName = "@Start_Date", ParameterType = DbType.DateTime, ParameterValue = startDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Start_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            if (endDate != DateTime.MaxValue)
                parameters.Add(new ParameterData() { ParameterName = "@End_Date", ParameterType = DbType.DateTime, ParameterValue = endDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@End_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            return SQLHelper.ExecuteStoredProcForDataSet<DSVCardDetail>("pr_query_vcard", parameters);
        }

        public static int ActivatedVCard(string VCardNo, string VCardActivateNo, int activateUserId)
        {
            var parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@VCard_No", ParameterType = DbType.String, ParameterValue = VCardNo},
                new ParameterData(){ ParameterName = "@VCard_Activate_No", ParameterType = DbType.String, ParameterValue = VCardActivateNo},
                new ParameterData(){ ParameterName = "@Activate_User_Id", ParameterType = DbType.Int32, ParameterValue = activateUserId}
            };
            var returnObj = SQLHelper.ExecuteStoredProcForScalar("pr_activate_vcard", parameters);
            return Convert.ToInt32(returnObj);
        }

        public static DSVCardDetail QueryVCardDetail(string VCardNo, string VCardActivateNo)
        {
            var parameters = new List<ParameterData>()
            {
                new ParameterData(){ ParameterName = "@vcard_no", ParameterType = DbType.String, ParameterValue = VCardNo},
                new ParameterData(){ ParameterName = "@vcard_activate_no", ParameterType = DbType.String, ParameterValue = VCardActivateNo}
            };
            return SQLHelper.ExecuteStoredProcForDataSet<DSVCardDetail>("query_vcard_detail", parameters);
        }
    }
}
