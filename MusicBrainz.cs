using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TBP
{
    class MusicBrainz
    {
        string artist;
        string mbid;
        List<string> releaseName= new List<string>();
        List<string> releaseId = new List<string>();
        static XNamespace ns = "http://musicbrainz.org/ns/mmd-2.0#";

        public string Artist
        {
            get { return artist; }
            set { artist = value; }
        }
        public string Mbid
        {
            get { return mbid; }
            set { mbid = value; }
        }
        public List<string> ReleaseName
        {
            get { return releaseName; }
        }
        public List<string> ReleaseId
        {
            get { return releaseId; }
        }


        public MusicBrainz(string artist)
        {
            XDocument mbResult = XDocument.Load("http://www.musicbrainz.org/ws/2/artist/?query=" + artist);

            this.artist = artist;
            this.mbid = mbResult.Root.Element(ns + "artist-list").Element(ns + "artist").Attribute("id").Value;
        }

    /*   public string GetArtistMBID(string artist)
        {
            XDocument mbResult = XDocument.Load("http://www.musicbrainz.org/ws/2/artist/?query=Paramore");
            
            return mbResult.Root.Element(ns+"artist-list").Element(ns+"artist").Attribute("id").Value;
        }*/
        public void GetAlbums()
        {
            XDocument getAlbumsDoc = XDocument.Load("http://musicbrainz.org/ws/2/artist/"+mbid+"?inc=releases+media");
            foreach (XElement el in getAlbumsDoc.Root.Element(ns+"artist").Element(ns+"release-list").Elements(ns+"release"))
            {
                if (Convert.ToInt32(el.Element(ns+"medium-list").Element(ns+"medium").Element(ns+"track-list").Attribute("count").Value) > 5)
                {  
                    releaseId.Add(el.Attribute("id").Value);
                    releaseName.Add(el.Element(ns+"title").Value);

                }

            }
            
        }
    }
}
