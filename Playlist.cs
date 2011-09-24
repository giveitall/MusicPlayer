using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace TestPlayer
{
    [Serializable()]

    class Playlist 

    {
        private List<Song> listOfSongs=new List<Song>();
        
       /* public Playlist(List<Song> listOfSongs)
        {
            this.listOfSongs = listOfSongs;
        }*/
        public void Clear()
        {
            listOfSongs.Clear();
        }

  public Song GetISong(int i)
        {
            if ((i < listOfSongs.Count)&&i>=0)
                return listOfSongs[i];
            else return listOfSongs[0];

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
            
            listOfSongs.Add(s);
        }
        public int Count()
        {
            return listOfSongs.Count;
        }
        public void EditISong(int i,string uri)
        {
            listOfSongs.ElementAt(i).Source = uri; 
        }
        public List<Song> ListOfSongs
        {
            get {return listOfSongs;}
        }
        public void Play(ListBox listBox1)
        {
            for(int i=0;i<listOfSongs.Count();i++)
            {
                
                listBox1.Items.Add(listOfSongs[i].ArtistName + "-" + listOfSongs[i].SongName);
            }

        }
        public void Add(Playlist pl)
        {
            foreach (Song x in pl.ListOfSongs)
            {
                listOfSongs.Add(x);
            }
        }
        public  void Shuffle()
        {
             Random rnd=new Random();
            listOfSongs=listOfSongs.OrderBy(x => rnd.Next()).ToList();
        }

    }
}
