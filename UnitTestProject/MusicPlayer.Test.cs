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
        Mock<IMediaDatabase> mdb;
        MusicPlayer mp;

        string songTitle1 = "Some song"; //giving same title as used in nowplaying function
        string songTitle2 = "song2";

        public UnitTest5()
        {
            mdb = new Mock<IMediaDatabase>();
            mdb.Setup(x => x.IsConnected).Returns(true);
            mp = new MusicPlayer(mdb.Object);
        }

        [TestMethod]
        public void LoadSongs_Test()
        {
            mp.LoadSongs("test-song");
            mdb.Verify(x => x.FetchSongs("test-song"), Times.Once());
        }

        [TestMethod]
        public void NextSong_Playing_Test()
        {
            List<ISong> playlist = new List<ISong>()
                {
                    new Song(songTitle1),
                    new Song(songTitle2)
                };

            mp.LoadSongs("search");
            mp.NextSong();

            Assert.AreEqual(songTitle1, mp.NowPlaying());

        }

        [TestMethod]
        public void Play_NowPlaying_Test()
        {
            List<ISong> playlist = new List<ISong>()
                {
                    new Song(songTitle1),
                    new Song(songTitle2)
                };

            mp.LoadSongs("search");
            mp.Play();

            Assert.AreEqual(songTitle1, mp.NowPlaying());
        }

        [TestMethod]
        public void Stop_Test()
        {
            List<ISong> playlist = new List<ISong>()
                {
                    new Song(songTitle1),
                    new Song(songTitle2)
                };

            mp.LoadSongs("search");
            mp.Play();
            mp.Stop();

            Assert.AreEqual("Some song", mp.NowPlaying());
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseClosedException))]
        public void LoadSongs_DbCloseException_Test()
        {
            //set db disconnected
            mdb.Setup(x => x.IsConnected).Returns(false); 
            mp.LoadSongs("search-string");

        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseAlreadyOpenException))]
        public void DatabaseAlreadyOpen_Exception_Test()
        {
            //database is set in contructor as connected
            mp.ConnectDatabase();
        }
    }
}
