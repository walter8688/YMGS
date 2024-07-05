using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using YMGS.Trade.Web.Common;
using YMGS.Data.DataBase;
using YMGS.Business.SystemSetting;

namespace YMGS.Trade.Web.Public
{
    public partial class ValidateCode : SimplePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // CreateImage(CreateRandomCode(4));
                string vcode = CreateRandomCode(4);
                Session["vcode"] = vcode;
                CreateCheckImage(vcode);
            }
        }
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(ValidateCode), "Public").AbsoluteUri;
        }
        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return true;
        }

        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <param name="codeCount"></param>
        /// <returns></returns>
        private string CreateRandomCode(int codeCount)
        {
            string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,Q,R,S,T,U,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(33);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        /// <summary>
        /// 生成图片1
        /// </summary>
        /// <param name="checkCode"></param>
        private void CreateImage(string checkCode)//参数checkCode是要绘制的验证码
        {
            //设置图片宽度，与字体大小有关
            int _width = (int)(checkCode.Length * 13);
            System.Drawing.Bitmap _image = new System.Drawing.Bitmap(_width, 20);
            Graphics _graphics = Graphics.FromImage(_image);
            //设置字体
            Font _font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            //设置画笔
            Brush _brush = new System.Drawing.SolidBrush(Color.Red);
            //填充底色
            _graphics.FillRectangle(new System.Drawing.SolidBrush(Color.AliceBlue), 0, 0, _image.Width, _image.Height);
            //绘制文字  
            _graphics.DrawString(checkCode, _font, _brush, 3, 3);
            //输出图片 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            _image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(ms.ToArray());
            _graphics.Dispose();
            _image.Dispose();

        }
        /// <summary>
        /// 生成图片2
        /// </summary>
        /// <param name="checkCode"></param>
        private void CreateCheckImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == string.Empty)
            {
                return;
            }

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random radom = new Random();
                g.Clear(Color.White);
                for (int i = 0; i < 2; i++)
                {
                    int x1 = radom.Next(image.Width);
                    int x2 = radom.Next(image.Width);
                    int y1 = radom.Next(image.Height);
                    int y2 = radom.Next(image.Height);
                    g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 50; i++)
                {
                    int x = radom.Next(image.Width);
                    int y = radom.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(radom.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());

            }
            catch { }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }


    }
}