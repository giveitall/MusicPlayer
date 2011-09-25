using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TBP
{
    class Album
    {
        string albumMbId;
        string albumName;
        string artistName;
        List<Song> songsList;
        static XNamespace ns = "http://musicbrainz.org/ns/mmd-2.0#";

        public Album(string artistName, string albumName, string albumMbId)
        {
            this.albumMbId = albumMbId;
            this.albumName = albumName;
            this.artistName = artistName;
            this.songsList = new List<Song>();
        }
        public List<Song> SongsList
        {
            get { return songsList; }
        }
        public void GetSongs()
        {
            XDocument songsXml = XDocument.Load("http://musicbrainz.org/ws/2/release/" + albumMbId + "?inc=recordings");
            foreach (XElement x in songsXml.Root.Element(ns + "release").Element(ns + "medium-list").Element(ns + "medium").Element(ns + "track-list").Elements(ns + "track"))
            {
                Song temp = new Song(artistName, albumName, x.Element(ns + "recording").Element(ns + "title").Value, x.Element(ns + "length").Value);
                songsList.Add(temp);
            }

        }
        public void AddToPlaylist(Playlist playlist)
        {
            foreach (Song s in songsList)
            {
                playlist.AddSong(s);
            }
        }
    }
}
