using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Common;
using YMGS.Business.Cache;
using YMGS.Data.Presentation;
using YMGS.Manage.Web.Common;
using YMGS.Business.SystemSetting;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using YMGS.Business.AssistManage;
using YMGS.Business.GameMarket;

namespace YMGS.Manage.Web.SystemSetting
{
    [LeftMenuId(FunctionIdList.AssistantManagement.AdsManagePage)]
    [TopMenuId(FunctionIdList.AssistantManagement.AssistantManageModule)]
    public partial class ADRegion : QueryBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMatchlist();
                hfdaccessable.Value = MySession.Accessable(FunctionIdList.AssistantManagement.AdsManage) ? "1" : "0";
                BindMain();
                GetTopRaceData();//置顶比赛
            }
        }
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(ADRegion), "SystemSetting").AbsoluteUri;
        }
        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.AssistantManagement.AdsManagePage;
            }
        }
        protected override DataTable GetData()
        {
            return ADManager.QueryDSADWords().TB_AD_WORDS;
        }

        private void BindMain()
        {
            gdvMain.DataSource = GetData();
            gdvMain.DataBind();
        }

        public DSADWords DataSource()
        {
            var dsresult = ADManager.QueryDSADWords();
            return dsresult;
        }
        private void ClearText()
        {
            hfdid.Value = string.Empty;
            txttitle.Text = string.Empty;
            txttitleen.Text = string.Empty;
            txtdesc.Text = string.Empty;
            txtdescen.Text = string.Empty;
            txtweblink.Text = string.Empty;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                ClearData();
                this.btnSave.CommandArgument = ButtonCommandType.Add.ToString();
                mdlPopup.Show();
            }
            if (sender is LinkButton)
            {
                LinkButton btn = sender as LinkButton;
                var commandString = btn.CommandArgument;
                hfdid.Value = commandString;
                DSADWords ds = DataSource();
                var data = ds.TB_AD_WORDS.Where(s => s.AD_WORDS_ID.ToString() == hfdid.Value).FirstOrDefault();

                txttitle.Text = data.TITLE;
                txttitleen.Text = data.TITLE_EN;
                txtdesc.Text = data.DESC;
                txtdescen.Text = data.DESC_EN;
                txtweblink.Text = data.WEBLINK;

                this.btnSave.CommandArgument = ButtonCommandType.Edit.ToString();
                mdlPopup.Show();
            }
        }

        public void ClearData()
        {
            hfdid.Value = "0";
            txttitle.Text = "";
            txttitleen.Text = "";
            txtdesc.Text = "";
            txtdescen.Text = "";
            txtweblink.Text = "";
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = sender as LinkButton;
                hfdid.Value = btn.CommandArgument;
                DSADWords.TB_AD_WORDSRow data = SetEntity();
                ADManager.EditDSADWords(data, 2);
                ClearData();
                BindMain();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DSADWords.TB_AD_WORDSRow data = SetEntity();
                if (btnSave.CommandArgument == ButtonCommandType.Add.ToString())
                {
                    ADManager.EditDSADWords(data, 1);
                }
                if (btnSave.CommandArgument == ButtonCommandType.Edit.ToString())
                {
                    ADManager.EditDSADWords(data, 3);
                }
                ClearData();
                BindMain();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        private DSADWords.TB_AD_WORDSRow SetEntity()
        {
            DSADWords ds = new DSADWords();
            DSADWords.TB_AD_WORDSRow data = (DSADWords.TB_AD_WORDSRow)ds.TB_AD_WORDS.NewRow();
            data.AD_WORDS_ID = int.Parse(hfdid.Value);
            data.TITLE = txttitle.Text;
            data.TITLE_EN = txttitleen.Text;
            data.DESC = txtdesc.Text;
            data.DESC_EN = txtdescen.Text;
            data.WEBLINK = txtweblink.Text;
            return data;
        }

        protected void btnUploadcn_Click(object sender, EventArgs e)
        {
            if (fileuploadcn.PostedFile.ContentLength > 2 * 1024 * 1024 || fileuploaden.PostedFile.ContentLength > 2 * 1024 * 1024)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('请选择2M以内的广告图片上传!');", true);
                return;
            }
            if (!fileuploadcn.HasFile)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('请选择中文广告图片上传!');", true);
                return;
            }
            if (!fileuploaden.HasFile)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('请选择英文广告图片上传!');", true);
                return;
            }
            try
            {
                string contenttype = fileuploadcn.PostedFile.ContentType;
                string encontenttype = fileuploaden.PostedFile.ContentType;
                if ((contenttype == "image/png" || contenttype == "image/bmp" || contenttype == "image/gif" || contenttype == "image/pjpeg" || contenttype == "image/jpeg") &&
                   (encontenttype == "image/png" || encontenttype == "image/bmp" || encontenttype == "image/gif" || encontenttype == "image/pjpeg" || encontenttype == "image/jpeg"))
                {
                    int cnlength = fileuploadcn.PostedFile.ContentLength;
                    int enlength = fileuploaden.PostedFile.ContentLength;

                    byte[] cnbyteData = new byte[cnlength];
                    byte[] enbyteData = new byte[enlength];
                    fileuploadcn.PostedFile.InputStream.Read(cnbyteData, 0, cnlength);
                    fileuploaden.PostedFile.InputStream.Read(enbyteData, 0, enlength);

                    string strCnFileFormat = fileuploadcn.PostedFile.FileName.Substring(fileuploadcn.PostedFile.FileName.LastIndexOf(".") + 1);
                    string strEnFileFormat = fileuploaden.PostedFile.FileName.Substring(fileuploaden.PostedFile.FileName.LastIndexOf(".") + 1);

                    byte[] cnzipData = ZipPicture(cnbyteData, 380, true, strCnFileFormat);
                    byte[] enzipData = ZipPicture(enbyteData, 380, false, strEnFileFormat);

                    DSADPic pic = new DSADPic();
                    DSADPic.TB_AD_PICRow row = pic.TB_AD_PIC.NewRow() as DSADPic.TB_AD_PICRow;
                    row.PIC_ADDRESS = cnzipData;
                    row.PIC_ADDRESS_EN = enzipData;
                    ADManager.EditADPic(row, 1);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('上传成功!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('请选择图片上传!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('" + ex.Message + "');", true);
            }

        }

        //参数是图片的路径
        public byte[] GetPictureData(string imagePath)
        {
            FileStream fs = new FileStream(imagePath, FileMode.Open);
            byte[] byteData = new byte[fs.Length];
            fs.Read(byteData, 0, byteData.Length);
            fs.Close();
            return byteData;
        }

        protected void gdvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool flag = hfdaccessable.Value == "1" ? true : false;
                LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
                LinkButton btndel = e.Row.FindControl("btndel") as LinkButton;
                btndel.Visible = btnEdit.Visible = flag;
            }
        }

        public DSTopRace.TB_AD_TOPRACERow GetTopRace()
        {
            DSTopRace ds = TopRaceManager.QueryTopRace();
            if (ds.TB_AD_TOPRACE.Rows.Count > 0)
            {
                var data = ds.TB_AD_TOPRACE.FirstOrDefault();
                return data;
            }
            return null;
        }

        public void LoadMatchlist()
        {
            ddlmatchmarket.DataValueField = "MARCHID";
            ddlmatchmarket.DataTextField = "MARCHNAME";
            var matchlist = MatchManager.QueryMatchAndMarketForBetting(); ;

            var data = from s in matchlist.Match_List
                       select new { MARCHID = s.MATCH_ID, MARCHNAME = s.MATCH_NAME };
            ddlmatchmarket.DataSource = data;
            ddlmatchmarket.DataBind();
            ddlmatchmarket.Items.Insert(0, new ListItem("该比赛已过期请重新选择置顶比赛",""));
        }

        public void GetTopRaceData()
        {
            var data = GetTopRace();
            if (data == null)
                return;
           ListItem li= ddlmatchmarket.Items.FindByValue(data.MARCHID.ToString());
           if (li == null)
               ddlmatchmarket.SelectedValue = "";
           else
            ddlmatchmarket.SelectedValue = data.MARCHID.ToString();
            txtcnTitle.Text = data.CNTITLE;
            txtenTitle.Text = data.ENTITLE;
            txtcnContext.Text = data.CNCONTENT;
            txtenContent.Text = data.ENCONTENT;
        }
        public DSTopRace.TB_AD_TOPRACERow SetTopRaceData()
        {
            DSTopRace ds = new DSTopRace();
            DSTopRace.TB_AD_TOPRACERow data = ds.TB_AD_TOPRACE.NewRow() as DSTopRace.TB_AD_TOPRACERow;
            data.MARCHID = int.Parse(ddlmatchmarket.SelectedValue);
            data.CNTITLE = txtcnTitle.Text;
            data.ENTITLE = txtenTitle.Text;
            data.CNCONTENT = txtcnContext.Text;
            data.ENCONTENT = txtenContent.Text;
            int cnlength = fulcn.PostedFile.ContentLength;
            int enlength = fulen.PostedFile.ContentLength;
            byte[] cnbyteData = new byte[cnlength];
            byte[] enbyteData = new byte[enlength];
            fulcn.PostedFile.InputStream.Read(cnbyteData, 0, cnlength);
            fulen.PostedFile.InputStream.Read(enbyteData, 0, enlength);

            string strCnFileFormat = fulcn.PostedFile.FileName.Substring(fulcn.PostedFile.FileName.LastIndexOf(".") + 1);
            string strEnFileFormat = fulen.PostedFile.FileName.Substring(fulen.PostedFile.FileName.LastIndexOf(".") + 1);
            byte[] cnzipData = ZipPicture(cnbyteData, 180, false, strCnFileFormat);
            byte[] enzipData = ZipPicture(enbyteData, 180, false, strEnFileFormat);

            data.CNPIC = cnzipData;
            data.ENPIC = enzipData;
            return data;
        }

        protected void btnSaveTopRace_Click(object sender, EventArgs e)
        {
            try
            {
                if (fulcn.PostedFile.ContentLength > 2 * 1024 * 1024 || fulen.PostedFile.ContentLength > 2 * 1024 * 1024)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('请选择2M以内的图片上传!');", true);
                    return;
                }
                if (!fulcn.HasFile)
                {
                    PageHelper.ShowMessage(this.Page, "请上传中文图片");
                    return;
                }
                if (!fulen.HasFile)
                {
                    PageHelper.ShowMessage(this.Page, "请上传英文图片");
                    return;
                }

                string contenttype = fulcn.PostedFile.ContentType;
                string encontenttype = fulen.PostedFile.ContentType;
                if ((contenttype == "image/png" || contenttype == "image/bmp" || contenttype == "image/gif" || contenttype == "image/pjpeg" || contenttype == "image/jpeg") &&
                   (encontenttype == "image/png" || encontenttype == "image/bmp" || encontenttype == "image/gif" || encontenttype == "image/pjpeg" || encontenttype == "image/jpeg"))
                {
                    DSTopRace.TB_AD_TOPRACERow row = SetTopRaceData();
                    TopRaceManager.EditNotice(row, 1);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('保存置顶比赛成功!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showmessage", "alert('请选择图片上传!');", true);
                }
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        private byte[] ZipPicture(byte[] picDatas,int iWidthOrHeight, bool bIsWidth,string fileFormat)
        {
            int iCurWidth;
            int iCurHeight;
            MemoryStream oriStream = new MemoryStream(picDatas);
            Bitmap oriBitMap = new Bitmap(oriStream);
            if (bIsWidth)
            {
                if (oriBitMap.Width > iWidthOrHeight)
                    iCurWidth = iWidthOrHeight;
                else
                    iCurWidth = oriBitMap.Width;

                iCurHeight = (int)((iCurWidth / (oriBitMap.Width * 1.0)) * oriBitMap.Height);
            }
            else
            {
                if (oriBitMap.Height > iWidthOrHeight)
                    iCurHeight = iWidthOrHeight;
                else
                    iCurHeight = oriBitMap.Height;

                iCurWidth = (int)((iCurHeight / (oriBitMap.Height * 1.0)) * oriBitMap.Width);
            }

            Bitmap zipBitMap = new Bitmap(iCurWidth, iCurHeight, PixelFormat.Format24bppRgb);
            zipBitMap.SetResolution(80, 80);
            Graphics graphics = Graphics.FromImage(zipBitMap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            graphics.DrawImage(oriBitMap, new Rectangle(0, 0, iCurWidth, iCurHeight), 0, 0, oriBitMap.Width,
                                oriBitMap.Height, GraphicsUnit.Pixel);

            try
            {
                MemoryStream zipStream = new MemoryStream();
                switch (fileFormat.ToLower())
                {
                    case "gif":
                        zipBitMap.Save(zipStream, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "png":
                        zipBitMap.Save(zipStream, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        zipBitMap.Save(zipStream, System.Drawing.Imaging.ImageFormat.Jpeg);                        
                        break;
                }

                byte[] resultBytes = null;
                resultBytes = zipStream.GetBuffer();
                zipStream.Close();
                zipStream.Dispose();
                oriStream.Close();
                oriStream.Dispose();
                oriBitMap.Dispose();
                zipBitMap.Dispose();
                graphics.Dispose();
                return resultBytes;
            }
            catch
            {
                return null;
            }
        }
    }
}