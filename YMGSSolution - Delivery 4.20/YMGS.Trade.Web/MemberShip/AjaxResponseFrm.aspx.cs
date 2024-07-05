using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using YMGS.Business.MemberShip;

namespace YMGS.Trade.Web.MemberShip
{
    public partial class AjaxResponseFrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                object result = null;
                var executeMethod = GetRequsetKey();
                Type t = typeof(AjaxExecuter);
                object ob = System.Activator.CreateInstance(t);
                foreach (MethodInfo method in t.GetMethods())
                {
                    ParameterInfo[] parameters = method.GetParameters();
                    object[] param = new object[parameters.Length];
                    if (method.Name == executeMethod)
                    {
                        var paramList = this.GetRequestValues();
                        if (paramList.Count > 0)
                        {
                            for (int i = 0; i < paramList.Count; i++)
                            {
                                param[i] = paramList[i];
                            }
                        }
                        result = method.Invoke(ob, param);
                        break;
                    }
                }
                if (result != null)
                    ResponseResult(result.ToString());
            }
        }

        private string GetRequsetKey()
        {
            var Key = string.Empty;
            if (this.Request.QueryString["Key"] != null && this.Request.QueryString["Key"].ToString() != "")
            {
                Key = this.Request.QueryString["Key"].ToString();
            }
            return Key;
        }

        private IList<string> GetRequestValues()
        {
            var reqList = new List<string>();
            for (int i = 1; i < this.Request.QueryString.Count; i++)
            {
                reqList.Add(this.Request.QueryString[i].ToString());
            }
            return reqList;
        }

        private void ResponseResult(string result)
        {
            Response.Write(result);
            Response.End();
        }
    }

    public class AjaxExecuter
    {
        public string CheckAccountName(string accoutName)
        {
            return AgentAccountManager.CheckAccountNameExists(accoutName).ToString();
        }

        public string CheckEmail(string email)
        {
            return AgentAccountManager.CheckEmailExists(email).ToString();
        }
    }
}