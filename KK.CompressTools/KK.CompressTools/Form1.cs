using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;


namespace KK.CompressTools
{
    public partial class frmMain : Form
    {

        #region 窗体初始化

        public frmMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                rbtUse7Z.Checked = true;
                rbtFileType_ZIP.Checked = true;
                rbtFileType_RAR.Enabled = false;
#if DEBUG
                WriteConsole("当前工作目录：" + System.Environment.CurrentDirectory);
#endif

                // 加载7z、winrar的程序地址 通过注册表

                RegistryKey regRoot = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                RegistryKey regWinRAR = regRoot.OpenSubKey(@"SOFTWARE\WINRAR\");
                if (regWinRAR != null)
                {
                    String path = regWinRAR.GetValue("exe64")?.ToString();
                    if (String.IsNullOrEmpty(path))
                    {
                         path = regWinRAR.GetValue("exe32")?.ToString();
                    }
                    if (!String.IsNullOrEmpty(path))
                    {
                        txtWinRARCommandLine.Text = path;
                    }
                }

                RegistryKey reg7Z = regRoot.OpenSubKey(@"SOFTWARE\7-Zip\");
                if (reg7Z != null)
                {
                    String path = reg7Z.GetValue("Path")?.ToString();
                    if (String.IsNullOrEmpty(path))
                    {
                        path = reg7Z.GetValue("Path64")?.ToString();
                    }
                    if (!String.IsNullOrEmpty(path))
                    {
                        txt7ZCommandLine.Text = path + "7z.exe";
                    }
                }

                // 初始设置
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化异常：" + ex.Message);
            }
        }


        #endregion


        /// <summary>
        /// 选择压缩根目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectTargetFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtTargetFolder.Text = fbd.SelectedPath;
            }
        }

        private Boolean IsCompressing = false;
        private Boolean IsCancel = false;
        /// <summary>
        /// 执行压缩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecute_Click(object sender, EventArgs e)
        {
            
            try
            {
                panelSetting.Enabled = false;
                btnExecute.Enabled = false;
                btnStopCompress.Enabled = true;
                IsCompressing = true;
                IsCancel = false;

                Int32? totalCount = 1;

                // 构建process信息
                System.Diagnostics.ProcessStartInfo psinfo = new System.Diagnostics.ProcessStartInfo();
                psinfo.FileName = rbtUse7Z.Checked ? txt7ZCommandLine.Text : txtWinRARCommandLine.Text;
                psinfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                // 获取要压缩的文件夹下的文件和目录信息
                DirectoryInfo basefolder = new DirectoryInfo(txtTargetFolder.Text);
                FileInfo[] files = basefolder.GetFiles();
                DirectoryInfo[] dirs = basefolder.GetDirectories();
                if (chkIsParentFolder.Checked)
                {
                    totalCount = files?.Length + dirs?.Length;
                }

                // 设置进度条相关参数
                progressBar1.Maximum = (Int32)totalCount;
                progressBar1.Step = 1;
                progressBar1.Style = ProgressBarStyle.Continuous;

                Int32 currentIndex = 0;
                Task t = new Task(() => {
                    if (!chkIsParentFolder.Checked)
                    {
                        // 将所选择的目录压缩为一个单文件
                        psinfo.Arguments = GenerateArguements(basefolder.FullName, basefolder.Name, basefolder.FullName);
#if DEBUG
                        WriteConsole(psinfo.FileName);
                        WriteConsole(psinfo.Arguments);
#endif

                        currentIndex += 1;
                        this.BeginInvoke(new Action(() => {
                            progressBar1.PerformStep();
                            lblState.Text = String.Format("正在处理第【{0}】个/共【{1}】个", currentIndex, totalCount);
                        }));

                        System.Diagnostics.Process p = System.Diagnostics.Process.Start(psinfo);
                        p.WaitForExit();
                        if (p.ExitCode != 0)
                        {
                            WriteConsole("目录【" + basefolder.FullName + "】压缩失败，退出代码：" + p.ExitCode.ToString());
                        }
                        else
                        {
                            WriteConsole("目录【" + basefolder.FullName + "】压缩完成！");
                        }
                    }
                    else
                    {
                        foreach (FileInfo file in files)
                        {
                            if (IsCancel)
                            {
                                WriteConsole("用户已取消操作！");
                                break;
                            }
                            psinfo.Arguments = GenerateArguements(file.DirectoryName, System.IO.Path.GetFileNameWithoutExtension(file.Name), file.FullName);
#if DEBUG
                            WriteConsole(psinfo.FileName);
                            WriteConsole(psinfo.Arguments);
#endif

                            currentIndex += 1;
                            this.BeginInvoke(new Action(() => {
                                progressBar1.PerformStep();
                                lblState.Text = String.Format("正在处理第【{0}】个/共【{1}】个", currentIndex, totalCount);
                            }));

                            System.Diagnostics.Process p = System.Diagnostics.Process.Start(psinfo);
                            p.WaitForExit();
                            if (p.ExitCode != 0)
                            {
                                WriteConsole("文件【" + file.FullName + "】压缩失败，退出代码：" + p.ExitCode.ToString());
                            }
                            else
                            {
                                WriteConsole("文件【" + file.FullName + "】压缩完成！");
                            }
                        }
                        // 压缩目录
                        foreach (DirectoryInfo dir in dirs)
                        {
                            if (IsCancel)
                            {
                                WriteConsole("用户已取消操作！");
                                break;
                            }
                            psinfo.Arguments = GenerateArguements(dir.Parent.FullName, dir.Name, dir.FullName);
#if DEBUG
                            WriteConsole(psinfo.FileName);
                            WriteConsole(psinfo.Arguments);
#endif

                            currentIndex += 1;
                            this.BeginInvoke(new Action(() => {
                                progressBar1.PerformStep();
                                lblState.Text = String.Format("正在处理第【{0}】个/共【{1}】个", currentIndex, totalCount);
                            }));

                            System.Diagnostics.Process p = System.Diagnostics.Process.Start(psinfo);
                            p.WaitForExit();
                            if (p.ExitCode != 0)
                            {
                                WriteConsole("目录【" + dir.FullName + "】压缩失败，退出代码：" + p.ExitCode.ToString());
                            }
                            else
                            {
                                WriteConsole("目录【" + dir.FullName + "】压缩完成！");
                            }
                        }
                    }

                });
                t.Start();
                t.ContinueWith((x) => {
                    WriteConsole(String.Format("执行结束！!IsCompleted：{0},IsCanceled：{1},IsFaulted：{2}", x.IsCompleted, x.IsCanceled, x.IsFaulted));
                    IsCompressing = false;
                    panelSetting.Enabled = true;
                    btnExecute.Enabled = true;
                    btnStopCompress.Enabled = false;

                    progressBar1.Value = 0;
                    lblState.Text = "就绪！";
                });
            }
            catch (Exception ex)
            {
                WriteConsole("压缩异常：" + ex.Message);

                IsCompressing = false;
                panelSetting.Enabled = true;
                btnExecute.Enabled = true;
                btnStopCompress.Enabled = false;

                progressBar1.Value = 0;
                lblState.Text = "就绪！";
            }
            finally
            {
            }
        }


        private String GenerateArguements(String saveFolder , String fileName, String target)
        {
            String result = String.Empty;
            if (rbtUse7Z.Checked)
            {
                result = " a ";

                // 压缩文件的后缀名
                String fileEx = String.Empty;
                foreach (var ctrl in grpFileTypes.Controls)
                {
                    RadioButton rbt = ctrl as RadioButton;
                    if (rbt.Checked)
                    {
                        fileEx = rbt.Tag.ToString();
                        break;
                    }
                }
                result += String.Format("\"{0}\\{1}{2}.{3}\" ", saveFolder, fileName, (chkAddDatetimeSufix.Checked?"_" + DateTime.Now.ToString("yyyyMMddHHmmss"):""), fileEx);

                result += String.Format(" -t{0}", fileEx);

                // 压缩密码
                if (!String.IsNullOrEmpty(txtPassword.Text))
                {
                    result += " -p" + txtPassword.Text + " ";
                }

                // 压缩的目标文件夹/文件
                result += String.Format(" \"{0}\"", target);

                result = result.Replace(@"\\", @"\");
            }
            if (rbtUseRAR.Checked)
            {
                result = " a -y -m3 -inul -ibck -ep1 ";

                // 压缩文件的后缀名
                String fileEx = String.Empty;
                foreach (var ctrl in grpFileTypes.Controls)
                {
                    RadioButton rbt = ctrl as RadioButton;
                    if (rbt.Checked)
                    {
                        fileEx = rbt.Tag.ToString();
                        break;
                    }
                }

                result += String.Format(" -af{0} ", fileEx);

                result += String.Format("\"{0}\\{1}{2}.{3}\" ", saveFolder, fileName, (chkAddDatetimeSufix.Checked ? "_" + DateTime.Now.ToString("yyyyMMddHHmmss") : ""), fileEx);

                // 压缩密码
                if (!String.IsNullOrEmpty(txtPassword.Text))
                {
                    result += " -hp" + txtPassword.Text + " ";
                }

                // 压缩的目标文件夹/文件
                result += String.Format(" \"{0}\" ", target);


            }

            return result;
        }


        private void WriteConsole(String msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => {
                    txtConsole.AppendText("=".PadLeft(20, '=') + "\r\n");
                    txtConsole.AppendText(msg + "\r\n");
                }));
            }
            else
            {
                txtConsole.AppendText("=".PadLeft(20, '=') + "\r\n");
                txtConsole.AppendText(msg + "\r\n");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtConsole.Clear();
        }

        private void btnStopCompress_Click(object sender, EventArgs e)
        {
            IsCancel = true;
        }

        private void rbtUseRAR_CheckedChanged(object sender, EventArgs e)
        {
            rbtFileType_RAR.Enabled = rbtUseRAR.Checked;
            rbtFileType_7Z.Enabled = !rbtUseRAR.Checked;
        }
    }
}
