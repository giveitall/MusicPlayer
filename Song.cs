using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TBP
{
    [Serializable()]
    public class Song

    {
        string artistName;
        string albumName;
        string songName;
        string duration;
        int year;
        string source;

        public Song(string artistName,string albumName, string songName,string duration,int year, string source) 
    {
        this.artistName = artistName;
        this.albumName = albumName;
        this.songName = songName;
        this.duration = duration;
        this.year = year;
        this.source = source;
    }
        public Song(string artistName, string albumName, string songName, string duration)
        {
            this.artistName = artistName;
            this.albumName = albumName;
            this.songName = songName;
            this.duration = duration;
           
        }

        //getters setters
        public string SongName
        {
            get { return songName; }
            set { songName = value; }
        }
        public string ArtistName
        {
            get { return artistName; }
            set { artistName = value; }
        }
        public string AlbumName
        {
            get { return albumName; }
            set { albumName = value; }
        }
        public string Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        public int Year
        {
            get { return year; }
            set { year = value; }
        }
        public string Source
        {
            get { return source; }
            set { source = value; }
        }
    }
}
