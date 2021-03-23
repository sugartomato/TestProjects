using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using KK.Common;
using KK.Common.Ex;

using CefSharp;
using CefSharp.Wpf;
using System.Threading;

namespace QYDHelper
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        CefSharp.Wpf.ChromiumWebBrowser _Browser = null;
        String _defaultUrl = "https://m.quyundong.com//court/detail?id=22887&cid=1&share=1";
        String _orderUrl = "https://m.quyundong.com/court/book?bid=22887&t={0}&cid=1";
        String _loginUrl = "http://m.quyundong.com/login/";
        String _userAccountUrl = "https://m.quyundong.com/user/account";

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                _Browser = new ChromiumWebBrowser();
                //CefSettings setting = new CefSettings();
                //Cef.Initialize(setting);
                //_Browser.Width = CTRL_BrowserPanel.Width;
                //_Browser.Height = CTRL_BrowserPanel.Height;
                CTRL_BrowserPanel.Children.Add(_Browser);

                // 注册事件
                _Browser.AddressChanged += _Browser_AddressChanged;
                _Browser.LoadingStateChanged += _Browser_LoadingStateChanged;
                _Browser.FrameLoadEnd += _Browser_FrameLoadEnd;
                _Browser.ConsoleMessage += _Browser_ConsoleMessage;


                _Browser.Address = _defaultUrl;
                CTRL_URL.Text = _defaultUrl;

                CTRL_OrderDate.SelectedDate = DateTime.Now.Date.AddDays(7);
                String[] files = System.IO.Directory.GetFiles(Environment.CurrentDirectory + "\\JS\\");
                List<String> jsFileNames = new List<string>();
                if (files != null && files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        jsFileNames.Add(System.IO.Path.GetFileName(file));
                    }
                }
                CTRL_ScriptFileList.ItemsSource = jsFileNames;

                // 加载账号
                String account = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\Phones.json");
                List<Account> accountList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Account>>(account);
                CTRL_AccountList.ItemsSource = accountList;
                CTRL_AccountList.DisplayMemberPath = "phone";
                CTRL_ScriptName.Text = "Order1_7-9.js";
                //CTRL_ScriptFileList.IsEnabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("程序初始化异常：" + ex.Message + ex.StackTrace);
            }
        }


        private System.Windows.Threading.DispatcherTimer _timerReOrder;
        private System.DateTime _lastOrderTime = DateTime.Now;
        private void _Browser_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                //Clipboard.SetText(e.Message);
                WriteConsole("收到控制台消息：" + e.Message);
                if (e.Message.ToLower() == "x1")
                {
                    System.Threading.Thread.Sleep(300);
                    WriteConsole("执行刷新！");
                    _Browser.Reload();
                    return;
                }

                // 再支付界面，开启定时器，到时间了刷新页面
                if (e.Message.ToLower() == "x2")
                {
                    //OrderCount += 1;
                    //WriteConsole($"第【{OrderCount}】次下单");
                    //if (Common.Common.IntiialArg.ReCount > 0)
                    //{
                    //    if (Common.Common.IntiialArg.ReCount < OrderCount + 1)
                    //    {
                    //        this.Close();
                    //        return;
                    //    }
                    //}

                    if (_timerReOrder == null)
                    {
                        _timerReOrder = new System.Windows.Threading.DispatcherTimer();
                        _timerReOrder.Interval = new TimeSpan(0, 0, 2);
                        _timerReOrder.Tick += _timerReOrder_Tick;
                        _timerReOrder.Stop();
                    }

                    _lastOrderTime = DateTime.Now;
                    _timerReOrder.Start();

                    CTRL_Console.Text = "";
                }
            }));
        }

        /// <summary>
        /// 下单次数
        /// </summary>
        private Int32 OrderCount = 0;

        private void _timerReOrder_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - _lastOrderTime).TotalMinutes > 8)
            {
                DateTime d = (DateTime)CTRL_OrderDate.SelectedDate;
                _Browser.Load(String.Format(_orderUrl, d.ToUnixTime()));

                _timerReOrder.Stop();
            }
        }

        private void _Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
{
    //// 订单界面，选择场地脚本
    //WriteConsole("加载完成：" + _Browser.Address);

    //if (_Browser.Address.Contains("http://m.quyundong.com/court/book?bid=22887&"))
    //{
    //    WriteConsole("开始执行下单脚本！");
    //    System.Threading.Tasks.Task t = new Task(() =>
    //    {
    //        object js = EvaluateScript(_Browser, Script_OrderGoods);
    //        WriteConsole("下单脚本结果：" + js.ToString());
    //    });
    //    t.Start();
    //    //_Browser.ExecuteScriptAsyncWhenPageLoaded(Script_OrderGoods);
    //}

    //// 提交订单界面，执行订单提交按钮
    //if (_Browser.Address.Contains("https://m.quyundong.com/order/Confirm"))
    //{
    //    WriteConsole("执行订单提交脚本！");
    //    System.Threading.Tasks.Task t = new Task(() =>
    //    {
    //        object js = EvaluateScript(_Browser, Script_CommitOrder);
    //        WriteConsole("执行订单提交脚本结果：" + js.ToString());
    //    });
    //    t.Start();
    //}


}));



        }

        String _orderScript = String.Empty;


        private async void Wait(Int32 second)
        {
            await new Task(new Action(() =>
            {
                Thread.Sleep(second * 1000);
            }));      
        }






        private void _Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            String address = String.Empty;
            String scriptfileName = String.Empty;
            Boolean? isLoadScript = true;
            Account account = null;
            this.Dispatcher.Invoke(new Action(() =>
            {
                scriptfileName = CTRL_ScriptName.Text.Trim();
                isLoadScript = CTRL_ChkLoadScript.IsChecked;
                account = new Account();
                account.phone = CTRL_Account.Text;
                account.password = CTRL_Password.Text;

                //if (CTRL_AccountList.SelectedIndex > -1)
                //{
                //    account = CTRL_AccountList.SelectedItem as Account;
                //}

                address = _Browser.Address;
            }));

            WriteConsole(scriptfileName);

            try
            {
                if (e.IsLoading == false)
                {
                    ChangeLoadState("加载完成!");


                    // 登录地址，直接执行登录
                    if (address.Contains("http://m.quyundong.com/login/"))
                    {
                        String s = "$('.login-tel').val('" + account.phone + "');$('.J_pwd').val('" + account.password + "');$('.J_submit').click();";
                        ExecuteScript(_Browser, s);
                        return;
                    }

                    if (isLoadScript != null && (Boolean)isLoadScript)
                    {
                        System.Threading.Tasks.Task t = new Task(() =>
                        {
                            object js = EvaluateScript(_Browser, _orderScript);
                        });
                        t.Start();
                    }
                }
                else
                {
                    ChangeLoadState("加载中...");
                }
            }
            catch (Exception ex)
            {
                WriteConsole("浏览器加载事件异常：" + ex.Message);
            }

        }

        /// <summary>
        ///  打开下单页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Onclick_OpenOrderPage(object sender, RoutedEventArgs e)
        {
            DateTime d = (DateTime)CTRL_OrderDate.SelectedDate;
            _Browser.Load(String.Format(_orderUrl, d.ToUnixTime()));

        }

        /// <summary>
        /// 生成刷单脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick_GenerateOrderScript(object sender, RoutedEventArgs e)
        {
            // 读取脚本模板
            String template = GetScriptByFileName("order_zepto.js");
            String placeSelect = GetScriptByFileName(CTRL_ScriptName.Text);

            _orderScript = template.Replace("//$goodsid$", placeSelect);
            WriteConsole("脚本创建完成！");
            WriteConsole(_orderScript);
        }


        private void ChangeLoadState(String state)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                CTRL_PageLoadState.Text = state;
            }));
        }

        static object EvaluateScript(ChromiumWebBrowser b, string script)
        {
            var task = b.EvaluateScriptAsync(script);
            task.Wait();
            JavascriptResponse response = task.Result;
            return response.Success ? (response.Result ?? "") : response.Message;
        }

        static void ExecuteScript(ChromiumWebBrowser b, string script)
        {
            b.ExecuteScriptAsync(script);
        }

        private void RegjQuery()
        {
            _Browser.ExecuteScriptAsync(Script_LoadjQuery);
        }

        private void _Browser_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CTRL_URL.Text = _Browser.Address;
        }

        private void OnClick_OpenURL(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("1");
            _Browser.Address = CTRL_URL.Text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Cef.Shutdown();
        }

        private void OnClick_OpenDebug(object sender, RoutedEventArgs e)
        {
            _Browser.ShowDevTools();
        }

        private void OnClick_GoBack(object sender, RoutedEventArgs e)
        {
            if (_Browser.CanGoBack) _Browser.Back();
        }

        private void OnClick_GoForward(object sender, RoutedEventArgs e)
        {
            if (_Browser.CanGoForward) _Browser.Forward();
        }

        private void OnClick_ClearConsole(object sender, RoutedEventArgs e)
        {
            CTRL_Console.Text = "";
        }


        #region 脚本区

        public Dictionary<String, String> ScriptCache = new Dictionary<string, string>();

        private String _propScript_ReloadPage = String.Empty;
        internal String Script_ReloadPage
        {
            get
            {

                _propScript_ReloadPage = GetScriptByFileName("ReloadPage.js");
                return _propScript_ReloadPage;
            }
        }

        private String _propScript_LoadjQuery = String.Empty;
        internal String Script_LoadjQuery
        {
            get
            {
                _propScript_LoadjQuery = GetScriptByFileName("LoadjQuery.js");
                return _propScript_LoadjQuery;
            }
        }


        private String _propScript_OrderGoods = String.Empty;
        internal String Script_OrderGoods
        {
            get
            {
                _propScript_OrderGoods = GetScriptByFileName("Order.js");
                return _propScript_OrderGoods;
            }
        }

        private String _propScript_CommitOrder = String.Empty;
        internal String Script_CommitOrder
        {
            get
            {
                _propScript_CommitOrder = GetScriptByFileName("CommitOrder.js");
                return _propScript_CommitOrder;
            }
        }


        private String GetScriptByFileName(String fileName)
        {
            if (ScriptCache.ContainsKey(fileName))
            {
                return ScriptCache[fileName];
            }
            String file = Environment.CurrentDirectory + "\\JS\\" + fileName;
            if (System.IO.File.Exists(file))
            {
                String result = System.IO.File.ReadAllText(file);
                ScriptCache.Add(fileName, result);
                return result;
            }
            return String.Empty;

        }

        #endregion



        #region 测试区



        private void OnClick_RealoadPage(object sender, RoutedEventArgs e)
        {
            _Browser.Reload();
            //String script = Script_ReloadPage;
            //MessageBox.Show(script);
            //if (String.IsNullOrEmpty(script))
            //{
            //    MessageBox.Show("请求的js文件不存在或者内容为空！");
            //    return;
            //}
            //_Browser.ExecuteScriptAsync(Script_ReloadPage);
        }


        private void Onclick_Order(object sender, RoutedEventArgs e)
        {
            _Browser.ExecuteScriptAsyncWhenPageLoaded(Script_OrderGoods);

        }


        private void Onclick_CommitOrder(object sender, RoutedEventArgs e)
        {
            _Browser.ExecuteScriptAsyncWhenPageLoaded(Script_CommitOrder);

        }
        private void Onclick_MyAccount(object sender, RoutedEventArgs e)
        {
            _Browser.Load(_userAccountUrl);

        }




        private void Onclick_OpenLogin(object sender, RoutedEventArgs e)
        {
            _Browser.Load(_loginUrl);
        }

        private System.Windows.Threading.DispatcherTimer _timerWaitForOrder = null;
        DateTime? orderDate = null;
        private void Onclick_StartOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                // 准备数据
                orderDate = CTRL_OrderDate.SelectedDate;
                if (orderDate == null || orderDate < DateTime.Now)
                {
                    MessageBox.Show("选择的日期不正确！");
                    return;
                }

                if (_timerWaitForOrder == null)
                {
                    _timerWaitForOrder = new System.Windows.Threading.DispatcherTimer();
                    _timerWaitForOrder.Tick += _timer_Tick;
                    _timerWaitForOrder.Interval = new TimeSpan(0, 0, 0, 0, 500);
                }

                if (_timerWaitForOrder.IsEnabled)
                {
                    _timerWaitForOrder.Stop();
                    WriteConsole("定时器已停止！");
                    CTRL_TimerState.Content = "定时器已停止！";
                    return;
                }
                else
                {
                    _timerWaitForOrder.Start();
                    CTRL_TimerState.Content = "定时器运行中！";
                    WriteConsole("开始运行定时器");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生异常：" + ex.Message);
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            // 到23点59分开始页面刷新
            if (DateTime.Now.Hour == 23 && DateTime.Now.Minute >= 59)
            {
                _Browser.Load(String.Format(_orderUrl, ((DateTime)orderDate).ToUnixTime()));
                _timerWaitForOrder.Stop();
                CTRL_TimerState.Content = "定时器已停止！(触发器内部)";

            }
            else
            {
                WriteConsole("定时器触发，时间不到，调过页面加载！");
            }

        }

        #endregion

        #region 公共方法

        
        private void WriteConsole(String msg)
        {
            msg = DateTime.Now.ToString("HH:mm:ss") + ":" + msg + "\r\n";
            CTRL_Console.Dispatcher.Invoke(new Action(() =>
            {
                Boolean? c = CTRL_IsLimitMessageLength.IsChecked;
                if (c != null && (Boolean)c)
                {
                    if (CTRL_Console.Text.Length > 6000)
                    {
                        CTRL_Console.Text = CTRL_Console.Text.Substring(CTRL_Console.Text.Length - 600, 600) + msg;
                    }
                    else
                    {
                        CTRL_Console.Text += msg;
                    }
                }
                else
                {
                    CTRL_Console.Text += msg;
                }
                CTRL_ScrollConsole.ScrollToEnd();
            }));

        }

        #endregion


        private void CTRL_ScriptFileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CTRL_ScriptName.Text = CTRL_ScriptFileList.SelectedItem.ToString();
        }

        /// <summary>
        /// 账号选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CTRL_AccountList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Account a = CTRL_AccountList.SelectedItem as Account;
            CTRL_Account.Text = a.phone;
            CTRL_Password.Text = a.password;
        }

        public class Account
        {
            public String phone { get; set; }
            public String password { get; set; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                // 根据参数决定是否自动运行
                if (Common.Common.IntiialArg != null && Common.Common.IntiialArg.Phone.Length > 0)
                {

                    CTRL_ScriptName.Text = Common.Common.IntiialArg.Script;
                    CTRL_Account.Text = Common.Common.IntiialArg.Phone;
                    CTRL_Password.Text = "123456";

                    // 生成下单脚本
                    OnClick_GenerateOrderScript(null, null);

                    if (_Browser.IsBrowserInitialized)
                    {
                        DoLogin();
                    }
                    else
                    {
                        _Browser.IsBrowserInitializedChanged += _Browser_IsBrowserInitializedChanged;
                    }

                    WriteConsole(nameof(Window_ContentRendered) + "事件结束！");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序ContentRendered事件异常：" + ex.Message + ex.StackTrace);
            }
        }

        private void _Browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            WriteConsole("浏览器IsBrowserInitializedChanged事件触发");

            //ChromiumWebBrowser b = (ChromiumWebBrowser)sender;
            if (_Browser.IsBrowserInitialized)
            {
                this.Dispatcher.BeginInvoke(new Action(()=> {
                    DoLogin();
                }));
            }
        }

        private void DoLogin()
        {
            Onclick_OpenLogin(null, null);

            while (true)
            {
                if (_Browser.Address.Contains("m.quyundong.com/index/getCity") || _Browser.Address.Contains(""))
                {
                    break;
                }
                Wait(3);
            }

            // 执行下单
            WriteConsole("程序启动执行自动下单！");
            Onclick_StartOrder(null, null);
        }
    }
}
