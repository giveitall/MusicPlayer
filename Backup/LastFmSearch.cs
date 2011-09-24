using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;




namespace WpfApplication1
{
    class LastFmSearch
    {
        public static void TopTracks(Playlist pl, int n, string artist)
        {
            //Playlist lfmPlaylist = new Playlist();
            Console.WriteLine("Done");
            XmlDocument xd = new XmlDocument();
            xd.Load("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist="+artist+"&limit="+n.ToString()+"&api_key=b25b959554ed76058ac220b7b2e0a026");
           
            XmlElement xe = xd.DocumentElement;
            XmlNodeList xnl = xe.GetElementsByTagName("name");
            for (int i = 0; i < xnl.Count; i = i + 2)
            {
             pl.AddSong(new Song(xnl[i].InnerXml,artist,""));
             Console.WriteLine(xnl[i].InnerXml);
            }
            



        }

    }
}
