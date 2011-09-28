using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;



namespace TBP
{
    class LastFmSearch
    {
        public static List<Song> TopTracks(int n, string groupName)
        { 
           
            TextWriter writer = new StreamWriter(@"C:\debug.txt", true);
            Stopwatch sw3 = new Stopwatch();
            sw3.Start();
            List<Song> TopTrackList = new List<Song>();
            try
            {
                Stopwatch sw4 = new Stopwatch();
                sw4.Start();
                try
                {
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist=" + groupName + "&limit=" + n.ToString() + "&api_key=b25b959554ed76058ac220b7b2e0a026");
                    //XmlReader xmlReader = new XmlReader("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist=" + groupName + "&limit=" + n.ToString() + "&api_key=b25b959554ed76058ac220b7b2e0a026");

                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                    Stream receiveStream = myHttpWebResponse.GetResponseStream();

                    XDocument ttdoc = XDocument.Load(receiveStream);


                    sw4.Stop();
                    writer.WriteLine("Xml load:" + sw4.ElapsedMilliseconds);


                    foreach (XElement el in ttdoc.Root.Element("toptracks").Elements("track"))
                    {
                        string temp = el.Element("name").Value;

                        string temp2 = el.Element("duration").Value;
                        Song s = new Song(groupName, "", temp, temp2, 0, "");
                        TopTrackList.Add(s);
                    }
                }
                catch (Exception e) { return TopTrackList; }
                sw3.Stop();
                writer.WriteLine("toptrack 1 iter:" + sw3.ElapsedMilliseconds);

                writer.Close();
                return TopTrackList;


            }
            catch (TimeoutException e) { writer.WriteLine("LOLOMG"); return TopTrackList; }
        }
        public static string[] SimArtist(string groupName)
        {
            XDocument ttdoc = XDocument.Load("http://ws.audioscrobbler.com/2.0/?method=artist.getsimilar&artist=" + groupName + "&limit=" + 5 + "&api_key=b25b959554ed76058ac220b7b2e0a026");

            string[] temp=new string[5]; int i=0;
            List<Song> SimTrackList = new List<Song>();

            foreach (XElement el in ttdoc.Root.Element("similarartists").Elements("artist"))
            {
               // string temp = el.Element("name").Value;
                temp[i] = el.Element("name").Value;
                i++;
              //  SimTrackList. += TopTracks(5, temp);
                
               // string temp2 = el.Element("duration").Value;
              //  Song s = new Song(groupName, "", temp, temp2, 0, "");
                //TopTrackList.Add(s);
            }
            return temp;



        }


    }
}
