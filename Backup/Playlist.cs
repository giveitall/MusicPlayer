using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    class Playlist
    {
        public ArrayList pl= new ArrayList(30) ;
        public void Clear()
        {
            pl.Clear();
        }

        public Song GetISong(int i)
        {
            return (Song)pl[i];

        }
        public void SetSourceToISong(int i,string source)
        {
            this.GetISong(i).Source = source;

        }

       /* public Playlist(int n)
        {
            for (int i = 0; i < this.pl.Count; ++i)
            {
                this
            }
            
        }
        */
        public void AddSong(Song s)
        {
            pl.Add(s);
        }
        public void Print()
        {
            for (int i = 0; i < pl.Count; i++)
            {
                Song s = (Song)pl[i];
                
            }
        }

    }
}
