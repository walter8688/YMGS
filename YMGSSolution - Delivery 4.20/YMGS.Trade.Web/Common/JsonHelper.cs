using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace YMGS.Trade.Web.Common
{
    public class JsonHelper
    {
        /// <summary>
        /// 将List转换为Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="IL"></param>
        /// <returns></returns>
        public static string ListToJson<T>(IList<T> IL)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var jsonObj = serializer.Serialize(IL);
            return jsonObj;
        }
    }
}