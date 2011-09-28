using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Diagnostics;
using System.IO;


namespace TBP
{
    class Engine
    {

        public static Artist ParseArtist(string Query)
        {
           Artist tempArtist=new Artist(Query.Replace("artist:",""));
           tempArtist.GetAlbums();
           return tempArtist;
            

        }
        public static Playlist ParseTop(string Query)
        {
            Playlist myPl2 = new Playlist();
            Regex r = new Regex(@".*");
            Regex r1 = new Regex(@"[0-9]*");
            Regex r2 = new Regex(@":.*");
            string[] s1;
            string[] s2;
            string[] topSearch = r.Match(Query).Value.Replace("top", "").Split('+');
            foreach (string s in topSearch)
            {
                List<Song> temp = new List<Song>();

                temp = LastFmSearch.TopTracks(Convert.ToInt32(r1.Match(s).Value), r2.Match(s).Value.Replace(":", ""));
                foreach (Song s6 in temp)
                {
                    myPl2.AddSong(s6);
                }

            }
            return myPl2;
        }
        public static Playlist ParseSimilar(string query)
        {
            Playlist myPl2 = new Playlist();
            Regex r = new Regex(@".*");
            Regex r1 = new Regex(@"[0-9]*");
            Regex r2 = new Regex(@":.*");
            
            string[] simSearch = r.Match(query).Value.Replace("similar:", "").Split('+');
            foreach (string s in simSearch)
            {
               // 
                string[] tempp = new string[5];

                Stopwatch sw = new Stopwatch();
                Stopwatch sw2 = new Stopwatch();
                sw.Start();
                tempp= LastFmSearch.SimArtist(s);
               // sw.Stop();
               //writer.WriteLine(sw.ElapsedMilliseconds);
                
              // sw.Start();
                foreach (string s1 in tempp)
                {
                    //List<Song> temp = new List<Song>();
                  //  sw2.Start();
                   // temp = LastFmSearch.TopTracks(5, s1);
                    
                   sw2.Start();
                   foreach (Song s6 in LastFmSearch.TopTracks(5, s1))
                    {
                        myPl2.AddSong(s6);
                        
                    }
                  

                }
                sw.Stop();
                TextWriter writer = new StreamWriter(@"C:\debug.txt", true);
               writer.WriteLine("Total:"+sw.ElapsedMilliseconds);
                writer.WriteLine("Toptrack summ" + sw2.ElapsedMilliseconds);
               writer.Close();
            }
            
            return myPl2;
        }
        public static void ShowAlbums(Artist artist)
        {
            ListBox lbx = new ListBox();
            foreach (string x in artist.AlbumsName)
            {

                lbx.Items.Add(x);
                
            }
        }
        


    }
}
