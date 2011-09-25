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

                temp=LastFmSearch.TopTracks(Convert.ToInt32(r1.Match(s).Value), r2.Match(s).Value.Replace(":", ""));
                foreach (Song s6 in temp)
                {
                    myPl2.AddSong(s6);
                }
                
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
