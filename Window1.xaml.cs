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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Artist testArtist= new Artist();
        
         Playlist myPl = new Playlist();
       public String token;
         int n;
         string topOfArtist;
         
         Queue<string> history = new Queue<string>(10);

        
         
      

        public Window1()
        {
            InitializeComponent();
            Window2 wndow2 = new Window2();
           // wndow2.InitializeComponent();
           // webBr.Navigate("http://api.vkontakte.ru/oauth/authorize?client_id=13&scope=audio&redirect_uri=http://api.vkontakte.ru/blank.html&response_type=token");
            this.Hide();
            wndow2.Show();
            //token = wndow2.token;
            //label1.Content = token;
            
        }
        
        

     
        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
          /* if (textBox1.Text.Substring(0, 6) == "artist")
            {
                testArtist = Engine.ParseArtist(textBox1.Text);
                int i = 0;
                foreach (string x in testArtist.AlbumsName)
                {
                    System.Windows.Controls.Label tempLabel = new System.Windows.Controls.Label();
                    tempLabel.Content = x;
                    tempLabel.Tag = testArtist.AlbumsId[i];
                     tempLabel.MouseEnter += msenter;
                    tempLabel.MouseLeave += msleave;
                    tempLabel.MouseDoubleClick += msdblclick;
                    stackPanel1.Children.Add(tempLabel);
                    i++;

                }
            }*/
           if (textBox1.Text.Substring(0, 3) == "top")
           {
               myPl.Add(Engine.ParseTop(textBox1.Text));
               listBox1.Items.Clear();
               myPl.Play(listBox1);
           }
         //  TextWriter tempWriter = new StreamWriter(@"C:\Users\vlad\test.txt",true);
           //tempWriter.WriteLine(textBox1.Text);
           //tempWriter.Close();
           ComboBoxItem tempItem1 = new ComboBoxItem();
           tempItem1.Content = textBox1.Text;
           if (comboBox1.Items.Count == 10)
           {
               comboBox1.Items.RemoveAt(9);
               
           }
           comboBox1.Items.Insert(0, tempItem1);
           if (history.Count == 10)
           {
               history.Dequeue();
           }
           history.Enqueue(textBox1.Text);

            
        }

        
      

        private void msenter(object sender, System.Windows.Input.MouseEventArgs e)
        {

            System.Windows.Controls.Label temp = (System.Windows.Controls.Label)sender;
            temp.FontWeight=FontWeights.Bold;
        }
        private void msleave(object sender, System.Windows.Input.MouseEventArgs e)
        {

            System.Windows.Controls.Label temp = (System.Windows.Controls.Label)sender;
            temp.FontWeight = FontWeights.Normal;
            
        }
        private void msdblclick(object sender, System.Windows.Input.MouseEventArgs e)
        {

            System.Windows.Controls.Label temp = (System.Windows.Controls.Label)sender;
            Album newAlbum = new Album(textBox1.Text.Replace("artist:", ""), temp.Content.ToString(), temp.Tag.ToString());
            newAlbum.GetSongs();
            foreach (Song x in newAlbum.SongsList)
            {
                myPl.AddSong(x);
                
            }
            myPl.Play(listBox1);

            
            

            
            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string str1;
                str1 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "history.txt");
                TextReader tempReader = new StreamReader(str1);
               

                string tempStr;
                int j = 0;
                j = Int32.Parse(tempReader.ReadLine());
                for (int i = 0; i <= j; i++)
                {
                    ComboBoxItem tmpItem = new ComboBoxItem();
                    comboBox1.Items.Add(tmpItem);
                }
                while ((tempStr = tempReader.ReadLine()) != null)
                {
                    ComboBoxItem tempItem = new ComboBoxItem();
                    tempItem.Content = tempStr;
                    comboBox1.Items.RemoveAt(j - 1);
                    comboBox1.Items.Insert(j - 1, tempItem);
                    history.Enqueue(tempStr);
                    j--;

                }
                tempReader.Close();
            }
            catch { }

            
            
        }
        private void PlaySong(Song s)
        {
            //if (s.Source == "")
            
                VkSearch.GetSongUri(s, token);
                Uri ur = new Uri(s.Source);
                mediaElement1.Source = ur;
                mediaElement1.LoadedBehavior = MediaState.Manual;
                mediaElement1.Play();
            
           // timer();

        }

        private void listBox1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                PlaySong(myPl.ListOfSongs[listBox1.SelectedIndex]);
                
            }
        }

       /* private void webBr_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //webBr.Visible = false;
            
            Regex reg = new Regex(@"=(.*)&");
            string[] json = reg.Match(webBr.Url.ToString()).Value.Replace("=", "").Split('&');
            token = json[0];
            
        }*/

        private void savePL_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog serializDialog = new SaveFileDialog();
            serializDialog.InitialDirectory = "C:\tmp";
            serializDialog.ShowDialog();
            Stream serializeStream = File.Create(serializDialog.FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(serializeStream, myPl);
            serializeStream.Close();



        }

        private void loadPL_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog deserializDialog = new OpenFileDialog();
            deserializDialog.InitialDirectory = "C:\tmp";
            deserializDialog.ShowDialog();
            if (File.Exists(deserializDialog.FileName))
            {
                myPl.Clear();
                
                listBox1.Items.Clear();
                Stream desStream = File.OpenRead(deserializDialog.FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                myPl = (Playlist)deserializer.Deserialize(desStream);
                desStream.Close();
                myPl.Play(listBox1);

            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Play();

        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Pause();
        }
        private void mediaElement1_MediaEnded(object sender, RoutedEventArgs e)
        {
            int next;
            if ((bool)checkBox2.IsChecked)
            {
                PlaySong(myPl.ListOfSongs[listBox1.SelectedIndex]);
                return;
            }
            if ((bool)checkBox1.IsChecked)
            {
                Random rnd = new Random();
                next = Convert.ToInt32(rnd.Next(1, myPl.Count() - 1));
            }
            else
            { next = (listBox1.SelectedIndex +1)%myPl.Count(); }
            PlaySong(myPl.ListOfSongs[next]);
            listBox1.SelectedIndex = next;

        }
       private void timer()
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(2);
            dt.Tick += new EventHandler(dt_Tick);
            dt.Start();
        }

       private void dt_Tick(object sender, EventArgs e)
        {
            
            //songSlider.Value = mediaElement1.Position.TotalSeconds;
        
        }
        

        

        

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            myPl.Clear();
            listBox1.Items.Clear();
        }

        private void sliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement1.Volume = slider1.Value;
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            myPl.Shuffle();
            listBox1.Items.Clear();
            myPl.Play(listBox1);
        }

        private void gridMouseWheel(object sender, MouseWheelEventArgs e)
        {
            
            slider1.Value +=0.05*(e.Delta/120);
            
        }

        private void keyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
               // button2.AddToEventRoute+=button2.Click();
                button2_Click(sender, e);
            }
            
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp=(ComboBoxItem)comboBox1.SelectedItem;
            textBox1.Text = temp.Content.ToString();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            string str1;
            str1 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "history.txt");
            TextWriter writer = new StreamWriter(str1, false);
            writer.WriteLine(history.Count);
            
            foreach (string item in history)
            {
                writer.WriteLine(item);
            }
            writer.Close();
        }

       


        

     

          
       

       
    }
}
