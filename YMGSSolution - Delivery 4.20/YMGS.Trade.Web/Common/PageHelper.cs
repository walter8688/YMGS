using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using AjaxControlToolkit;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Data;
using System.Web.UI.WebControls;
using YMGS.Data.Entity;
using YMGS.Trade.Web.MasterPage;

namespace YMGS.Trade.Web.Common
{
    public class PageHelper
    {
        /// <summary>
        /// 显示提醒信息
        /// </summary>
        /// <param name="strMessage"></param>
        public static void ShowMessage(Page page, string strMessage)
        {
            ToolkitScriptManager toolkitScriptManager = null;
            if (page.Master is HomeMaster)
                toolkitScriptManager = (page.Master as HomeMaster).ScriptManager;

            string strScript = @"<script>alert('" + strMessage.Replace("\r", @"\r").Replace("\n", @"\n").Replace("'", "\"") + "');</script>";

            if (toolkitScriptManager != null)
            {
                ToolkitScriptManager.RegisterStartupScript(page, page.GetType(), "showmessage", strScript, false);
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "info", strScript);
            }
        }

        public static void ShowMessageByScriptManager(Page page, string strMessage)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), "alert('" + strMessage + "')", true);
        }

        /// <summary>
        /// 获得当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public static DetailUserInfo GetCurrentUser()
        {
            var session = HttpContext.Current.Session;

            if (session[CommonConstant.CurrentLoginUserSessionKey] != null)
                return (DetailUserInfo)session[CommonConstant.CurrentLoginUserSessionKey];
            else
                return null;
        }
        /// <summary>
        /// 给页面控件赋值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="obj"></param>
        public static void GetValue(DataTable dt, Control obj)
        {
            if (dt.Rows.Count != 1)
                return;
            DataRow dr = null;
            if (dt.Rows.Count == 1)
                dr = dt.Rows[0];
            else
                dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                string cn = dc.ColumnName;
                Control ctl = obj.FindControl(cn);
                if (ctl == null)
                    continue;
                if (ctl is DropDownList)
                {
                    ((DropDownList)ctl).SelectedValue = dr[cn].ToString();
                    continue;
                }
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = dr[cn].ToString();
                    continue;
                }
                if (ctl is HiddenField)
                {
                    ((HiddenField)ctl).Value = dr[cn].ToString();
                    continue;
                }
                if (ctl is CheckBox)
                {
                    ((CheckBox)ctl).Checked = dr[cn].ToString() == "1" ? true : false;
                }

                if (ctl is CheckBoxList)
                {
                    CheckBoxList cblist = ((CheckBoxList)ctl);
                    foreach (string item in dr[cn].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        foreach (ListItem li in cblist.Items)
                        {
                            if (item == li.Value)
                                li.Selected = true;
                        }
                    }
                    continue;
                }
                if (ctl is RadioButtonList)
                {
                    ((RadioButtonList)ctl).SelectedValue = dr[cn].ToString();
                    continue;
                }

            }
        }
        /// <summary>
        /// 从页面控件取值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="obj"></param>
        public static void SetValue(DataTable dt, Control obj)
        {

            DataRow dr = null;
            if (dt.Rows.Count == 1)
                dr = dt.Rows[0];
            else
                dr = dt.NewRow();
            
            foreach (DataColumn dc in dt.Columns)
            {
               
                string cn = dc.ColumnName; Control ctl = obj.FindControl(cn);
                string typename = dc.DataType.ToString();
                if (ctl == null)
                {
                    if (dc.AllowDBNull)
                    {
                        if(dr[cn]==null)
                        dr[cn] = DBNull.Value;
                    }
                    continue;
                }
                string v = null;
                if (ctl is DropDownList)
                {
                    v = ((DropDownList)ctl).SelectedValue;

                    goto SetValueMark;
                }

                if (ctl is TextBox)
                {
                    v = ((TextBox)ctl).Text;
                    goto SetValueMark;
                }
                if (ctl is HiddenField)
                {
                    v = ((HiddenField)ctl).Value;
                    goto SetValueMark;
                }
                if (ctl is CheckBox)
                {
                    v = ((CheckBox)ctl).Checked ? "1" : "0";
                    goto SetValueMark;
                }

                if (ctl is CheckBoxList)
                {
                    CheckBoxList cblist = ((CheckBoxList)ctl);
                    string values = "";
                    foreach (ListItem li in cblist.Items)
                    {
                        if (li.Selected)
                            values = li.Value + ",";
                    }
                    values = values.Substring(0, values.Length - 1);
                    v = values;
                    goto SetValueMark;
                }
                if (ctl is RadioButtonList)
                {
                    v = ((RadioButtonList)ctl).SelectedValue;
                    goto SetValueMark;
                }

            SetValueMark:
                {
                    if (typename.ToLower() == "system.int32")
                    {
                        int result = 0;
                        try
                        {
                            result = int.Parse(v);
                        }
                        catch
                        {
                        }
                        dr[cn] = result;
                    }
                    else
                        dr[cn] = v;
                }
            }
            if (dt.Rows.Count != 1)
            dt.Rows.Add(dr);

            dt.AcceptChanges();
        }

        /// <summary>
        /// 获得当前用户的权限访问对象
        /// </summary>
        /// <returns></returns>
        public static UserAccess GetCurrentUserAccess()
        {
            return new UserAccess(GetCurrentUser());
        }

        public static string MakeClientUrl(string requestAppPath, string url, NameValueCollection queryParams)
        {
            var unmappedUrl = Regex.Replace(url,
                "^~/", x => requestAppPath.TrimEnd('/', '\\') + "/");
            return unmappedUrl;
        }


        /// <summary>
        /// 绑定List类型控件数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="datasource"></param>
        /// <param name="dataTextField"></param>
        /// <param name="dataValueField"></param>
        /// <param name="needNullItem"></param>
        public static void BindListControlData(Control obj, object datasource, string dataTextField, string dataValueField, bool needNullItem)
        {
            ListItem item = new ListItem(CommonConstant.DropDownListNullKey, CommonConstant.DropDownListNullValue);
            if (obj is DropDownList)
            {
                DropDownList drp = obj as DropDownList;
                drp.Items.Clear();
                drp.DataSource = datasource;
                drp.DataTextField = dataTextField;
                drp.DataValueField = dataValueField;
                drp.DataBind();
                if (needNullItem)
                    drp.Items.Insert(0, item);
            }
            if (obj is ListBox)
            {
                ListBox ckcList = obj as ListBox;
                ckcList.Items.Clear();
                ckcList.DataSource = datasource;
                ckcList.DataTextField = dataTextField;
                ckcList.DataValueField = dataValueField;
                ckcList.DataBind();
                if (needNullItem)
                    ckcList.Items.Insert(0, item);
            }
        }

        public static void SetUserInfoToSession(DetailUserInfo userInfo)
        {
            var session = HttpContext.Current.Session;
            session[CommonConstant.CurrentLoginUserSessionKey] = userInfo;
        }
    }
}