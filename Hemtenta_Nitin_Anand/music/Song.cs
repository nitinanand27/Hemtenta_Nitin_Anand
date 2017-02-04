using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class Song : ISong
    {
        string title;
        public string Title
        {
            get
            {
                return title;
            }
        }

        public Song(string title)
        {
            this.title = title;
        }
    }
}
