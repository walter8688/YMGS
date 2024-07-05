using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Presentation;
using YMGS.DataAccess.SystemSetting;
using YMGS.Framework;

namespace YMGS.Business.SystemSetting
{
    public class VCardManager
    {
        private static char[] constant = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static char[] constantNum = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        /// <summary>
        /// 生成V网卡
        /// </summary>
        /// <param name="VCardNo"></param>
        /// <param name="VCardActivateNo"></param>
        /// <param name="VCardFaceValue"></param>
        /// <param name="createUserId"></param>
        /// <returns></returns>
        public static int GenerateVCards(int VCardFaceValue, int createUserId, int vcardNums)
        {
            int successGenerateNum = 0;
            var VCardNo = "V-";
            var VCardActivateNo = string.Empty;
            Random rd = new Random();
            for (var i = 0; i < vcardNums; i++)
            {
                for (var j = 0; j < 12; j++)
                {
                    VCardNo = VCardNo + constantNum[rd.Next(constantNum.Length)]; //随机生成V网卡卡号
                    VCardActivateNo = VCardActivateNo + constant[rd.Next(constant.Length)];//随机生成V网卡激活码
                }
                VCardNo = EncryptManager.DESEnCrypt(VCardNo);
                VCardActivateNo = EncryptManager.DESEnCrypt(VCardActivateNo);

                if (VCardDA.GenerateVCard(VCardNo, VCardActivateNo, VCardFaceValue, createUserId) != -1)
                {
                    successGenerateNum++;
                }
                VCardNo = "V-";
                VCardActivateNo = string.Empty;
            }
            return successGenerateNum;
        }

        /// <summary>
        /// 获取V网卡信息
        /// </summary>
        /// <param name="VCardFaceValue"></param>
        /// <param name="VCardStatus"></param>
        /// <returns></returns>
        public static DSVCardDetail QueryAllVCardInfo(int VCardFaceValue, int VCardStatus, DateTime startDate, DateTime endDate)
        {
            return VCardDA.QueryAllVCardInfo(VCardFaceValue, VCardStatus, startDate, endDate);
        }

        /// <summary>
        /// 激活V网卡
        /// </summary>
        /// <param name="VCardNo"></param>
        /// <param name="VCardActivateNo"></param>
        /// <param name="activateUserId"></param>
        public static int ActivatedVCard(string VCardNo, string VCardActivateNo, int activateUserId)
        {
            return VCardDA.ActivatedVCard(VCardNo, VCardActivateNo, activateUserId);
        }

        /// <summary>
        /// 获取V网卡ID
        /// </summary>
        /// <param name="VCardNo"></param>
        /// <param name="VCardActivateNo"></param>
        /// <returns></returns>
        public static int QueryVCardID(string VCardNo, string VCardActivateNo)
        {
            var VCardDS = VCardDA.QueryVCardDetail(VCardNo, VCardActivateNo);
            if (VCardDS == null)
                return -1;
            if (VCardDS.Tables[0].Rows.Count == 0 || string.IsNullOrEmpty(VCardDS.Tables[0].Rows[0][0].ToString()))
                return -1;
            return Convert.ToInt32(VCardDS.TB_VCARD_DETAIL[0].VCARD_ID);
        }

        /// <summary>
        /// 生成单个V网卡
        /// </summary>
        /// <param name="VCardFaceValue"></param>
        /// <param name="createUserId"></param>
        /// <returns></returns>
        public static int GenerateVCard(int VCardFaceValue, int createUserId)
        {
            var VCardNo = "V-";
            var VCardActivateNo = string.Empty;
            Random rd = new Random();

            for (var j = 0; j < 12; j++)
            {
                VCardNo = VCardNo + constantNum[rd.Next(constantNum.Length)]; //随机生成V网卡卡号
                VCardActivateNo = VCardActivateNo + constant[rd.Next(constant.Length)];//随机生成V网卡激活码
            }
            VCardNo = EncryptManager.DESEnCrypt(VCardNo);
            VCardActivateNo = EncryptManager.DESEnCrypt(VCardActivateNo);

            var VardID = Convert.ToInt32(VCardDA.GenerateVCard(VCardNo, VCardActivateNo, VCardFaceValue, createUserId));
            return VardID;
        }
    }
}
