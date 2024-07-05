using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Entity
{
    public class HelperObject
    {
        /// <summary>
        /// ITEMID
        /// </summary>
        public int ITEMID { get; set; }
        /// <summary>
        /// PITEMID
        /// </summary>
        public int PITEMID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string LinkAddress { get; set; }
        /// <summary>
        /// 排列序号
        /// </summary>
        public string OrderNO { get; set; }
        /// <summary>
        /// 规则ID
        /// </summary>
        public string RulesID { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int LevelNO { get; set; }
        /// <summary>
        /// 是否有子项
        /// </summary>
        public bool IsHasChild { get; set; }
    }
}
