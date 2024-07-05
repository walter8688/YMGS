using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using YMGS.Business.GameControlService;

namespace YMGS.Service
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            writeInLog("123",false);
            GameControl();
            InitializeComponent();
        }


        private void GameControl()
        {
            while (true)
            {
                try
                {
                    var matchDS = GameControlServiceManager.GetAutoFreezeStartMatchs();
                    foreach (var match in matchDS.TB_MATCH)
                    {
                        GameControlServiceManager.AutoFreezeStartMatch(match.MATCH_ID);
                        if (DateTime.Now.CompareTo(match.AUTO_FREEZE_DATE) > 0 || DateTime.Now.CompareTo(match.STARTDATE) > 0)
                            writeInLog(string.Format("比赛[{0}]于{1}进行自动封盘开始动作，比赛ID:{2}", match.MATCH_NAME, DateTime.Now, match.MATCH_ID), false);
                    }
                }
                catch (Exception ex)
                {
                    writeInLog(ex.Message, false);
                    throw ex;
                }
                //Thread.Sleep(5000);
            }
        }
        private void writeInLog(string msg, bool IsAutoDelete)
        {
            try
            {
                string logFileDir = System.IO.Directory.GetCurrentDirectory() + "\\log";
                string logFileName = string.Format(logFileDir + @"\\log{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                if (!Directory.Exists(logFileDir))
                {
                    Directory.CreateDirectory(logFileDir);
                }
                FileInfo fileinfo = new FileInfo(logFileName);
                if (IsAutoDelete)
                {
                    if (fileinfo.Exists && fileinfo.Length >= 1024)
                    {
                        fileinfo.Delete();
                    }
                }
                using (FileStream fs = fileinfo.OpenWrite())
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine("=====================================");
                    sw.Write("添加日期为:" + DateTime.Now.ToString() + "\r\n");
                    sw.Write("日志内容为:" + msg + "\r\n");
                    sw.WriteLine("=====================================");
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
