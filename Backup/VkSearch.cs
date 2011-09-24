using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WpfApplication1
{
    class VkSearch
    {

        public static void GetUri(Playlist playL, string token)
        {
            foreach (Song s in playL.pl)
            {
                GetSongUri(s, token);
            }

        }
        public static string GetSongUri(Song s,string token)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load("https://api.vkontakte.ru/method/audio.search.xml?q="+s.SongName+"-"+s.GroupName+"&count=1&access_token="+token);
           
            XmlElement xe = xd.DocumentElement;
            XmlNodeList xnl = xe.GetElementsByTagName("url");
            //s.Source = xnl[0].InnerXml;
            return xnl[0].InnerXml;
        }



    }
}
