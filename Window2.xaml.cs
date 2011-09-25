using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
namespace TBP
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public string token;
        public Window2()
        {
            InitializeComponent();
            webBr.Navigate("http://api.vkontakte.ru/oauth/authorize?client_id=13&scope=audio&redirect_uri=http://api.vkontakte.ru/blank.html&response_type=token");
        }

        private void webBr_Navigated(object sender, System.Windows.Forms.WebBrowserNavigatedEventArgs e)
        {
            
                Regex reg = new Regex(@"=(.*)&");
                string[] json = reg.Match(webBr.Url.ToString()).Value.Replace("=", "").Split('&');
                token = json[0];
                if (token.Length>=10)
                {
                    this.Close();
                }
            
        }

        private void w2Closed(object sender, EventArgs e)
        {
            var window1 = System.Windows.Application.Current.Windows.OfType<Window1>().FirstOrDefault();
            window1.token = token;
            window1.Show();
        }
    }
}
