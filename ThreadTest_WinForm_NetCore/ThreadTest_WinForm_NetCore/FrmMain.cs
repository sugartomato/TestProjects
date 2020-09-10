using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadTest_WinForm_NetCore
{
    public partial class FrmMain : Form
    {

        private TextBox mTextConsole = null;
        public FrmMain()
        {
            InitializeComponent();

            this.Height = 500;
            this.Width = 600;

            Panel panelMain = new Panel()
            {
            };

            this.Controls.Add(panelMain);
            panelMain.Dock = DockStyle.Fill;

            FlowLayoutPanel panelToolBar = new FlowLayoutPanel();
            panelMain.Controls.Add(panelToolBar);
            panelToolBar.Dock = DockStyle.Left;
            panelToolBar.WrapContents = true;
            panelToolBar.FlowDirection = FlowDirection.LeftToRight;
            panelToolBar.Width = 300;

            mTextConsole = new TextBox();
            mTextConsole.Multiline = true;
            mTextConsole.ScrollBars = ScrollBars.Both;
            panelMain.Controls.Add(mTextConsole);
            mTextConsole.Location = new Point(305, 0);
            mTextConsole.Size = new Size(panelMain.Width - 305, panelMain.Height);
            mTextConsole.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top;



            // 添加测试按钮
            Button btnTest1 = new Button();
            btnTest1.Text = "线程池";
            btnTest1.Click += BtnTest1_Click;
            panelToolBar.Controls.Add(btnTest1);





            Action<String> ge = t =>
            {

            };
            ge("123");


            Button btnTest2 = new Button();
            btnTest2.Text = "Task";
            btnTest2.Click += BtnTest2_Click;
            panelToolBar.Controls.Add(btnTest2);

            Button btnTest3 = new Button()
            {
                Text = "清理剪切板",
            };
            btnTest3.Click += BtnTest3_Click;
            panelToolBar.Controls.Add(btnTest3);

        }

        private void BtnTest3_Click(object sender, EventArgs e)
        {
            Task task1 = new Task(() =>
            {
                this.Invoke(new Action(() =>
                {
                    Clipboard.Clear();
                    WriteConsole("子线程清理剪切板！");
                }));
            });
            Clipboard.SetText("主线程写入剪切板：12");
            WriteConsole(Clipboard.GetText());
            task1.Start();
        }

        private void BtnTest2_Click(object sender, EventArgs e)
        {
            Task task1 = new Task(() => {
                System.Threading.Thread.Sleep(200);
                WriteConsole($"task1的线程ID为{System.Threading.Thread.CurrentThread.ManagedThreadId}");
            });
            task1.Start();
        }

        private void BtnTest1_Click(object sender, EventArgs e)
        {
            for (Int32 i = 0; i < 100; i++)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((obj) =>
                {
                    WriteConsole(obj.ToString());
                }), i);
            }
        }

        private void WriteConsole(String msg)
        {
            if (mTextConsole.InvokeRequired)
            {
                mTextConsole.BeginInvoke(new Action(() =>
                {
                    mTextConsole.AppendText(msg + "\r\n");
                }));
            }
            else
            {
                mTextConsole.AppendText(msg + "\r\n");
            }
        }
    }
}
