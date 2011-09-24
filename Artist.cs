using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestPlayer
{
    class Artist
    {
        string artistName;
        string artistMbId;
        List<string> albumsId = new List<string>();
        List<string> albumsName = new List<string>();
        static XNamespace ns = "http://musicbrainz.org/ns/mmd-2.0#";
        public string ArtistName
        {
            get { return artistName; }
            set { artistName = value; }
        }
        public string ArtistMbId
        {
            get { return artistMbId; }
            set { artistMbId = value; }
        }
        public List<string> AlbumsId
        {
            get { return albumsId; }
        }
        public List<string> AlbumsName
        {
            get { return albumsName; }
        }
        public Artist(string artistName)
        {
            XDocument mbResult = XDocument.Load("http://www.musicbrainz.org/ws/2/artist/?query=" + artistName);

            this.artistName = artistName;
            this.artistMbId = mbResult.Root.Element(ns + "artist-list").Element(ns + "artist").Attribute("id").Value;
        }
        public Artist()
        {
            this.artistMbId = "";
            this.albumsId = new List<string>();
            this.artistName = "";
            this.albumsName = new List<string>();
        }

        public void GetAlbums()
        {
            XDocument getAlbumsDoc = XDocument.Load("http://musicbrainz.org/ws/2/artist/" + artistMbId + "?inc=release-groups");
            foreach (XElement el in getAlbumsDoc.Root.Element(ns + "artist").Element(ns + "release-group-list").Elements(ns + "release-group"))
            {
                if (el.Attribute("type").Value=="Album")
                {
                    XDocument getReleaseId = XDocument.Load("http://musicbrainz.org/ws/2/release-group/"+el.Attribute("id").Value+"?inc=releases");
                    
                    albumsId.Add(getReleaseId.Root.Element(ns+"release-group").Element(ns+"release-list").Element(ns+"release").Attribute("id").Value);
                    albumsName.Add(el.Element(ns + "title").Value);

                }

            }

        }


    }
}
