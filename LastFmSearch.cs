using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Linq;




namespace TBP
{
    class LastFmSearch
    {
        public static List<Song> TopTracks(int n, string groupName)
        {
            XDocument ttdoc = XDocument.Load("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist=" + groupName + "&limit=" + n.ToString() + "&api_key=b25b959554ed76058ac220b7b2e0a026");
            

           List<Song> TopTrackList = new List<Song>();
            
            foreach (XElement el in ttdoc.Root.Element("toptracks").Elements("track"))
            {
                 string temp = el.Element("name").Value; 

                string temp2 = el.Element("duration").Value;
                Song s = new Song(groupName, "", temp, temp2, 0, "");
                TopTrackList.Add(s);
            }
            return TopTrackList;



        }
        public static List<Song> SimArtist(string groupName)
        {
            XDocument ttdoc = XDocument.Load("http://ws.audioscrobbler.com/2.0/?method=artist.getsimilar&artist=" + groupName + "&limit=" + 5.ToString() + "&api_key=b25b959554ed76058ac220b7b2e0a026");


            List<Song> SimTrackList = new List<Song>();

            foreach (XElement el in ttdoc.Root.Element("similarartists").Elements("artist"))
            {
                string temp = el.Element("name").Value;
              //  SimTrackList. += TopTracks(5, temp);
                foreach(Song s in TopTracks(5,temp))
                    SimTrackList.Add(s);
                {
                }
               // string temp2 = el.Element("duration").Value;
              //  Song s = new Song(groupName, "", temp, temp2, 0, "");
                //TopTrackList.Add(s);
            }
            return SimTrackList;



        }


    }
}
