using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    public class Song

    {
        string groupName;
        string songName;
        string source;
        public Song(string songName,string groupName,string source)
    {
        this.groupName = groupName;
        this.songName = songName;
        this.source = source;
    }
        public Song()
        {
            this.songName = "";
            this.groupName = "";
            this.source = "";
        }
        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        public string SongName
        {
            get { return songName; }
            set { songName = value; }
        }
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

    }
}
