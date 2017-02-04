using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class MusicPlayer : IMusicPlayer
    {
        List<ISong> playlist;

        IMediaDatabase db;

        public MusicPlayer(IMediaDatabase db)
        {
            this.db = db;
            playlist = new List<ISong>();
        }
        public int NumSongsInQueue
        {
            get
            {
                return playlist.Count;
            }
        }

        public void LoadSongs(string search)
        {
            if (!db.IsConnected)
            {
                throw new DatabaseClosedException();
            }
            if (!String.IsNullOrEmpty(search))
            {
                playlist = db.FetchSongs(search);
            }

            db.CloseConnection();
        }

        public void NextSong()
        {
            playlist = new List<ISong>();
            ISoundMaker soundMaker = new SoundMaker();

            if (NumSongsInQueue > 0)
            {
                playlist.RemoveAt(0);
                soundMaker.Play(playlist.FirstOrDefault());
            }
            else
            {
                Stop();
            }
        }

        public string NowPlaying()
        {
            ISoundMaker soundMaker = new SoundMaker();

            if (String.IsNullOrEmpty(soundMaker.NowPlaying))
            {
                return "Some song";
            }
            else
            {
                return soundMaker.NowPlaying;
            }
        }

        public void Play()
        {
            ISoundMaker soundMaker = new SoundMaker();
            playlist = new List<ISong>();

            //checks if no song is playing, next in que plays
            if (String.IsNullOrEmpty(soundMaker.NowPlaying))
            {
                soundMaker.Play(playlist.FirstOrDefault());
            }
            //else no effect
        }

        public void Stop()
        {
            ISoundMaker soundMaker = new SoundMaker();
            soundMaker.Stop();
        }

        public void ConnectDatabase()
        {
            if (db.IsConnected)
            {
                throw new DatabaseAlreadyOpenException();
            }
            else
            {
                db.OpenConnection();
            }
        }
    }
}
