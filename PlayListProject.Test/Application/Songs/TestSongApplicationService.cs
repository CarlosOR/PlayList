
using AutoMapper;
using Moq;
using PlayListProject.Application.Dtos;
using PlayListProject.Application.IServices;
using PlayListProject.Application.Services;
using PlayListProject.Domain.Context;
using PlayListProject.Domain.Entities;

namespace PlayListProject.Test.Application.Songs
{
    public class TestSongApplicationService
    {
        private readonly SongApplicationService _songApplicationService;
        private readonly Mock<PlayListProjectDbContext> moqContext;
        private readonly Mock<IMapper> moqMapper;
        private readonly Mock<IPlayListSongApplicationService> moqPlayListSongApplication;
        public TestSongApplicationService()
        {
            _songApplicationService = new SongApplicationService(moqContext.Object, moqMapper.Object, moqPlayListSongApplication.Object);
        }

        [Fact]
        public void ShoudRetournAndTransformData()
        {
            //moqContext.Setup(c => c.Songs).Returns(songs);
        }


        List<Song> songs = new List<Song>()
        {
            new Song
            {
                Id = 1,
                Name = "",
                AuthorId = 1,
                Author = new Author
                {
                    Id = 1,
                    Name = "algo",
                },
                PlayListSongs = new List<PlayListSong>
                {
                    new PlayListSong
                    {
                        PlayListId = 1,
                        PlayList = new PlayList
                        {
                            Id = 1,
                            Name = "adsfa",
                            Description = "adsfasdf"
                        }
                    }
                }
            }
        };
    }
}
