using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
         Playlist myPl = new Playlist();
        String token;
         int n;
         string topOfArtist;
        public Window1()
        {
            InitializeComponent();
        }

        

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd= new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "Video Files|*.avi;*.mpeg;*.wmv|" +
                "Music Files|*.mp3;*.wma|" +
                "All Files|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //listBox1.Items.Add(ofd.FileName);
                //mediaElement1.Source = new Uri(ofd.FileName);
                Song s = new Song("Karma Police", "2", ofd.FileName);
                myPl.AddSong(s);
            }



            

        }

        private void listBox1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Song s = (Song)myPl.pl[listBox1.SelectedIndex];
            if (myPl.GetISong(listBox1.SelectedIndex).Source == "")
            {
                myPl.SetSourceToISong(listBox1.SelectedIndex, VkSearch.GetSongUri(myPl.GetISong(listBox1.SelectedIndex), token));


            }
            
            Uri ur = new Uri(myPl.GetISong(listBox1.SelectedIndex).Source);
            
            mediaElement1.Source = ur;
            
           
            mediaElement1.Play();
           
        }

        

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            foreach (Song s in myPl.pl)
            {
                listBox1.Items.Add(s.SongName);
            }
        }

        
        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            //myPl.Clear();
            listBox1.Items.Clear();
            Regex r = new Regex(@".*");
            Regex r1 = new Regex(@"[0-9]*");
            Regex r2 = new Regex(@":.*");
            string[] s1;
            string[] s2;
            string[] topSearch = r.Match(textBox1.Text).Value.Replace("top", "").Split('+');
            foreach (string s in topSearch)
            {
                
                LastFmSearch.TopTracks(myPl, Convert.ToInt32(r1.Match(s).Value), r2.Match(s).Value.Replace(":",""));
            }

         //   n = Convert.ToInt32(topSearch[0]);
           // topOfArtist = topSearch[1];
            //LastFmSearch.TopTracks(myPl, n, topOfArtist);
            for (int i = 0; i < myPl.pl.Count; i++)
            {
                Song s = (Song)myPl.pl[i];
                listBox1.Items.Add(s.GroupName+"-"+s.SongName);

            }
            

            webBr.Navigate("http://api.vkontakte.ru/oauth/authorize?client_id=13&scope=audio&redirect_uri=http://api.vkontakte.ru/blank.html&response_type=token");

            
        }
        private void webBr_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            
            Regex reg = new Regex(@"=(.*)&");
            string[] json = reg.Match(webBr.Url.ToString()).Value.Replace("=", "").Split('&');
            token = json[0];
            VkSearch.GetUri(myPl, token);
        }

       /* private void button2_Click(object sender, RoutedEventArgs e)
        {
            //string s;
            //s = webBr.Url.ToString();
            Regex reg = new Regex(@"=(.*)&e");
            string json = reg.Match(webBr.Url.ToString()).Value.Replace("=", "").Replace("&", "").Replace("e", "");
            token = json;
        }*/

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = token;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Play();
        }

        private void button3_Click_1(object sender, RoutedEventArgs e)
        {
            mediaElement1.Pause();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Stop();
        }


        private void mediaElement1_MediaEnded(object sender, RoutedEventArgs e)
        {
            Random rnd= new Random();
            int numb = Convert.ToInt32(rnd.Next(myPl.pl.Count-1));
            
            if (myPl.GetISong(numb).Source == "")
            {
                myPl.SetSourceToISong(numb, VkSearch.GetSongUri(myPl.GetISong(numb), token));


            }

            Uri ur = new Uri(myPl.GetISong(numb).Source);

            mediaElement1.Source = ur;


            mediaElement1.Play();
            
        }
       

       
    }
}
