using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TBP
{
    class VkSearch
    {

        
        public static void GetSongUri(Song s,string token)
        {
           
                XDocument songUri = XDocument.Load("https://api.vkontakte.ru/method/audio.search.xml?q=" + s.SongName + "-" + s.ArtistName + "&count=1&access_token=" + token);
                s.Source = songUri.Root.Element("audio").Element("url").Value;
            
          
          
        }

        

    }
}
