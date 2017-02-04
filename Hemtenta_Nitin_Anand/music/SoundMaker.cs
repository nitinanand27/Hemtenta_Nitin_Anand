using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class SoundMaker : ISoundMaker
    {
        ISong currentSong;
        public string NowPlaying
        {
            get
            {
                if (currentSong == null)
                {
                    return "";
                }
                else
                {
                    return currentSong.Title;
                }
            }
        }

        public void Play(ISong song)
        {
            currentSong = song;
        }

        public void Stop()
        {
            currentSong = null;
        }
    }
}
