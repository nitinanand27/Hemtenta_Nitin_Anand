using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HemtentaTdd2017;
using HemtentaTdd2017.music;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest5
    {

        [TestMethod]
        public void LoadSongs_Test()
        {            
            Mock<IMediaDatabase> mdb = new Mock<IMediaDatabase>();
            mdb.Setup(x => x.IsConnected).Returns(true);
            MusicPlayer mp = new MusicPlayer(mdb.Object);

            mp.LoadSongs("test-song");
            mdb.Verify(x => x.FetchSongs("test-song"), Times.Once());
        }

        [TestMethod]
        public void NextSong_Playing_Test()
        {
            Mock<IMediaDatabase> mdb = new Mock<IMediaDatabase>();
            mdb.Setup(x => x.IsConnected).Returns(true);

            MusicPlayer mplayer = new MusicPlayer(mdb.Object);
            string songTitle1 = "Some song";
            string songTitle2 = "song2";

            List<ISong> playlist = new List<ISong>()
                {
                    new Song(songTitle1),
                    new Song(songTitle2)
                };

            mplayer.LoadSongs("search");
            mplayer.NextSong();

            Assert.AreEqual(songTitle1, mplayer.NowPlaying());

        }

        [TestMethod]
        public void Play_NowPlaying_Test()
        {
            Mock<IMediaDatabase> mdb = new Mock<IMediaDatabase>();
            mdb.Setup(x => x.IsConnected).Returns(true);

            MusicPlayer mplayer = new MusicPlayer(mdb.Object);
            string songTitle1 = "Some song"; //giving same title as used in nowplaying function
            string songTitle2 = "song2";

            List<ISong> playlist = new List<ISong>()
                {
                    new Song(songTitle1),
                    new Song(songTitle2)
                };

            mplayer.LoadSongs("search");
            mplayer.Play();

            Assert.AreEqual(songTitle1, mplayer.NowPlaying());
        }

        [TestMethod]
        public void Stop_Test()
        {
            Mock<IMediaDatabase> mdb = new Mock<IMediaDatabase>();
            mdb.Setup(x => x.IsConnected).Returns(true);

            MusicPlayer mplayer = new MusicPlayer(mdb.Object);
            string songTitle1 = "Some song";
            string songTitle2 = "song2";

            List<ISong> playlist = new List<ISong>()
                {
                    new Song(songTitle1),
                    new Song(songTitle2)
                };

            mplayer.LoadSongs("search");
            mplayer.Play();
            mplayer.Stop();

            Assert.AreEqual("Some song", mplayer.NowPlaying());
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseClosedException))]
        public void LoadSongs_DbCloseException_Test()
        {
            Mock<IMediaDatabase> mdb = new Mock<IMediaDatabase>();
            MusicPlayer mplayer = new MusicPlayer(mdb.Object);

            mdb.Setup(x => x.IsConnected).Returns(false); //set db disconnected
            mplayer.LoadSongs("search-string");

        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseAlreadyOpenException))]
        public void DatabaseAlreadyOpen_Exception_Test()
        {
            Mock<IMediaDatabase> mdb = new Mock<IMediaDatabase>();
            //setup so databse is connected here
            mdb.Setup(x => x.IsConnected).Returns(true);
            MusicPlayer mp = new MusicPlayer(mdb.Object);
            mp.ConnectDatabase();
        }
    }
}
