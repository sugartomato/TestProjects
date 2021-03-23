using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Pipes;
using System.Linq;
using System.Security.Policy;
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

namespace RandomNumberPick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Random mRandomTarget = null;
        private Int32 mMinValue = 1;
        private Int32 mMaxValue = 151;
        private Int32 mCurrentSelect = Int32.MinValue;
        private Boolean IsDoRandom = false;
        private List<Int32> mSelectedNumbers = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
            BitmapImage img = new BitmapImage(new Uri(".\\Image\\back.jpg", UriKind.Relative));
            CTRL_IMG_Background.ImageSource = img;
            //CTRL_IMG_Background.ImageSource 
            mRandomTarget = new Random();
            CTRL_BTN_START.Focus();



        }

        private void OnClick_StopRandom(object sender, RoutedEventArgs e)
        {
            IsDoRandom = false;
        }




        private void OnClick_StartRandom(object sender, RoutedEventArgs e)
        {
            if (IsDoRandom)
            {
                IsDoRandom = !IsDoRandom;
                mSelectedNumbers.Add(mCurrentSelect);
                CTRL_TXT_ButtonText.Text = "开始";
                CTRL_BTN_START.Style = this.Resources["DefaultButton"] as Style;
                CTRL_GRID_RangeSetting.IsEnabled = true;

                if (mSelectedNumbers != null && mSelectedNumbers.Count > 0)
                {
                    String txt = String.Empty;
                    foreach (var n in mSelectedNumbers)
                    {
                        txt += n.ToString() + ",";
                    }
                    CTRL_TXT_Status.Text = txt;
                }
            }
            else

            {
                IsDoRandom = !IsDoRandom;
                Task.Factory.StartNew(new Action(() =>
                {
                    Task t = new Task(() => { PickRandomNumber(); });
                    t.Start();
                }));

                CTRL_TXT_ButtonText.Text = "停止";
                CTRL_BTN_START.Style = this.Resources["StopButton"] as Style;
                CTRL_GRID_RangeSetting.IsEnabled = false;
            }
        }

        private void PickRandomNumber()
        {
            while (IsDoRandom)
            {
                while (true)
                {
                    // 去重
                    mCurrentSelect = mRandomTarget.Next(mMinValue, mMaxValue);
                    if (mSelectedNumbers != null && !mSelectedNumbers.Contains(mCurrentSelect))
                    {
                        break;
                    }
                }
                Action<String> action = new Action<string>(UpdateUI);
                this.Dispatcher.BeginInvoke(action, mCurrentSelect.ToString());
                System.Threading.Thread.Sleep(20);
            }
        }

        private void UpdateUI(String val)
        {
            CTRL_TXT_Number.Text = val;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                //OnClick_StartRandom(null,null);
            }
        }

        private void OnClick_SettingRange(object sender, RoutedEventArgs e)
        {
            mMinValue = Int32.Parse(CTRL_TXT_StartNumber.Text.Trim());
            mMaxValue = Int32.Parse(CTRL_TXT_EndNumber.Text.Trim()) + 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mSelectedNumbers.Clear();
            mRandomTarget = new Random();
            CTRL_TXT_Status.Text = "已复位";
            CTRL_TXT_Number.Text = mMinValue.ToString();
        }

        private void OnClick_ChangeTextColor(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Media.Color c = System.Windows.Media.Color.FromArgb(cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B);
                CTRL_TXT_Number.Foreground = new SolidColorBrush(c);
            }

        }
    }
}
