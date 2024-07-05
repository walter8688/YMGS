using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Threading;

namespace YMGS.Manage.Web.Controls
{
    /// <summary>
    /// 进度条类
    /// </summary>
    public class ASPProgressBar
    {
        /// <summary>
        /// 是否已经init
        /// </summary>
        private bool hasInited = false;

        /// <summary>
        /// 页面对象
        /// </summary>
        System.Web.UI.Page Page = null;

        /// <summary>
        /// 进度条类构造方法
        /// </summary>
        /// <param name="response">当前页面</param>
        /// <param name="id">进度条客户端id</param>
        public ASPProgressBar(System.Web.UI.Page Page, string id)
        {
            this.Page = Page;
            this.id = id;
        }

        /// <summary>
        /// 进度条类构造方法
        /// </summary>
        /// <param name="response">当前页面</param>
        /// <param name="id">进度条客户端id</param>
        /// <param name="autoRunOver">是否自动运行到100%</param>
        /// <param name="timeOver">运行到100%,预设时间,单位为秒</param>
        public ASPProgressBar(System.Web.UI.Page Page, string id, bool autoRunOver, int timeOver)
        {
            this.Page = Page;
            this.id = id;
            this.autoRunOver = autoRunOver;
            this.timeOver = timeOver;
        }

        string id = "";
        /// <summary>
        /// 进度条客户端id
        /// </summary>
        public string ID
        {
            get
            {
                return this.id;
            }
        }

        string imageUrl = "";
        /// <summary>
        /// 展现图片url
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return this.imageUrl;
            }
            set
            {
                this.imageUrl = value;
                if (this.hasInited)
                {
                    HttpResponse response = this.Page.Response;
                    response.Write("<script>");
                    response.Write(this.ID);
                    response.Write("_var.setImageUrl('" + value + "');");
                    response.Write("</script>\r\n");
                    response.Flush();
                }
            }
        }

        string showText = "";
        /// <summary>
        /// 展现图片url
        /// </summary>
        public string ShowText
        {
            get
            {
                return this.showText;
            }
            set
            {
                this.showText = value;
                if (this.hasInited)
                {
                    HttpResponse response = this.Page.Response;
                    response.Write("<script>");
                    response.Write(this.ID);
                    response.Write("_var.setShowText('" + value.Replace("\'", "\\\'") + "');");
                    response.Write("</script>\r\n");
                    response.Flush();
                }
            }
        }

        int timeOver = 30;
        /// <summary>
        /// 预计结束时间
        /// </summary>
        public int TimeOver
        {
            get
            {
                return this.timeOver;
            }
        }

        bool autoRunOver = false;
        /// <summary>
        /// 是否自动运行结束
        /// </summary>
        public bool AutoRunOver
        {
            get
            {
                return this.autoRunOver;
            }
        }

        /// <summary>
        /// 初始化进度条
        /// </summary>
        ///<param name="scriptPath">脚本相对路径默认为../script</param>
        public void Init(string scriptPath)
        {
            string strp = "../Images/pba.jpg";
            if (this.ImageUrl.Length <= 0)
            {
                this.ImageUrl = strp;
            }

            HttpResponse response = this.Page.Response;
            response.Write("<script  language =\"javascript\" type=\"text/javascript\" src=\"" + scriptPath + "/jquery-1.8.3.min.js\"></script>\r\n");
            response.Write("<script  language =\"javascript\" type=\"text/javascript\" src=\"" + scriptPath + "/ASPProgressBar.js\"></script>\r\n");
            response.Write("<script>\r\n");
            response.Write("var ");
            response.Write(this.ID);
            response.Write("_var= new ASPProgressBar(\"");
            response.Write(this.ID);
            response.Write("\",\"");
            response.Write(this.ImageUrl);
            response.Write("\",");
            response.Write(this.AutoRunOver.ToString().ToLower());
            response.Write(",");
            response.Write(this.TimeOver);
            if (this.ShowText.Length > 0)
            {
                response.Write(",'");
                response.Write(this.ShowText.Replace("\'", "\\\'"));
                response.Write("'");
            }
            response.Write(");\r\n");
            response.Write(this.ID);
            response.Write("_var.write();");
            response.Write("</script>\r\n");
            response.Flush();
            this.hasInited = true;
        }

        /// <summary>
        /// 初始化进度条
        /// </summary>
        public void Init()
        {
            string strp = "../Scripts";
            this.Init(strp);
        }

        /// <summary>
        /// 更新进度
        /// </summary>
        /// <param name="value">进度值</param>
        /// <param name="Text">进度描述</param>
        public void UpdateProgress(int value, string Text)
        {
            UpdateProgress(value, Text, 10, "");
        }

        /// <summary>
        /// 更新进度
        /// </summary>
        /// <param name="value">进度值</param>
        /// <param name="Text">进度描述</param>
        /// <param name="timeOver">到达值预计时间单位秒</param>
        public void UpdateProgress(int value, string Text, int timeOver)
        {
            UpdateProgress(value, Text, timeOver, "");
        }

        /// <summary>
        /// 更新进度
        /// </summary>
        /// <param name="value">进度值</param>
        /// <param name="Text">进度描述</param>
        /// <param name="timeOver">到达值预计时间单位秒</param>
        /// <param name="imgUrl">进度更新图片</param>
        public void UpdateProgress(int value, string Text, int timeOver, string imgUrl)
        {
            HttpResponse response = this.Page.Response;
            response.Write("<script>");
            response.Write(this.ID);
            response.Write("_var.setProgress(");
            response.Write(value);
            response.Write(",'");
            response.Write(Text.Replace("'", "\""));
            response.Write("',");
            response.Write(timeOver);
            response.Write(",'");
            response.Write(imgUrl);
            response.Write("'");
            response.Write(");");
            response.Write("</script>\r\n");
            response.Flush();
        }

        /// <summary>
        /// 隐藏进度条
        /// </summary>
        public void Hide()
        {
            HttpResponse response = this.Page.Response;
            response.Write("<script>");
            response.Write(this.ID);
            response.Write("_var.hide();");
            response.Write("</script>\r\n");
            response.Flush();
        }

        /// <summary>
        /// 隐藏进度条
        /// </summary>
        public void Hide(string url)
        {
            HttpResponse response = this.Page.Response;
            response.Write("<script>");
            response.Write(this.ID);
            response.Write("_var.hide(\"" + url + "\");");
            response.Write("</script>\r\n");
            response.Flush();
        }

        /// <summary>
        /// 重新显示进度条,并重置为0
        /// </summary>
        public void Show()
        {
            HttpResponse response = this.Page.Response;
            response.Write("<script>");
            response.Write(this.ID);
            response.Write("_var.show();");
            response.Write("</script>\r\n");
            response.Flush();
        }
    }
}
