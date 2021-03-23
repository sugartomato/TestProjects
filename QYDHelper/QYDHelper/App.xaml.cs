using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QYDHelper
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {


        public void OnAppStartup(object sender, StartupEventArgs e)
        {

            if (e.Args != null && e.Args.Length == 3)
            {
                Common.Common.IntiialArg = new Args();
                Common.Common.IntiialArg.Phone = e.Args[0];
                Common.Common.IntiialArg.Script = e.Args[1];

                Common.Common.IntiialArg.ReCount = Int32.Parse(e.Args[2].Trim());
            }

            MainWindow m = new MainWindow();
            m.Show();

        }
    }
}
