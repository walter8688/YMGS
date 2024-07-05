using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Entity
{
    public class FootBallMatchScore
    {
        /// <summary>
        /// 主队上半场得分
        /// </summary>
        public int? HomeFirHalfScore
        {
            get;
            set;
        }

        /// <summary>
        /// 客队上半场得分
        /// </summary>
        public int? GuestFirHalfScore
        {
            get;
            set;
        }

        /// <summary>
        /// 主队下半场得分
        /// </summary>
        public int? HomeSecHalfScore
        {
            get;
            set;
        }

        /// <summary>
        /// 客队下半场得分
        /// </summary>
        public int? GuestSecHalfScore
        {
            get;
            set;
        }

        /// <summary>
        /// 主队加时得分
        /// </summary>
        public int? HomeOverTimeScore
        {
            get;
            set;
        }

        /// <summary>
        /// 客队加时得分
        /// </summary>
        public int? GuestOverTimeScore
        {
            get;
            set;
        }

        /// <summary>
        /// 主队点球得分
        /// </summary>
        public int? HomePointScore
        {
            get;
            set;
        }

        /// <summary>
        /// 客队点球得分
        /// </summary>
        public int? GuestPointScore
        {
            get;
            set;
        }

        /// <summary>
        /// 主队全场得分
        /// </summary>
        public int? HomeFullScore
        {
            get;
            set;
        }

        /// <summary>
        /// 客队全场得分
        /// </summary>
        public int? GuestFullScore
        {
            get;
            set;
        }
    }
}
