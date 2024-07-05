using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using YMGS.Business.GameControlService;
using YMGS.Data.DataBase;
using YMGS.Data.Common;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace YMGS.Service
{
    partial class GameControlService : ServiceBase
    {
        public GameControlService()
        {
            InitializeComponent();
        }
        public bool Flag = true;
        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            StartGameControl();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            try
            {
                ThreadGameControl.Abort();

                Flag = false;

                System.Diagnostics.Trace.Write("线程停止");
            }
            catch (Exception ex)
            {
                writeInLog(ex.Message, false);
            }
        }

        private Thread ThreadGameControl;

        private void StartGameControl()
        {
            try
            {
                ThreadGameControl = new Thread(new ThreadStart(GameControl));
                ThreadGameControl.Start();
                System.Diagnostics.Trace.Write("线程任务开始");
            }
            catch (Exception ex)
            {
                writeInLog(ex.Message, false);
            }
        }

        private void GameControl()
        {
            while (Flag)
            {
                try
                {
                    var matchDS = GameControlServiceManager.GetAutoFreezeStartMatchs();
                    foreach (var match in matchDS.TB_MATCH)
                    {
                        GameControlServiceManager.AutoFreezeStartMatch(match.MATCH_ID);
                        if (DateTime.Now.Subtract(match.AUTO_FREEZE_DATE).Seconds >= 0 && DateTime.Now.Subtract(match.AUTO_FREEZE_DATE).Seconds <= 5)
                        {
                            writeInLog(string.Format("比赛{0}于{1}进行自动封盘清理市场动作，比赛ID{2}", match.MATCH_NAME, DateTime.Now, match.MATCH_ID), false);
                        }
                        if (DateTime.Now.Subtract(match.STARTDATE).Seconds >= 0 && match.STATUS == (int)MatchStatusEnum.Activated && match.ADDITIONALSTATUS != (int)MatchAdditionalStatusEnum.Suspended)
                        {
                            writeInLog(string.Format("比赛{0}于{1}进行自动开始比赛动作，比赛ID{2}", match.MATCH_NAME, DateTime.Now, match.MATCH_ID), false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    writeInLog(ex.Message,false);
                }
                Thread.Sleep(5000);
            }
        }

        private void writeInLog(string msg, bool IsAutoDelete)
        {
            try
            {
                //string logFileDir = @"C:\YMGS";
                string logFileDir = Application.StartupPath + @"\\log";
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
