using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace YMGS.Framework
{
    [SerializableAttribute]
    public class ParameterData
    {
        private string strParamName;
        private object objParamValue;
        private System.Data.DbType objParamType;

        public ParameterData()
        {
        }

        public ParameterData(string paramName,object paramValue,DbType paramType)
        {
            strParamName = paramName;
            objParamValue = paramValue;
            objParamType = paramType;
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName
        {
            get
            {
                return strParamName;
            }
            set
            {
                strParamName = value;
            }
        }

        /// <summary>
        /// 参数值
        /// </summary>
        public object ParameterValue
        {
            get
            {
                return objParamValue;
            }
            set
            {
                objParamValue = value;
            }
        }

        /// <summary>
        /// 参数类型
        /// </summary>
        public System.Data.DbType ParameterType
        {
            get
            {
                return objParamType;
            }
            set
            {
                objParamType = value;
            }
        }
    }
}
